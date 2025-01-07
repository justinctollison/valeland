using UnityEngine;
using UnityEngine.AI;

public class FreezeStatusEffect : StatusEffect
{
    [SerializeField] float speedMultiplier = 0.1f;
    private NavMeshAgent agent;
    private BasicAnimator animator;
    public override void OnApply()
    {
        agent = receiver.GetComponent<NavMeshAgent>();
        animator = receiver.GetComponent<BasicAnimator>();
        if (agent != null) {
            agent.speed *= speedMultiplier;
        }
        if(animator != null) {
            animator.MultiplySpeed(speedMultiplier);
        }
    }

    public override void OnRemoved()
    {
        if(agent != null)
            agent.speed /= speedMultiplier;
        if (animator != null)
            animator.MultiplySpeed(1/speedMultiplier);
        base.OnRemoved();
    }
}
