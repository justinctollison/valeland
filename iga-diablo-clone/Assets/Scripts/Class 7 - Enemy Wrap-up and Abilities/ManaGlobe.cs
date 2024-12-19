using UnityEngine;

public class ManaGlobe : MonoBehaviour
{
    [SerializeField] private GameObject _manaFill;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventsManager.Instance.onManaChanged.AddListener(UpdateManaBar);
    }

    private void OnDestroy()
    {
        EventsManager.Instance.onManaChanged.RemoveListener(UpdateManaBar);
    }

    void UpdateManaBar(float newManaPercent)
    {
        if (newManaPercent > 1) newManaPercent = 1;
        if (newManaPercent < 0) newManaPercent = 0;

        _manaFill.transform.localScale = new Vector3(1, newManaPercent, 1);
    }
}
