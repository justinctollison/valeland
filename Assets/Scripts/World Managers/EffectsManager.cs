using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager Instance;

    [SerializeField] GameObject smallEffect;
    [SerializeField] GameObject bigEffect;
    [SerializeField] GameObject iceExplosionEffect;
    [SerializeField] GameObject bloodSplurt;
    [SerializeField] GameObject damageIndicatorPrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }


    GameObject SpawnEffect(GameObject effectPrefab, Vector3 location, float duration, Transform effectParent = null)
    {
        GameObject newEffect = Instantiate(effectPrefab, location, Quaternion.identity);
        if (effectParent != null) newEffect.transform.SetParent(effectParent);
        if (duration > 0) Destroy(newEffect, duration);
        return newEffect;
    }

    public void PlaySmallBoom(Vector3 location, float duration, Transform effectParent = null)
    {
        SpawnEffect(smallEffect, location, duration, effectParent);
    }
    public void PlayBigBoom(Vector3 location, float duration, Transform effectParent = null)
    {
        SpawnEffect(bigEffect, location, duration, effectParent);
    }
    public void PlayIceExplosion(Vector3 location, float duration, Transform effectParent = null)
    {
        SpawnEffect(iceExplosionEffect, location, duration, effectParent);
    }
    public void PlayDamageIndicator(string text, Vector3 location, float duration = 1, Transform effectParent = null)
    {
        GameObject fx = SpawnEffect(damageIndicatorPrefab, location, duration, effectParent);
        DamageIndicator damageIndicator = fx.GetComponent<DamageIndicator>();
        damageIndicator.SetDamageText(text);
        damageIndicator.FaceOut(duration);
    }
    public void PlayBloodSplurt(Vector3 location, float duration, Transform effectParent = null)
    {
        SpawnEffect(bloodSplurt, location, duration, effectParent);
    }

}
