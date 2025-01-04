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
}
