using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] public List<ItemData> _items = new List<ItemData>();

    private List<InventorySlot> _inventorySlots = new List<InventorySlot>();
    private Dictionary<EquipmentType, ItemData> _equipmentSlots = new Dictionary<EquipmentType, ItemData>();

    private int _maxInventorySlots = 18;

    public GameObject inventorySlotPrefab;
    public Transform inventoryGrid;

    public ItemData testItem;
    public ItemData testItem2;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeInventoryUI();
    }

    void Start()
    {
        if (testItem != null && testItem2 != null)
        {
            AddItem(testItem);
            AddItem(testItem2);
        }
    }

    private void InitializeInventoryUI()
    {
        foreach (var slot in _inventorySlots)
        {
            Destroy(slot.gameObject);
        }

        _inventorySlots.Clear();

        for (int i = 0; i < _maxInventorySlots; i++)
        {
            GameObject newSlot = Instantiate(inventorySlotPrefab, inventoryGrid);
            InventorySlot slot = newSlot.GetComponent<InventorySlot>();
            _inventorySlots.Add(slot);
            slot.Initialize(null);
        }
    }

    public void AddItem(ItemData newItem)
    {
        if (_items.Count < _maxInventorySlots)
        {
            _items.Add(newItem);

            for (int i = 0; i < _inventorySlots.Count; i++)
            {
                if (_inventorySlots[i].currentItemData == null)
                {
                    _inventorySlots[i].Initialize(newItem);
                    break; 
                }
            }
        }
        else
        {
            Debug.LogWarning("Inventory is full!");
        }
    }

    public void RemoveItem(ItemData itemToRemove)
    {
        //if (_items.Contains(itemToRemove))
        //{
            _items.Remove(itemToRemove);  // Remove from the inventory list
            Debug.Log($"Item removed from inventory: {itemToRemove.itemName}");

            // Loop through the inventory slots and clear the one that matches the item
            for (int i = 0; i < _inventorySlots.Count; i++)
            {
                if (_inventorySlots[i].currentItemData == itemToRemove)
                {
                    _inventorySlots[i].ClearSlot();  // Remove item from slot (clear the UI)
                    break;  // Exit the loop once we find the matching slot
                }
            }
        //}
    }

    public void UseItem(ItemData itemData)
    {
        if (itemData.itemType == ItemType.Consumable)
        {
            Debug.Log($"Using consumable item: {itemData.itemName}");
            // Logic for consumable items (e.g., healing, buffs, etc.)
            // Update the item's stack size, apply effects, or destroy the item if it's consumed
        }
        else if (itemData.itemType == ItemType.Weapon)
        {
            Debug.Log($"Equipping weapon: {itemData.itemName}");
            // Logic for equipping weapon
        }
        else if (itemData.itemType == ItemType.Armor)
        {
            Debug.Log($"Equipping armor: {itemData.itemName}");
            // Logic for equipping armor
        }
    }

    public void EquipItem(ItemData itemData, EquipmentType slotType)
    {
        if (_equipmentSlots.ContainsKey(slotType))
        {
            UnequipItem(slotType);
        }

        if (itemData.equipmentData.equipmentType == EquipmentType.Weapon)
        {
            EquipWeapon(itemData);
        }
        else
        {
            EquipArmor(itemData);
        }

        UpdateEquipmentSlotUI(slotType, itemData);
    }

    private void EquipWeapon(ItemData itemData)
    {
        _equipmentSlots[EquipmentType.Weapon] = itemData;
        Debug.Log($"Equipped {itemData.itemName} in Weapon slot.");
    }

    private void EquipArmor(ItemData itemData)
    {
        _equipmentSlots[itemData.equipmentData.equipmentType] = itemData;
        Debug.Log($"Equipped {itemData.itemName} in {itemData.equipmentData.equipmentType} slot.");
    }

    public void UnequipItem(EquipmentType slotType)
    {
        if (_equipmentSlots.ContainsKey(slotType))
        {
            ItemData removedItem = _equipmentSlots[slotType];
            _equipmentSlots.Remove(slotType);
            Debug.Log($"Unequipped {removedItem.itemName} from {slotType} slot.");
            UpdateEquipmentSlotUI(slotType, null);
        }
    }

    private void UpdateEquipmentSlotUI(EquipmentType slotType, ItemData itemData)
    {
        if (itemData != null)
        {
            Debug.Log($"Updating {slotType} slot with {itemData.itemName}.");
        }
        else
        {
            Debug.Log($"Clearing UI slot for {slotType}.");
        }
    }
}
