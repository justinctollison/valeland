using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatReceiver : MonoBehaviour, IClickable
{
    [HideInInspector] public UnityEvent OnHoverEnterEvent;
    [HideInInspector] public UnityEvent OnHoverExitEvent;
    [HideInInspector] public UnityEvent OnTakeDamageEvent;

    protected NPCHealthBarUI _healthBarUI;

    protected FactionID _factionID = 0;

    [SerializeField] protected float _currentHP;
    [SerializeField] protected float _currentMana;

    protected float _maxHP;
    protected float _maxMana;
    protected bool _isAlive = true;

    protected List<StatusEffect> _statusEffects = new List<StatusEffect>();
    public float height = 1.2f;

    protected virtual void Awake() { }

    protected virtual void Start()
    {
        _healthBarUI = NPCHealthBarUI.Instance.GetComponent<NPCHealthBarUI>();
        _currentHP = _maxHP;
        _currentMana = _maxMana;

        OnHoverEnterEvent.AddListener(() => _healthBarUI.ShowHealthBar(_currentHP, _maxHP));
        OnHoverEnterEvent.AddListener(() => _healthBarUI.SetNameText(GetComponent<BasicAI>().GetNPCData().name));
        OnHoverExitEvent.AddListener(() => _healthBarUI.HideHealthBar());
        OnTakeDamageEvent.AddListener(() => _healthBarUI.UpdateHealthBar(_currentHP, _maxHP));
    }

    protected virtual void Update() {
        if (_isAlive)
        {
            foreach(StatusEffect status in _statusEffects)
            {
                status.RunStatusEffect();
            }
        }
    }

    public virtual void Die()
    {
        _isAlive = false;

        OnHoverEnterEvent.RemoveListener(() => _healthBarUI.ShowHealthBar(_currentHP, _maxHP));
        OnHoverEnterEvent.RemoveListener(() => _healthBarUI.SetNameText(GetComponent<BasicAI>().GetNPCData().name));
        OnHoverExitEvent.RemoveListener(() => _healthBarUI.HideHealthBar());
        OnTakeDamageEvent.RemoveListener(() => _healthBarUI.UpdateHealthBar(_currentHP, _maxHP));
    }

    public void SetFactionID(FactionID newID)
    {
        _factionID = newID;
    }

    public FactionID GetFactionID() => _factionID;

    public virtual void TakeDamage(float amount)
    {
        if (!_isAlive) { return; }

        _currentHP -= amount;
        Vector3 dmgIndicatorPos = transform.position + new Vector3(0, height, 0);
        EffectsManager.Instance.PlayDamageIndicator(amount, dmgIndicatorPos);
        if (_currentHP <= 0)
        {
            Die();
        }
    }

    public virtual void Revive()
    {

    }

    public bool GetIsAlive()
    {
        return _isAlive;
    }

    #region Clickable Functions
    public bool CanBeClicked()
    {
        return true;
    }

    public virtual void OnClick() { }

    public virtual void OnHover() { }

    public virtual void OnHoverExit() { }
    #endregion

    #region Status Effects
    public void ApplyStatusEffect<T>() where T : StatusEffect
    {
        if(!_isAlive) { return; }
        if (HasStatusEffect<T>()) { GetStatusEffect<T>().RefreshDuration(); }

        //todo: use a StatusEffect Manager instead of Resources.FindObjectsOfTypeAll
        print(Resources.FindObjectsOfTypeAll<T>().Length);
        GameObject statusEffectObject = Resources.FindObjectsOfTypeAll<T>()[0].gameObject;
        statusEffectObject = Instantiate(statusEffectObject, transform);
        T statusEffect = statusEffectObject.GetComponent<T>();

        _statusEffects.Add(statusEffect);
        statusEffect.receiver = this;
        statusEffect.OnApply();
    }

    public void RemoveStatusEffect<T>() where T : StatusEffect
    {
        StatusEffect statusEffect = GetStatusEffect<T>();
        RemoveStatusEffect(statusEffect);
    }
    public void RemoveStatusEffect(StatusEffect statusEffect)
    {
        _statusEffects.Remove(statusEffect);
        statusEffect.OnRemoved();
    }

    public bool HasStatusEffect<T>() where T : StatusEffect
    {
        return _statusEffects.Exists(x => x.GetType() == typeof(T));
    }

    private StatusEffect GetStatusEffect<T>() where T: StatusEffect
    {
        if(!HasStatusEffect<T>()) 
            throw new ArgumentException("Attempted to Get a StatusEffect not attached to CombatReceiver");
        return _statusEffects.Find(x => x.GetType() == typeof(T)) as T;
    }
    #endregion
}