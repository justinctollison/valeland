using System.Collections.Generic;
using UnityEngine;

public class IceExplosionAttack : CombatActor
{
    public float knockbackForce = 7;
    public float knockbackDuration = .3f;
    List<CombatReceiver> _hitTargets = new List<CombatReceiver>();
    void Start()
    {
        Destroy(gameObject, 50f);
    }

    protected override void HitReceiever(CombatReceiver target)
    {
        if (target.HasStatusEffect("Burn"))
        {
            target.RemoveStatusEffect("Burn");
            base.HitReceieverMultiplied(target, 2);
        }
        else
        {
            base.HitReceiever(target);
        }
        target.ApplyStatusEffect("Freeze");
        target.ReceiveKnockbackAwayFromPlayer(knockbackForce, knockbackDuration);
        //target.GetComponentInChildren<RagDollController>().EnableRagdoll();
    }

    protected override void OnTriggerEnter(Collider other)
    {}
    private void OnTriggerStay(Collider other)
    {
        var target = other.GetComponent<CombatReceiver>();

        if (CanDamageReceiver(other, target))
        {
            HitReceiever(target);
            _hitTargets.Add(target);
        }
    }

    private bool CanDamageReceiver(Collider other, CombatReceiver target)
    {
        return target != null && !other.isTrigger && target.GetFactionID() != _factionID && !_hitTargets.Contains(target);
    }
}
