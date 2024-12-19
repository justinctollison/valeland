using UnityEngine;
using UnityEngine.Events;

public class CombatReceiver : MonoBehaviour, IClickable
{
    public UnityEvent OnHoverEnterEvent;
    public UnityEvent OnHoverExitEvent;
    public UnityEvent OnTakeDamageEvent;

    private EnemyHealthBarUI _healthBarUI;

    protected int _factionID = 0;

    [SerializeField] protected float _currentHP;
    [SerializeField] protected float _currentMana;

    protected float _maxHP;
    protected float _maxMana;
    protected bool _isAlive = true;

    protected virtual void Awake()
    {
        SetLayer(LayerMask.NameToLayer("Clickable"));
    }

    protected virtual void Start()
    {
        _healthBarUI = EnemyHealthBarUI.Instance.GetComponent<EnemyHealthBarUI>();
        _currentHP = _maxHP;

        OnHoverEnterEvent.AddListener(() => _healthBarUI.ShowHealthBar(_currentHP, _maxHP));
        OnHoverEnterEvent.AddListener(() => _healthBarUI.SetNameText(GetComponent<Statemachine>().GetNPCData().name));
        OnHoverExitEvent.AddListener(() => _healthBarUI.HideHealthBar());
        OnTakeDamageEvent.AddListener(() => _healthBarUI.UpdateHealthBar(_currentHP, _maxHP));
    }

    public virtual void Die()
    {
        _isAlive = false;

        OnHoverEnterEvent.RemoveListener(() => _healthBarUI.ShowHealthBar(_currentHP, _maxHP));
        OnHoverEnterEvent.RemoveListener(() => _healthBarUI.SetNameText(GetComponent<Statemachine>().GetNPCData().name));
        OnHoverExitEvent.RemoveListener(() => _healthBarUI.HideHealthBar());
        OnTakeDamageEvent.RemoveListener(() => _healthBarUI.UpdateHealthBar(_currentHP, _maxHP));
    }

    public void SetFactionID(int newID)
    {
        _factionID = newID;
    }

    public int GetFactionID() => _factionID;

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

    public void SetLayer(int layer)
    {
        gameObject.layer = layer;
    }

    public int GetLayer()
    {
        return gameObject.layer;
    }

    public bool GetIsAlive()
    {
        return _isAlive;
    }

    public bool CanBeClicked()
    {
        return true;
    }

    public virtual void OnClick()
    {
        
    }

    public virtual void OnHover()
    {

    }

    public virtual void OnHoverExit()
    {

    }
}
