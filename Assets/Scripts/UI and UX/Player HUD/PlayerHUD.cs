using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] GameObject _statLevelUpButton;
    [SerializeField] GameObject _skillLevelUpButton;

    private void Start()
    {
        EventsManager.Instance.onPlayerLeveledUp.AddListener(ShowStatLevelUpButton);
        EventsManager.Instance.onPlayerLeveledUp.AddListener(ShowSkillLevelUpButton);

        HideStatLevelUpButton();
        HideSkillLevelUpButton();
    }
    private void OnDestroy()
    {
        EventsManager.Instance.onPlayerLeveledUp.RemoveListener(ShowStatLevelUpButton);
        EventsManager.Instance.onPlayerLeveledUp.RemoveListener(ShowSkillLevelUpButton);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) { ToggleCharacterStatPanel(); }
        if (Input.GetKeyDown(KeyCode.T)) { ToggleSkillTreePanel(); }
        if (Input.GetKeyDown(KeyCode.I)) { ToggleInventory(); ToggleEquipment(); }
        if (Input.GetKeyDown(KeyCode.B)) { ToggleEquipment(); ToggleInventory(); }
    }

    public void ToggleCharacterStatPanel()
    {
        UIManager.Instance.ToggleCharacterStatsPanel();
        AudioManager.Instance.PlayUIButtonSFX();

        HideStatLevelUpButton();
    }

    public void ToggleSkillTreePanel()
    {
        UIManager.Instance.ToggleSkillTree();
        AudioManager.Instance.PlayUIButtonSFX();

        HideSkillLevelUpButton();
    }

    public void ToggleInventory()
    {
        UIManager.Instance.ToggleInventory();
        AudioManager.Instance.PlayInventorySFX();
    }
    
    public void ToggleEquipment()
    {
        UIManager.Instance.ToggleEquipment();
        AudioManager.Instance.PlayInventorySFX();

    }

    #region StatLevelUpButton
    public void HideStatLevelUpButton()
    {
        _statLevelUpButton.SetActive(false);
    }
    public void ShowStatLevelUpButton()
    {
        _statLevelUpButton.SetActive(true);
    }
    #endregion

    #region Skill Level Up Button
    public void HideSkillLevelUpButton()
    {
        _skillLevelUpButton.SetActive(false);
    }

    public void ShowSkillLevelUpButton()
    {
        _skillLevelUpButton.SetActive(true);
    }
    #endregion
}
