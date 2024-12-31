using UnityEngine;

public class FireballCA : CombatActor
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

        EffectsManager.Instance.PlaySmallBoom(transform.position, 1);
        target.ApplyStatusEffect<BurnStatus>();
    }

    public void SetShootDirection(Vector3 newDirection)
    {
        _shootDirection = newDirection;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponent<CombatReceiver>();

        if (target != null && !other.isTrigger)
        {
            if (target.GetFactionID() != _factionID)
            {
                HitReceiever(target);
                Destroy(gameObject);
            }
        }
    }
}
