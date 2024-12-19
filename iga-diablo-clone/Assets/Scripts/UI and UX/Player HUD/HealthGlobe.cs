using UnityEngine;

public class HealthGlobe : MonoBehaviour
{
    [SerializeField] GameObject healthFill;

    private void Start()
    {
        EventsManager.Instance.onHealthChanged.AddListener(UpdateHealthBar);
    }

    private void OnDestroy()
    {
        EventsManager.Instance.onHealthChanged.RemoveListener(UpdateHealthBar);
    }

    void UpdateHealthBar(float newHPPercent)
    {
        if (newHPPercent > 1) newHPPercent = 1;
        if (newHPPercent < 0) newHPPercent = 0;

        healthFill.transform.localScale = new Vector3(1, newHPPercent, 1);
    }
}
