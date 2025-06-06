using UnityEngine;

public class Projectile : CombatActor
{
    [SerializeField] float _speed = 25;
    private Vector3 _shootDirection = Vector3.zero;

    void Start()
    {
        Destroy(gameObject, 5);
    }

    void FixedUpdate()
    {
        transform.Translate(_shootDirection * _speed * Time.fixedDeltaTime);
    }

    protected override void HitReceiever(CombatReceiver target)
    {
        base.HitReceiever(target);
    }

    public void SetShootDirection(Vector3 newDirection)
    {
        _shootDirection = newDirection;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponent<CombatReceiver>();

        if (CanDamageReceiver(other, target))
        {
            HitReceiever(target);
            Destroy(gameObject);
        }
    }
    bool CanDamageReceiver(Collider other, CombatReceiver target)
    {
        return target != null && !other.isTrigger && target.GetFactionID() != _factionID;
    }
}
