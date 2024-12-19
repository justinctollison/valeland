using UnityEngine;

public class MeleeAttackCA : CombatActor
{
    private void Start()
    {
        Destroy(gameObject, 0.1f);
    }
}
