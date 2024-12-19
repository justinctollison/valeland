using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager Instance;

    [SerializeField] GameObject smallEffect;
    [SerializeField] GameObject bigEffect;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }


    void SpawnEffect(GameObject effectPrefab, Vector3 location, float duration, Transform effectParent = null)
    {
        GameObject newEffect = Instantiate(effectPrefab, location, Quaternion.identity);
        if (effectParent != null) newEffect.transform.SetParent(effectParent);
        if (duration > 0) Destroy(newEffect, duration);
    }

    public void PlaySmallBoom(Vector3 location, float duration, Transform effectParent = null)
    {
        SpawnEffect(smallEffect, location, duration, effectParent);
    }
    public void PlayBigBoom(Vector3 location, float duration, Transform effectParent = null)
    {
        SpawnEffect(bigEffect, location, duration, effectParent);
    }
}
