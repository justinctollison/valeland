using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Statemachine : MonoBehaviour
{
    [SerializeField] private string _currentStateName;
    private State _currentState;
    public EnemyAttack activeAttack;

    private IdleState _idleState;
    private PatrolState _patrolState;
    private EngageState _engageState;
    private AttackState _attackState;
    private WindUpState _windUpState;
    private DeathState _deathState;

    private void Awake()
    {
        // We construct the new States at runtime

        _idleState = new IdleState(this);
        _patrolState = new PatrolState(this);
        _engageState = new EngageState(this);
        _attackState = new AttackState(this);
        _deathState = new DeathState(this);
        _windUpState = new WindUpState(this);
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

    public IdleState GetIdleState() => _idleState;
    public PatrolState GetPatrolState() => _patrolState;
    public EngageState GetEngageState() => _engageState;
    public AttackState GetAttackState() => _attackState;
    public DeathState GetDeathState() => _deathState;
    public WindUpState GetWindUpState() => _windUpState;
}
