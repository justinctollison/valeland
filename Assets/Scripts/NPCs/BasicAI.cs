using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BasicAI : MonoBehaviour
{
    [SerializeField] private NPCData _data;
    [SerializeField] private GameObject _attackPrefab;

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

    public virtual void SetFactionID(FactionID newID)
    {
        _factionID = newID;
        GetComponent<CombatReceiver>().SetFactionID(newID);
    }

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

    public void InstantiateNewAttack(Vector3 spawnPosition)
    {
        GameObject newAttack = Instantiate(_attackPrefab, spawnPosition, Quaternion.identity);

        //TODO Change to NPC data, each NPC has a set crit chance
        float critMod = 1f;
        float calculatedDamage = Mathf.Round(Random.Range(_data.minDamage, _data.maxDamage) * critMod);

        newAttack.GetComponent<CombatActor>().InitializeDamage(calculatedDamage);
        newAttack.GetComponent<CombatActor>().SetFactionID(GetComponent<CombatReceiver>().GetFactionID());
    }

    public bool TargetIsInAttackRange()
    {
        if (_currentTarget != null)
        {
            return Vector3.Distance(transform.position, _currentTarget.transform.position) <= _data.attackRange;
        }

        return false;
    }

    public List<CombatReceiver> GetTargetsList() => _targetsList;
    public CombatReceiver GetCurrentTarget() => _currentTarget;
    public NPCData GetNPCData() => _data;
}
