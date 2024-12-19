using UnityEngine;

public class EngageState : State
{
    public EngageState(Statemachine stateMachine) : base(stateMachine) { }

    CombatReceiver target;

    public override void Enter()
    {
        target = stateMachine.GetCurrentTarget();
    }

    public override void Execute()
    {
        target = stateMachine.GetCurrentTarget();

        if (target == null)
        {
            stateMachine.ChangeState(stateMachine.GetIdleState());
            return;
        }

        stateMachine.TargetsListSorting();
        agent.destination = target.transform.position;

        if (stateMachine.TargetIsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.GetAttackState());
        }

        stateMachine.RemoveTargetBasedOnDistance();

        if (stateMachine.GetTargetsList().Count <= 0)
        {
            stateMachine.ChangeState(stateMachine.GetIdleState());
        }
    }

    public override void Exit()
    {

    }
}
