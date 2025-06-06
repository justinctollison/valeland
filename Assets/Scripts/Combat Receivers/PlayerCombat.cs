using UnityEngine;

public class PlayerCombat : CombatReceiver
{
    // Regen Variables
    [SerializeField] protected float _healthRegenBase = 0.5f;
    protected float _healthRegenMod = 1f;
    [SerializeField] protected float _manaRegenBase = 0.5f;
    protected float _manaRegenMod = 1.2f;
    protected float _regenUpdateTickTimer = 0;
    protected float _regenUpdateTickTime = 2f;

    public bool atFullHP;
    public bool atFullMana;

    protected override void Start()
    {
        _maxHP = PlayerCharacterSheet.Instance.GetMaxHP();
        _maxMana = PlayerCharacterSheet.Instance.GetMaxMana();


        _factionID = GetComponent<PlayerController>().GetFactionID();

        EventsManager.Instance.onPlayerLeveledUp.AddListener(LevelUp);
        EventsManager.Instance.onStatPointSpent.AddListener(StatsChangedAdjustment);
        EventsManager.Instance.onPlayerRevived.AddListener(FullHeal);

        base.Start();
        Debug.Log($"Our faction for Player is {_factionID}");
    }

    protected override void Update()
    {
        base.Update();

        if (_isAlive)
        {
            RunRegen();
        }

        FullHealthAndFullManaCheck();
    }

    private void OnDestroy()
    {
        EventsManager.Instance.onPlayerLeveledUp.RemoveListener(LevelUp);
        EventsManager.Instance.onStatPointSpent.RemoveListener(StatsChangedAdjustment);
        EventsManager.Instance.onPlayerRevived.RemoveListener(FullHeal);
    }

    public override void Die()
    {
        base.Die();

        GetComponent<PlayerController>().TriggerDeath();
    }
    public override void Revive()
    {
        base.Revive();

        GetComponent<PlayerController>().TriggerRevive();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        EventsManager.Instance.onHealthChanged.Invoke(_currentHP / _maxHP);
    }

    // TODO: Refactor this, it's hard-coded with a magic-number
    public void TakeHealthPotion()
    {
        if (!atFullHP)
        {
            _currentHP += 25f;
            AudioManager.Instance.PlayHealthRestoredSFX();
        }
    }

    public void TakeManaPotion()
    {
        if (!atFullMana)
        {
            _currentMana += 25f;
            AudioManager.Instance.PlayManaRestoredSFX();
        }
    }

    public override bool CanBeClicked()
    {
        return false;
    }

    private void FullHealthAndFullManaCheck()
    {
        if (_currentHP == _maxHP)
        {
            atFullHP = true;
        }
        else if (_currentHP < _maxHP)
        {
            atFullHP = false;
        }

        if (_currentMana == _maxMana)
        {
            atFullMana = true;
        }
        else if (_currentMana < _maxMana)
        {
            atFullMana = false;
        }
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

    protected override void FullHeal()
    {
        base.FullHeal();
        EventsManager.Instance.onHealthChanged.Invoke(_currentHP / _maxHP);
        EventsManager.Instance.onManaChanged.Invoke(_currentMana / _maxMana);
    }
}
