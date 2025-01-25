using System;
using System.Linq;
using UnityEngine;
public class EngageState : State
{
    public EngageState(Statemachine stateMachine) : base(stateMachine) { }

    CombatReceiver target;
    float timeOutTimer = 0.0f;
    System.Random random = new System.Random();
    public override void Enter()
    {
        target = basicAI.GetCurrentTarget();
        SetRandomActiveAttack();
    }

    public override void Execute()
    {
        target = basicAI.GetCurrentTarget();

        if (target == null)
        {
            stateMachine.ChangeState(stateMachine.GetIdleState());
            return;
        }

        timeOutTimer += Time.deltaTime;
        if(timeOutTimer > stateMachine.activeAttack.maxTimeOutSeconds)
        {
            SetRandomActiveAttackWithGreaterRange();
            timeOutTimer = 0.0f;
        }

        basicAI.TargetsListSorting();
        agent.destination = target.transform.position;

        if (basicAI.TargetIsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.GetWindUpState());
            timeOutTimer = 0.0f;
        }

        basicAI.RemoveTargetBasedOnDistance();

        if (basicAI.GetTargetsList().Count <= 0)
        {
            stateMachine.ChangeState(stateMachine.GetIdleState());
            timeOutTimer = 0.0f;
        }
    }

    public override void Exit()
    {

    }
}
