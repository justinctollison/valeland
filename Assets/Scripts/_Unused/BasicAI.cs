using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BasicAI : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected bool alive;
    protected int factionID = 0;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Start()
    {

    }

    protected  virtual void Update()
    {
        if (alive)
        {
            RunAI();
        }
    }

    protected virtual void RunAI()
    {

    }

    public virtual void SetFactionID(int newID)
    {
        factionID = newID;
        GetComponent<CombatReceiver>().SetFactionID(newID);
    }

    public virtual void TriggerDeath()
    {
        if (!alive)
        {
            return;
        }

        alive = false;

        if (GetComponent<EnemyAnimator>() != null)
        {
            GetComponent<EnemyAnimator>().TriggerDeath();
        }

        Collider[] attachedColliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in attachedColliders)
        {
            collider.enabled = false;
        }

        agent.enabled = false;
    }
}
