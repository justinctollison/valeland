using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BasicAI : MonoBehaviour
{
    [SerializeField] private NPCData _data;

    [SerializeField] private List<CombatReceiver> _targetsList = new List<CombatReceiver>();
    [SerializeField] private CombatReceiver _currentTarget;

    protected Statemachine _stateMachine;
    protected NavMeshAgent _agent;
    protected bool alive;
    protected FactionID _factionID = 0;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _stateMachine = GetComponent<Statemachine>();
    }

    protected virtual void Start()
    {
        SetFactionID(_data.factionID);
    }

    protected virtual void Update()
    {
        TargetsListSorting();
    }
    #region Engaging
    protected virtual void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject.GetComponent<CombatReceiver>();
        if (target && !other.isTrigger)
        {
            if (target.GetFactionID() != this.GetComponent<CombatReceiver>().GetFactionID())
            {
                AddToTargetsList(target);
                _stateMachine.ChangeState(_stateMachine.GetEngageState());
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        var target = other.gameObject.GetComponent<CombatReceiver>();
        if (target && !other.isTrigger)
        {
            if (target.GetFactionID() != this.GetComponent<CombatReceiver>().GetFactionID())
            {
                RemoveTargetsFromList(target);
            }
        }
    }
    #endregion
    #region TargetsList
    public void AddToTargetsList(CombatReceiver target)
    {
        if (!_targetsList.Contains(target))
        {
            _targetsList.Add(target);
            _currentTarget = _targetsList[0];
        }
    }

    public void RemoveTargetsFromList(CombatReceiver target)
    {
        if (_targetsList.Contains(target))
        {
            _targetsList.Remove(target);
        }
    }

    public void RemoveTargetBasedOnDistance()
    {
        foreach (var target in _targetsList.ToList())
        {
            if (Vector3.Distance(this.transform.position, target.transform.position) > _data.engageDistance)
            {
                _targetsList.Remove(target);
            }
        }
    }

    public void TargetsListSorting()
    {
        if (_targetsList.Count == 0)
        {
            _currentTarget = null;
            return;
        }

        foreach (var target in _targetsList.ToList())
        {
            var distance = Vector3.Distance(this.transform.position, target.transform.position);

            _targetsList.Sort((target1, target2) =>
            {
                float distance1 = Vector3.Distance(this.transform.position, target1.transform.position);
                float distance2 = Vector3.Distance(this.transform.position, target2.transform.position);
                return distance1.CompareTo(distance2);
            });

            _currentTarget = _targetsList[0];
        }
    }

    public void DropItems()
    {
        if (_data.lootTable != null)
        {
            List<ItemData> droppedItems = LootManager.GenerateLoot(_data.lootTable);

            foreach (ItemData item in droppedItems)
            {
                Debug.Log($"Dropped item: {item.itemName}");
                InventoryManager.Instance.AddItem(item);
            }
        }
    }

    #endregion
    #region Attacking

    public void InstantiateNewAttack(Vector3 spawnPosition)
    {
        GameObject newAttack = Instantiate(_stateMachine.activeAttack.AttackPrefab, spawnPosition, Quaternion.identity);

        //TODO Change to NPC data, each NPC has a set crit chance
        float critMod = 1f;
        float calculatedDamage = Mathf.Round(Random.Range(_stateMachine.activeAttack.minDamage, _stateMachine.activeAttack.maxDamage) * critMod);

        CombatActor attackActor = newAttack.GetComponent<CombatActor>();
        attackActor.InitializeDamage(calculatedDamage);
        attackActor.SetFactionID(GetComponent<CombatReceiver>().GetFactionID());
        switch(attackActor.attackType)
        {
            case AttackType.Melee:
                break;
            case AttackType.Projectile:
                Projectile projectile = newAttack.GetComponent<Projectile>();
                //newAttack.transform.LookAt(_currentTarget.transform.position);
                projectile.SetShootDirection(((_currentTarget.transform.position - transform.position).normalized));
                break;
            case AttackType.AreaSpell:
                newAttack.transform.position = transform.position;
                break;
            case AttackType.MultiProjectile:
                Destroy(newAttack);
                Projectile[] projectiles = new Projectile[8];
                for (int i = 0; i < 8; i++)
                {
                    projectiles[i] = Instantiate(_stateMachine.activeAttack.AttackPrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
                    
                    calculatedDamage = Mathf.Round(Random.Range(_stateMachine.activeAttack.minDamage, _stateMachine.activeAttack.maxDamage) * critMod);

                    attackActor = projectiles[i].GetComponent<CombatActor>();
                    attackActor.InitializeDamage(calculatedDamage);
                    attackActor.SetFactionID(GetComponent<CombatReceiver>().GetFactionID());

                    Vector3 direction = Quaternion.Euler(0, 45 * i, 0) * (_currentTarget.transform.position - transform.position).normalized;
                    projectiles[i].SetShootDirection(direction);
                }
                break;
        }
    }
    public bool TargetIsInAttackRange()
    {
        if (_currentTarget != null)
        {
            return Vector3.Distance(transform.position, _currentTarget.transform.position) <= _stateMachine.activeAttack.attackRange;
        }

        return false;
    }
    #endregion

    public virtual void SetFactionID(FactionID newID)
    {
        _factionID = newID;
        GetComponent<CombatReceiver>().SetFactionID(newID);
    }
    public List<CombatReceiver> GetTargetsList() => _targetsList;
    public CombatReceiver GetCurrentTarget() => _currentTarget;
    public NPCData GetNPCData() => _data;
}
