using UnityEngine;

public class EngageState : State
{
    public EngageState(Statemachine stateMachine) : base(stateMachine) { }

    CombatReceiver target;

    public override void Enter()
    {
        target = basicAI.GetCurrentTarget();
    }

    public override void Execute()
    {
        target = basicAI.GetCurrentTarget();

        if (target == null)
        {
            stateMachine.ChangeState(stateMachine.GetIdleState());
            return;
        }

        basicAI.TargetsListSorting();
        agent.destination = target.transform.position;

        if (basicAI.TargetIsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.GetAttackState());
        }

        basicAI.RemoveTargetBasedOnDistance();

        if (basicAI.GetTargetsList().Count <= 0)
        {
            stateMachine.ChangeState(stateMachine.GetIdleState());
        }
    }

    public override void Exit()
    {

    }
}
