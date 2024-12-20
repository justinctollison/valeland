using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCHealthBarUI : MonoBehaviour
{
    public static NPCHealthBarUI Instance;

    [SerializeField] private GameObject _healthBarUI;
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _healthFill;

    public void Awake()
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

    public void Start()
    {
        HideHealthBar();
    }

    public void ShowHealthBar(float health, float maxHealth)
    {
        _healthBarUI.SetActive(true);
        _healthBarSlider.maxValue = maxHealth;
        _healthBarSlider.value = health;
    }

    public void UpdateHealthBar(float health, float maxHealth)
    {
        _healthBarSlider.maxValue = maxHealth;
        _healthBarSlider.value = health;
    }

    public void HideHealthBar()
    {
        _healthBarUI.SetActive(false);
    }

    public void SetNameText(string name)
    {
        _nameText.text = name;
    }

    public void SetHealthBarColor(Color color)
    {
        _healthFill.color = color;
    }
}
