using UnityEngine;

public class EnemyCombat : CombatReceiver
{
    protected override void Start()
    {
        _maxHP = GetComponent<Statemachine>().GetNPCData().maxHealth;

        base.Start();
    }

    public override void Die()
    {
        base.Die();

        // We'll notify the AI when the Combat Receiver Dies
        GetComponent<Statemachine>().ChangeState(GetComponent<Statemachine>().GetDeathState());
        // We'll grant the player experience
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        OnTakeDamageEvent?.Invoke();
    }

    public override void OnHover()
    {
        OnHoverEnterEvent?.Invoke();
    }

    public override void OnHoverExit()
    {
        OnHoverExitEvent?.Invoke();
    }
}
