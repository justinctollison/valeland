using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ArrowAttack : Projectile
{
    public GameObject arrowMesh;
    private void Start()
    {
        arrowMesh.transform.LookAt(PlayerController.Instance.transform);
    }
    protected override void HitReceiever(CombatReceiver target)
    {
        EffectsManager.Instance.PlayBloodSplurt(transform.position, 1);
        base.HitReceiever(target);
        Destroy(gameObject);
    }
}
