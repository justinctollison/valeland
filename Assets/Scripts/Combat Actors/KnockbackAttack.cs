using UnityEngine;
using System.Collections.Generic;
public class KnockbackAttack : CombatActor
{
    [SerializeField] float _speed = 25;
    private Vector3 _shootDirection = Vector3.zero;
    [SerializeField] private float _knockbackForce = 10;
    List<CombatReceiver> _hitTargets = new List<CombatReceiver>();

    void Start()
    {
        Destroy(gameObject, 3);
    }

    void FixedUpdate()
    {
        transform.Translate(_shootDirection * _speed * Time.fixedDeltaTime);
    }

    protected override void HitReceiever(CombatReceiver target)
    {
        base.HitReceiever(target);
        target.ReceiveKnockbackAwayFromPlayer(_knockbackForce);
        //todo: add freeze effect
        //EffectsManager.Instance.PlaySmallBoom(transform.position, 1);
        //target.ApplyStatusEffect("Freeze");
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
            if (target.GetFactionID() != _factionID && !_hitTargets.Contains(target))
            {
                HitReceiever(target);
                _hitTargets.Add(target);
            }
        }
    }
}
