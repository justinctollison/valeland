using System;
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

        if (_attackCooldownTimer > stateMachine.activeAttack.attackCooldown)
        {
            _attackCooldownTimer -= stateMachine.activeAttack.attackCooldown;
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
        stateMachine.GetComponent<BasicAnimator>().TriggerAttack();
        stateMachine.transform.LookAt(basicAI.GetCurrentTarget().transform.position);
        Vector3 attackDirection = (basicAI.GetCurrentTarget().transform.position - stateMachine.transform.position).normalized;
        Vector3 spawnPosition = stateMachine.transform.position + attackDirection * 2;// spawn the attack in front of the AI

        basicAI.InstantiateNewAttack(spawnPosition);
    }
}
