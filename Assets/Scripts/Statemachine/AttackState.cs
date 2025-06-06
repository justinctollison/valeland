using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackState : State
{
    public AttackState(Statemachine stateMachine) : base(stateMachine) { }
    CombatReceiver target;
    private float _attackCooldownTimer = 0.0f;
    private int repeatAttackCounter = 0;
    public override void Enter()
    {
        target = basicAI.GetCurrentTarget();
        _attackCooldownTimer = 0.0f;
        repeatAttackCounter = 0;
    }

    public override void Execute()
    {
        _attackCooldownTimer += Time.deltaTime;

        if (_attackCooldownTimer > stateMachine.activeAttack.attackCooldown)
        {
            _attackCooldownTimer -= stateMachine.activeAttack.attackCooldown;
            repeatAttackCounter++;
            SpawnAttackPrefab();
            if (UnityEngine.Random.Range(0, 100) < stateMachine.activeAttack.attackChanceToChangeAttackState || repeatAttackCounter > stateMachine.activeAttack.maxRepeatAttacks)
            {
                repeatAttackCounter = 0;
                SetRandomDifferentAttack();
                return;
            }
            stateMachine.ChangeState(stateMachine.GetWindUpState());
        }

        if (!basicAI.TargetIsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.GetEngageState());
        }

        Vector3 lookDirection = basicAI.GetCurrentTarget().transform.position;
        lookDirection.y = basicAI.transform.position.y;
        stateMachine.transform.LookAt(lookDirection);
    }

    public override void Exit()
    {

    }

    private void SpawnAttackPrefab()
    {
        stateMachine.GetComponent<BasicAnimator>().TriggerAnimation(stateMachine.activeAttack.animatorTriggerAttackName);
        stateMachine.transform.LookAt(basicAI.GetCurrentTarget().transform.position);
        Vector3 attackDirection = (basicAI.GetCurrentTarget().transform.position - stateMachine.transform.position).normalized;
        Vector3 spawnPosition = stateMachine.transform.position + attackDirection;// spawn the attack in front of the AI

        basicAI.InstantiateNewAttack(spawnPosition);
    }
}
