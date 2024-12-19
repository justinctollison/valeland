using TMPro;
using UnityEngine;

public class InteractableUIManager : MonoBehaviour
{
    public static InteractableUIManager Instance;

    [SerializeField] private GameObject _popupPanel;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void ShowPopup(string title, string description)
    {
        _popupPanel.SetActive(true);

        _titleText.text = title;
        _descriptionText.text = description;
    }

    public void HidePopup()
    {
        _popupPanel.SetActive(false);
    }
}
