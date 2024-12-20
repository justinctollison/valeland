using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _playerHUD;
    [SerializeField] GameObject _characterStatPanel;
    [SerializeField] GameObject _skillTree;
    [SerializeField] GameObject _inventoryMenu;
    [SerializeField] GameObject _equipmentMenu;
    [SerializeField] GameObject _dialogueMenu;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        // HideAll();

        HideEquipment();
        HideInventory();
        HideCharacterStatsPanel();
        HideSkillTree();
    }

    public void HideAll()
    {
        // Hide everything
        HidePlayerHUD();
        HideCharacterStatsPanel();
        HideSkillTree();
        HideInventory();
        HideEquipment();
    }

    #region PlayerHUD
    public void ShowPlayerHUD()
    {
        _playerHUD.SetActive(true);
    }
    public void HidePlayerHUD()
    {
        _playerHUD.SetActive(false);
    }
    #endregion

    #region Character Stats Panel
    public void ShowCharacterStatsPanel()
    {
        _characterStatPanel.SetActive(true);
    }

    public void HideCharacterStatsPanel()
    {
        _characterStatPanel.SetActive(false);
    }

    public void ToggleCharacterStatsPanel()
    {
        if (_characterStatPanel.activeInHierarchy)
        {
            HideCharacterStatsPanel();
        }
        else
        {
            ShowCharacterStatsPanel();
        }
    }
    #endregion

    #region Skill Tree
    public void ShowSkillTree()
    {
        _skillTree.SetActive(true);
    }

    public void HideSkillTree()
    {
        _skillTree?.SetActive(false);
    }

    public void ToggleSkillTree()
    {
        if (_skillTree.activeInHierarchy)
        {
            HideSkillTree();
        }
        else
        {
            ShowSkillTree();
        }
    }
    #endregion

    #region Dialogue Menu
    public void ShowDialogueMenu()
    {
        _dialogueMenu.SetActive(true);
    }

    public void HideDialogueMenu()
    {
        _dialogueMenu?.SetActive(false);
    }

    public void ToggleDialogueMenu()
    {
        if (_dialogueMenu.activeInHierarchy)
        {
            HideDialogueMenu();
        }
        else
        {
            ShowDialogueMenu();
        }
    }
    #endregion

    #region Inventory Menu
    public void ShowInventory()
    {
        _inventoryMenu.SetActive(true);
    }

    public void HideInventory()
    {
        _inventoryMenu?.SetActive(false);
    }

    public void ToggleInventory()
    {
        if (_inventoryMenu.activeInHierarchy)
        {
            HideInventory();
        }
        else
        {
            ShowInventory();
        }
    }
    #endregion

    #region Equipment Menu
    public void ShowEquipment()
    {
        _equipmentMenu.SetActive(true);
    }

    public void HideEquipment()
    {
        _equipmentMenu?.SetActive(false);
    }

    public void ToggleEquipment()
    {
        if (_equipmentMenu.activeInHierarchy)
        {
            HideEquipment();
        }
        else
        {
            ShowEquipment();
        }
    }
    #endregion
}
