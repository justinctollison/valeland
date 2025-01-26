using UnityEngine;

public class EnemyCombat : CombatReceiver
{
    protected override void Start()
    {
        _maxHP = GetComponent<BasicAI>().GetNPCData().maxHealth;

        base.Start();
    }

    public override void Die()
    {
        base.Die();

        // We'll notify the AI when the Combat Receiver Dies
        GetComponent<Statemachine>().ChangeState(GetComponent<Statemachine>().GetDeathState());
        // We'll grant the player experience
    }
    public override void Revive()
    {
        base.Revive();

        // We'll notify the AI when the Combat Receiver Dies
        GetComponent<Statemachine>().ChangeState(GetComponent<Statemachine>().GetReviveState());
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
        _healthBarUI.SetHealthBarColor(Color.red);
    }

    public override void OnHoverExit()
    {
        OnHoverExitEvent?.Invoke();
    }
}
