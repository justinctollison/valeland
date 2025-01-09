using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

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
        OnHoverEnterEvent.AddListener(() => _healthBarUI.SetPortrait(GetComponent<BasicAI>().GetNPCData().portrait));
        OnHoverExitEvent.AddListener(() => _healthBarUI.HideHealthBar());
        OnTakeDamageEvent.AddListener(() => _healthBarUI.UpdateHealthBar(_currentHP, _maxHP));
    }

    protected virtual void LateUpdate() {
        if (_isAlive)
        {
            for(int i = 0; i< _statusEffects.Count; i++)
            {
                _statusEffects[i].RunStatusEffect();
            }
        }
    }
    protected virtual void Update() { }

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
        EffectsManager.Instance.PlayDamageIndicator(amount.ToString(), dmgIndicatorPos);
        AudioManager.Instance.PlayHitReactionSFX();
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
    public virtual bool CanBeClicked()
    {
        return true;
    }

    public virtual void OnClick() { }

    public virtual void OnHover() { }

    public virtual void OnHoverExit() { }
    #endregion

    #region Status Effects
    public void ApplyStatusEffect(string statusEffectName)
    {
        if(!_isAlive) { return; }
        if (HasStatusEffect(statusEffectName)) 
        { 
            GetStatusEffect(statusEffectName).RefreshDuration();
            return;
        }

        //todo: use a StatusEffect Manager instead of Resources.FindObjectsOfTypeAll
        GameObject statusEffectObject = StatusEffectManager.instance.GetStatusEffectPrefab(statusEffectName);
        statusEffectObject = Instantiate(statusEffectObject, transform);
        statusEffectObject.transform.position = transform.position + Vector3.up*height/2; //makes the particle effect appear at the center of the receiver
        StatusEffect statusEffect = statusEffectObject.GetComponent<StatusEffect>();

        _statusEffects.Add(statusEffect);
        statusEffect.receiver = this;
        statusEffect.OnApply();
    }

    public void RemoveStatusEffect(string statusEffectName)
    {
        StatusEffect statusEffect = GetStatusEffect(statusEffectName);
        RemoveStatusEffect(statusEffect);
    }
    public void RemoveStatusEffect(StatusEffect statusEffect)
    {
        if (!HasStatusEffect(statusEffect.effectName)) return;

        _statusEffects.Remove(statusEffect);
        statusEffect.OnRemoved();
    }

    public bool HasStatusEffect(string statusEffectName)
    {
        return _statusEffects.Exists(x => x.effectName == statusEffectName);
    }
    public StatusEffect GetStatusEffect(string statusEffectName)
    {
        return _statusEffects.Find(x => x.effectName == statusEffectName);
    }
    #endregion

    #region Knockback
    private IEnumerator KnockbackRoutine(Vector3 knockbackVector, float duration)
    {
        BasicAnimator animator = GetComponent<BasicAnimator>();
        if(animator != null)
            GetComponent<BasicAnimator>().StartCoroutine("PauseAnimation", duration);
        yield return new WaitForSeconds(0.05f);//hitstop? why not?
        for(float i = 0; i < duration; i += Time.deltaTime)
        {
            transform.position += knockbackVector * Time.deltaTime;
            yield return null;
        }
    }
    public void ReceiveKnockback(Vector3 knockbackVector, float duration = 0.8f)
    {
        StartCoroutine(KnockbackRoutine(knockbackVector, duration));
    }
    public void ReceiveKnockbackAwayFromPlayer(float knockbackForce, float duration = 0.8f)
    {
        Vector3 knockbackVector = GetDirectionAwayFromPlayer() * knockbackForce;
        StartCoroutine(KnockbackRoutine(knockbackVector,duration));
    }
    public Vector3 GetDirectionAwayFromPlayer()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;
        return (transform.position - playerPos).normalized;
    }

    #endregion
}