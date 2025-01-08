using UnityEngine;

public class DeathState : State
{
    public DeathState(Statemachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        TriggerDeath();

        // Add some Experience!
        EventsManager.Instance.onExperienceGranted.Invoke(data.experienceValue);
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }

    public void TriggerDeath()
    {
        if (!isAlive)
        {
            return;
        }

        isAlive = false;
        basicAI.DropItems();

        if (stateMachine.GetComponent<EnemyAnimator>() != null)
        {
            stateMachine.GetComponent<EnemyAnimator>().TriggerDeath();
        }

        Collider[] attachedColliders = stateMachine.GetComponentsInChildren<Collider>();
        foreach (Collider collider in attachedColliders)
        {
            collider.enabled = false;
        }

        agent.enabled = false;
    }
}
