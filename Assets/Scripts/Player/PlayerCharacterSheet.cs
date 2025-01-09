using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterSheet : MonoBehaviour
{
    public static PlayerCharacterSheet Instance;

    private Dictionary<string, float> baseStats;
    private Dictionary<string, float> modifiedStats;

    private int _level = 1;
    private float _experience = 0;

    private int _statPointsToSpend;
    private int _skillPointsToSpend;

    // Character Stats
    [SerializeField] private float _strength = 15;
    [SerializeField] private float _dexterity = 15;
    [SerializeField] private float _vitality = 15;
    [SerializeField] private float _energy = 15;

    private float _currentHitPoints = 35;
    private float _maxHitpoints = 35;
    private float _currentMana = 35;
    private float _maxMana = 35;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        EventsManager.Instance.onExperienceGranted.AddListener(AddExperience);

        baseStats = new Dictionary<string, float> 
        {
            {"Strength", _strength},
            {"Dexterity", _dexterity},
            {"Vitality", _vitality},
            {"Energy", _energy},
        };

        modifiedStats = new Dictionary<string, float>(baseStats);
    }

    public void ApplyModifiers(List<StatModifier> modifiers)
    {
        foreach (var modifier in modifiers)
        {
            if (modifiedStats.ContainsKey(modifier.statName))
                modifiedStats[modifier.statName] += modifier.value;
            else
                Debug.LogWarning($"Stat {modifier.statName} does not exist.");
        }
        UpdateStats();
    }

    public void RemoveModifiers(List<StatModifier> modifiers)
    {
        foreach (var modifier in modifiers)
        {
            if (modifiedStats.ContainsKey(modifier.statName))
                modifiedStats[modifier.statName] -= modifier.value;
        }
        UpdateStats();
    }

    private void UpdateStats()
    {
        _strength = modifiedStats["Strength"];
        _dexterity = modifiedStats["Dexterity"];
        _vitality = modifiedStats["Vitality"];
        _energy = modifiedStats["Energy"];

        Debug.Log($"Stats Updated: Strength = {_strength}, Dexterity = {_dexterity}, Vitality = {_vitality}, Energy = {_energy}");
    }

    private void OnDestroy()
    {
        EventsManager.Instance.onExperienceGranted.RemoveListener(AddExperience);
    }

    #region Levels and Experience
    public void AddExperience(float amount)
    {
        _experience += amount;

        if (_experience >= GetExperienceToNextLevel())
        {
            LevelUp();
        }

       EventsManager.Instance.onExperienceUpdated.Invoke(_experience / GetExperienceToNextLevel());
    }

    public void LevelUp()
    {
        _experience -= GetExperienceToNextLevel();
        _level++;

        _skillPointsToSpend += 1;
        _statPointsToSpend += 5;
        EventsManager.Instance.onPlayerLeveledUp.Invoke();
    }

    public int GetLevel() => _level;
    public float GetExperience() => _experience;
    public float GetExperienceToNextLevel() => (100 * _level);
    #endregion

    #region Stats
    public float GetStrength() => _strength;
    public float GetDexterity() => _dexterity;
    public float GetVitality() => _vitality;
    public float GetEnergy() => _energy;
    #endregion

    #region HP and Mana
    public float GetMaxHP()
    {
        return (5 + (3 * _vitality));
    }
    public float GetMaxMana()
    {
        return (5 + (3 * _energy));
    }
    #endregion

    #region Stat and Skill Getters
    public int GetStatPointsToSpend() => _statPointsToSpend;
    public int GetSkillPointsToSpend() => _skillPointsToSpend;
    #endregion

    #region Stat Point Spending
    private bool PointSpendSuccessful()
    {
        if (_statPointsToSpend <= 0) { return false; }
        else
        {
            _statPointsToSpend--;
            AudioManager.Instance.PlayUIButtonSFX();
            return true;
        }
    }

    public void BuyStrengthPoint()
    {
        if (PointSpendSuccessful())
        {
            _strength++;
            EventsManager.Instance.onStatPointSpent.Invoke();
            AudioManager.Instance.PlayUIButtonSFX();
        }
    }
    
    public void BuyDexterityPoint()
    {
        if (PointSpendSuccessful())
        {
            _dexterity++;
            EventsManager.Instance.onStatPointSpent.Invoke();
            AudioManager.Instance.PlayUIButtonSFX();

        }
    }
    
    public void BuyVitalityPoint()
    {
        if (PointSpendSuccessful())
        {
            _vitality++;
            EventsManager.Instance.onStatPointSpent.Invoke();
            AudioManager.Instance.PlayUIButtonSFX();

        }
    }
    
    public void BuyEnergyPoint()
    {
        if (PointSpendSuccessful())
        {
            _energy++;
            EventsManager.Instance.onStatPointSpent.Invoke();
            AudioManager.Instance.PlayUIButtonSFX();
        }
    }
    #endregion

    public void HideCharacterStatPanel()
    {
        UIManager.Instance.HideCharacterStatsPanel();
    }

    public bool SkillPointSpendSuccessful()
    {
        if (_skillPointsToSpend <= 0)
        {
            return false;
        }
        else
        {
            _skillPointsToSpend--;
            return true;
        }
    }
}
