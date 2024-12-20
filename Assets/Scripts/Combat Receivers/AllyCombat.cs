using UnityEngine;

public class AllyCombat : CombatReceiver
{
    protected override void Start()
    {
        _maxHP = GetComponent<BasicAI>().GetNPCData().maxHealth;

        base.Start();
    }

    protected override void Update()
    {
        
    }

    public override void Die()
    {
        base.Die();

        // We'll notify the AI when the Combat Receiver Dies
        GetComponent<Statemachine>().ChangeState(GetComponent<Statemachine>().GetDeathState());
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        OnTakeDamageEvent?.Invoke();
    }

    public override void OnHover()  
    {
        OnHoverEnterEvent?.Invoke();
        _healthBarUI.SetHealthBarColor(Color.green);
    }

    public override void OnHoverExit()
    {
        OnHoverExitEvent?.Invoke();
    }
}
