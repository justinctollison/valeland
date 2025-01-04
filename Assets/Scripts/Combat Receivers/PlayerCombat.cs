using UnityEngine;

public class PlayerCombat : CombatReceiver
{
    // Regen Variables
    [SerializeField] protected float _healthRegenBase = 0.5f;
    protected float _healthRegenMod = 1f;
    [SerializeField] protected float _manaRegenBase = 0.5f;
    protected float _manaRegenMod = 1f;
    protected float _regenUpdateTickTimer = 0;
    protected float _regenUpdateTickTime = 2f;

    protected override void Start()
    {
        _maxHP = 35f;
        _maxMana = 100f;

        _factionID = GetComponent<PlayerController>().GetFactionID();

        EventsManager.Instance.onPlayerLeveledUp.AddListener(LevelUp);
        EventsManager.Instance.onStatPointSpent.AddListener(StatsChangedAdjustment);

        base.Start();
        Debug.Log($"Our faction for Player is {_factionID}");
    }

    protected override void Update()
    {
        if (_isAlive)
        {
            RunRegen();
        }
    }

    private void OnDestroy()
    {
        EventsManager.Instance.onPlayerLeveledUp.RemoveListener(LevelUp);
        EventsManager.Instance.onStatPointSpent.RemoveListener(StatsChangedAdjustment);
    }

    public override void Die()
    {
        base.Die();

        GetComponent<PlayerController>().TriggerDeath();
        EventsManager.Instance.onPlayerDied?.Invoke();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        EventsManager.Instance.onHealthChanged.Invoke(_currentHP / _maxHP);
    }

    public override bool CanBeClicked()
    {
        return false;
    }

    #region Mana Management
    public float GetMana() => _currentMana;
    public void SpendMana(float amount)
    {
        _currentMana -= amount;
        EventsManager.Instance.onManaChanged.Invoke(_currentMana / _maxMana);
    }
    #endregion

    #region Level up Events
    private void LevelUp()
    {
        _currentHP = _maxHP;
        _currentMana = _maxMana;

        EventsManager.Instance.onManaChanged.Invoke(_currentMana / _maxMana);
        EventsManager.Instance.onHealthChanged.Invoke(_currentHP / _maxHP);
    }

    private void StatsChangedAdjustment()
    {
        UpdateBaseRegen();

        float oldMaxHP = _maxHP;
        float oldMaxMana = _maxMana;

        _maxHP = PlayerCharacterSheet.Instance.GetMaxHP();
        _maxMana = PlayerCharacterSheet.Instance.GetMaxMana();

        _currentHP += _maxHP - oldMaxHP;
        _currentMana += _maxMana - oldMaxMana;
        EventsManager.Instance.onManaChanged.Invoke(_currentMana / _maxMana);
        EventsManager.Instance.onHealthChanged.Invoke(_currentHP / _maxHP);
    }
    #endregion

    #region Regen Stats
    protected void RunRegen()
    {
        _currentHP += (_healthRegenBase * _healthRegenMod * Time.deltaTime);
        if (_currentHP > _maxHP) { _currentHP = _maxHP; }

        _currentMana += (_manaRegenBase * _manaRegenMod * Time.deltaTime);
        if (_currentMana > _maxMana) { _currentMana = _maxMana; }

        _regenUpdateTickTimer += Time.deltaTime;
        if (_regenUpdateTickTimer >= _regenUpdateTickTime)
        {
            _regenUpdateTickTimer -= _regenUpdateTickTime;
            EventsManager.Instance.onHealthChanged.Invoke(_currentHP / _maxHP);
            EventsManager.Instance.onManaChanged.Invoke(_currentMana / _maxMana);
        }
    }

    public void SetHPRegenMod(float newMod)
    {
        _healthRegenMod = newMod;
    }

    public void SetManaRegenMod(float newMod)
    {
        _manaRegenMod = newMod;
    }

    protected void UpdateBaseRegen()
    {
        _healthRegenBase = 0.5f + (.01f * PlayerCharacterSheet.Instance.GetVitality());
        _manaRegenBase = 0.5f + (0.1f * PlayerCharacterSheet.Instance.GetEnergy());
    }
    #endregion
}
