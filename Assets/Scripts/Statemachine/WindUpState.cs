using Unity.VisualScripting.FullSerializer;
using UnityEngine;
//This class is currently only used for the arrow attack, if no windupTime is set to 0 for the attack
//it will effectively skip the windup state and go straight to the attack state
public class WindUpState : State
{
    public WindUpState(Statemachine stateMachine) : base(stateMachine) { }

    CombatReceiver target;
    public float timeToCharge;
    private float chargeTimer = 0.0f;
    public override void Enter()
    {
        if(stateMachine.activeAttack.windUpTime == 0.0f)
        {
            stateMachine.ChangeState(stateMachine.GetAttackState());
            return;
        }
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
        Debug.Log(chargeTimer);
        chargeTimer += Time.deltaTime;

        if (chargeTimer > timeToCharge)
        {
            stateMachine.ChangeState(stateMachine.GetAttackState());
            chargeTimer = 0.0f;
        }
    }

    public override void Exit()
    {

    }
}
