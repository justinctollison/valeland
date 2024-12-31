using UnityEngine;

public class DamageOverTimeStatus : StatusEffect
{
    [SerializeField] protected float damagePerTick;
    [SerializeField] protected float timeBetweenTicksSeconds;
    protected float _timeSinceLastTick = 0;

    public override void RunStatusEffect()
    {
        _timeSinceLastTick += Time.deltaTime;

        if (_timeSinceLastTick >= timeBetweenTicksSeconds)
        {
            _timeSinceLastTick = 0;
            receiver.TakeDamage(damagePerTick);
        }
        base.RunStatusEffect();
    }
}
