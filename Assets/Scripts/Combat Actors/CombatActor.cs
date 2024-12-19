using UnityEngine;

public class CombatActor : MonoBehaviour
{
    protected int _factionID = 0;
    protected float _damage = 1;

    public virtual void InitializeDamage(float amount)
    {
        _damage = amount;
    }

    public void SetFactionID(int newID)
    {
        _factionID = newID;
    }

    protected virtual void HitReceiever(CombatReceiver target)
    {
        target.TakeDamage(_damage);
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

    public int GetFactionID() => _factionID;
}
