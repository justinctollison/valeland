using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterStatPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI strengthText;
    [SerializeField] TextMeshProUGUI dexterityText;
    [SerializeField] TextMeshProUGUI vitalityText;
    [SerializeField] TextMeshProUGUI energyText;

    [SerializeField] List<GameObject> spendButtons = new List<GameObject>();

    #region Listener Subscription
    private void Start()
    {
        EventsManager.Instance.onStatPointSpent.AddListener(UpdateCharacterSheetPanel);
        EventsManager.Instance.onPlayerLeveledUp.AddListener(UpdateCharacterSheetPanel);
    }
    private void OnDestroy()
    {
        EventsManager.Instance.onStatPointSpent.RemoveListener(UpdateCharacterSheetPanel);
        EventsManager.Instance.onPlayerLeveledUp.RemoveListener(UpdateCharacterSheetPanel);
    }
    #endregion

    private void OnEnable()
    {
        if (PlayerCharacterSheet.Instance == null) { return; }

        UpdateCharacterSheetPanel();
    }

    void UpdateCharacterSheetPanel()
    {
        if (PlayerCharacterSheet.Instance.GetStatPointsToSpend() == 0) { HideSpendButtons(); }
        else { ShowSpendButtons(); }

        UpdateStatDisplayText();
    }
    void UpdateStatDisplayText()
    {
        int displayInt = 15;

        // Update Strength Display
        displayInt = (int)PlayerCharacterSheet.Instance.GetStrength();
        strengthText.text = displayInt.ToString();

        // Update Dexterity Display
        displayInt = (int)PlayerCharacterSheet.Instance.GetDexterity();
        dexterityText.text = displayInt.ToString();

        // Update Vitality Display
        displayInt = (int)PlayerCharacterSheet.Instance.GetVitality();
        vitalityText.text = displayInt.ToString();

        // Update Energy Display
        displayInt = (int)PlayerCharacterSheet.Instance.GetEnergy();
        energyText.text = displayInt.ToString();
    }
    #region Spend Button Display
    void HideSpendButtons()
    {
        foreach (GameObject b in spendButtons)
        {
            b.SetActive(false);
        }
    }
    void ShowSpendButtons()
    {
        foreach (GameObject b in spendButtons)
        {
            b.SetActive(true);
        }
    }
    #endregion

    #region Spend Functions
    public void BuyStrength()
    {
        PlayerCharacterSheet.Instance.BuyStrengthPoint();
    }
    public void BuyDexterity()
    {
        PlayerCharacterSheet.Instance.BuyDexterityPoint();
    }
    public void BuyVitality()
    {
        PlayerCharacterSheet.Instance.BuyVitalityPoint();
    }
    public void BuyEnergy()
    {
        PlayerCharacterSheet.Instance.BuyEnergyPoint();
    }
    #endregion

    public void HideCharacterStatPanel()
    {
        UIManager.Instance.HideCharacterStatsPanel();
    }
}
