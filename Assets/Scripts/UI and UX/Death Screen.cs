using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject _deathScreen; // attach the child of this object to this field. Assigning the object this is attached to will stop the onPlayerDied event from calling this object.
    private void OnEnable()
    {
        EventsManager.Instance.onPlayerDied.AddListener(ShowDeathScreen);
        EventsManager.Instance.onPlayerRevived.AddListener(HideDeathScreen);
        HideDeathScreen();
    }
    private void OnDisable()
    {
        EventsManager.Instance.onPlayerDied.RemoveListener(ShowDeathScreen);
        EventsManager.Instance.onPlayerRevived.RemoveListener(HideDeathScreen);
    }

    private void ShowDeathScreen()
    {
        _deathScreen.SetActive(true);
    }
    public void HideDeathScreen()
    {
        _deathScreen.SetActive(false);
    }

    public void RevivePlayer()
    {
        FindFirstObjectByType<PlayerCombat>().Revive();
    }
}
