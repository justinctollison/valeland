using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Statemachine : MonoBehaviour
{
    [SerializeField] private NPCData _npcData;
    [SerializeField] private GameObject _attackPrefab;

    [SerializeField] private List<CombatReceiver> _targetsList = new List<CombatReceiver>();
    [SerializeField] private CombatReceiver _currentTarget;

    [SerializeField] private string _currentStateName;
    private State _currentState;

    private IdleState _idleState;
    private PatrolState _patrolState;
    private EngageState _engageState;
    private AttackState _attackState;
    private DeathState _deathState;

    //TODO: Refactor functionality into AI scripts

    private void Awake()
    {
        // We construct the new States at runtime

        _idleState = new IdleState(this);
        _patrolState = new PatrolState(this);
        _engageState = new EngageState(this);
        _attackState = new AttackState(this);
        _deathState = new DeathState(this);
    }

    private void Start()
    {
        ChangeState(_idleState);

        EventsManager.Instance.onPlayerDied.AddListener(_idleState.Enter);
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.Execute();
        }

        TargetsListSorting();
    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject.GetComponent<CombatReceiver>();
        if (target && !other.isTrigger)
        {
            if (target.GetFactionID() != this.GetComponent<CombatReceiver>().GetFactionID())
            {
                AddToTargetsList(target);
            }
        }
    }

    private void OnTriggerExit(Collider other)
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

    public void ChangeState(State newState)
    {
        // We will Exit our current state and enter our new state and then execute that state.
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = newState;
        _currentState.Enter();

        _currentStateName = _currentState.GetType().Name;
    }

    public void AddToTargetsList(CombatReceiver target)
    {
        if (!_targetsList.Contains(target))
        {
            _targetsList.Add(target);
            _currentTarget = _targetsList[0];
            ChangeState(_engageState);
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
            if (Vector3.Distance(this.transform.position, target.transform.position) > _npcData.engageDistance)
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
        float calculatedDamage = Random.Range(_npcData.minDamage, _npcData.maxDamage) * critMod;

        newAttack.GetComponent<CombatActor>().InitializeDamage(calculatedDamage);
        newAttack.GetComponent<CombatActor>().SetFactionID(GetComponent<CombatReceiver>().GetFactionID());
    }

    public bool TargetIsInAttackRange()
    {
        if (_currentTarget != null)
        {
            return Vector3.Distance(transform.position, _currentTarget.transform.position) <= _npcData.attackRange;
        }

        return false;
    }

    public List<CombatReceiver> GetTargetsList() => _targetsList;
    public CombatReceiver GetCurrentTarget() => _currentTarget;
    public NPCData GetNPCData() => _npcData;
    public IdleState GetIdleState() => _idleState;
    public PatrolState GetPatrolState() => _patrolState;
    public EngageState GetEngageState() => _engageState;
    public AttackState GetAttackState() => _attackState;
    public DeathState GetDeathState() => _deathState;
}
