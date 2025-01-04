using UnityEngine;

public class CombatActor : MonoBehaviour
{
    protected FactionID _factionID = 0;
    [SerializeField] protected float _damage = 1;

    public virtual void InitializeDamage(float amount)
    {
        _damage = amount;
    }

    public void SetFactionID(FactionID newID)
    {
        _factionID = newID;
    }

    protected virtual void HitReceiever(CombatReceiver target)
    {
        target.TakeDamage(_damage);
    }
    protected virtual void HitReceiever(CombatReceiver target,float damageMultiplier)
    {
        target.TakeDamage(_damage * damageMultiplier);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponent<CombatReceiver>();

        if (target != null && !other.isTrigger)
        {
            if (target.GetFactionID() != _factionID)
            {
                HitReceiever(target);
            }
        }
    }

    public FactionID GetFactionID() => _factionID;
    public float GetBaseDamage() => _damage;
}
