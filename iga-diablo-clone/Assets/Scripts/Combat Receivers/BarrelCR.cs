using UnityEngine;

public class BarrelCR : CombatReceiver
{
    public override void Die()
    {
        base.Die();

        _maxHP = 10f;

        Destroy(gameObject);
    }
}
