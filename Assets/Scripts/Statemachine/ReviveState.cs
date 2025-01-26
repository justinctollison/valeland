using UnityEngine;

public class ReviveState : State
{
    public ReviveState(Statemachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        TriggerRevive();
        stateMachine.ChangeState(stateMachine.GetIdleState());
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }

    public void TriggerRevive()
    {
        isAlive = true;

        if (stateMachine.GetComponent<EnemyAnimator>() != null)
        {
            stateMachine.GetComponent<EnemyAnimator>().TriggerRevive();
        }
        Collider[] attachedColliders = stateMachine.GetComponentsInChildren<Collider>();
        foreach (Collider collider in attachedColliders)
        {
            collider.enabled = true;
        }

        agent.enabled = true;
    }
}
