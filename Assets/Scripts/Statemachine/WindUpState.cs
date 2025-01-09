using Unity.VisualScripting.FullSerializer;
using UnityEngine;
public class WindUpState : State
{
    public WindUpState(Statemachine stateMachine) : base(stateMachine) { }

    CombatReceiver target;
    public float timeToCharge;
    private float chargeTimer = 0.0f;
    public override void Enter()
    {
        chargeTimer = 0.0f;
        agent.destination = stateMachine.transform.position;
        target = basicAI.GetCurrentTarget();
        timeToCharge = stateMachine.activeAttack.windUpTime;
        if(stateMachine.activeAttack.animatorTriggerWindUpName != null && stateMachine.activeAttack.animatorTriggerWindUpName != "")
            stateMachine.GetComponent<BasicAnimator>().TriggerAnimation(stateMachine.activeAttack.animatorTriggerWindUpName);
    }
    public override void Execute()
    {
        if (!basicAI.TargetIsInAttackRange())
        {
            stateMachine.GetComponent<BasicAnimator>().TriggerAnimation("CancelAttack");
            stateMachine.ChangeState(stateMachine.GetEngageState());
            return;
        }
        Vector3 lookDirection = basicAI.GetCurrentTarget().transform.position;
        lookDirection.y = 0;
        stateMachine.transform.LookAt(lookDirection);
        chargeTimer += Time.deltaTime;
        //stateMachine.transform.LookAt(target.transform.position);
        if (chargeTimer > timeToCharge)
        {
            stateMachine.ChangeState(stateMachine.GetAttackState());
        }
    }

    public override void Exit()
    {

    }
}
