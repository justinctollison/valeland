using UnityEngine;

public class FireballCA : Projectile
{
    protected override void HitReceiever(CombatReceiver target)
    {
        //deal double damage to frozen targets
        if (target.HasStatusEffect("Freeze"))
        {
            target.RemoveStatusEffect("Freeze");
            base.HitReceieverMultiplied(target, 2);
        }
        else
            base.HitReceiever(target);

        EffectsManager.Instance.PlaySmallBoom(transform.position, 1);
        target.ApplyStatusEffect("Burn");
        //target.ReceiveKnockbackAwayFromPlayer(10);
    }
}
