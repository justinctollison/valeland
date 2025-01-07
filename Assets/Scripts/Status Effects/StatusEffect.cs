using UnityEngine;
public abstract class StatusEffect : MonoBehaviour
{
    [SerializeField] public string effectName;
    [SerializeField] protected float duration;
    protected float timeElapsed;
    [HideInInspector] public CombatReceiver receiver;

    //cosmetic variables
    [SerializeField] protected Color TextColor;
    [SerializeField] protected ParticleSystem ApplicationParticles;

    public virtual void OnApply()
    {
        // Play the application particles
        //ApplicationParticles.Play();
    }
    public virtual void OnRemoved()
    {
        Destroy(gameObject);
    }

    public virtual void RunStatusEffect()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= duration)
        {
            receiver.RemoveStatusEffect(this);
        }
    }
    public void RefreshDuration()
    {
        timeElapsed = 0;
    }
}
