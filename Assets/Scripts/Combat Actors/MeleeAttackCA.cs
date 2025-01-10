using UnityEngine;

public class MeleeAttackCA : CombatActor
{
    private void Start()
    {
        Destroy(gameObject, 0.1f);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        AudioManager.Instance.PlayMeleeCombatSFX();
    }
}
