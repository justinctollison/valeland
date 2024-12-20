using UnityEngine;

public class AttackState : State
{
    public AttackState(Statemachine stateMachine) : base(stateMachine) { }

    private float _attackCooldownTimer = 0.0f;

    public override void Enter()
    {

    }

    public override void Execute()
    {
        _attackCooldownTimer += Time.deltaTime;

        if (_attackCooldownTimer > data.attackCooldown)
        {
            _attackCooldownTimer -= data.attackCooldown;
            stateMachine.GetComponent<BasicAnimator>().TriggerAttack();
            SpawnAttackPrefab();
        }

        if (!basicAI.TargetIsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.GetEngageState());
        }
    }

    public override void Exit()
    {

    }

    private void SpawnAttackPrefab()
    {
        stateMachine.transform.LookAt(basicAI.GetCurrentTarget().transform.position);
        Vector3 attackDirection = (basicAI.GetCurrentTarget().transform.position - stateMachine.transform.position).normalized;
        Vector3 spawnDirection = (attackDirection * data.attackRange) + stateMachine.transform.position;

        basicAI.InstantiateNewAttack(spawnDirection);
    }
}
