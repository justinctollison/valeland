using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private ItemData _currentItemData;
    [SerializeField] private EquipmentType _slotType;
    [SerializeField] private Image _icon;

    private GameObject _draggedItemImage;

    public UnityEvent<ItemData> onItemEquipped;
    public UnityEvent<ItemData> onItemUnequipped;

    public void EquipItem(ItemData itemData)
    {
        if (IsValidEquipType(itemData) && _currentItemData == null)
        {
            if (_currentItemData != null)
            {
                RemoveItem();
            }

            _currentItemData = itemData;
            _icon.sprite = itemData.itemIcon;
            _icon.gameObject.SetActive(true);

            onItemEquipped?.Invoke(itemData);

            switch (_slotType)
            {
                case EquipmentType.Head:
                    EquipmentCustomizer.Instance.EquipOpenHelmet(_currentItemData.equipmentData.ArmorID);
                    break;
                case EquipmentType.Shoulders:
                    EquipmentCustomizer.Instance.EquipShoulderArmor(_currentItemData.equipmentData.ArmorID);
                    break;
                case EquipmentType.Chest:
                    EquipmentCustomizer.Instance.EquipChestArmor(_currentItemData.equipmentData.ArmorID, 2);
                    break;
                case EquipmentType.Gloves:
                    EquipmentCustomizer.Instance.EquipGloves(_currentItemData.equipmentData.ArmorID);
                    break;
                case EquipmentType.Legs:
                    EquipmentCustomizer.Instance.EquipLegArmor(_currentItemData.equipmentData.ArmorID);
                    break;
                case EquipmentType.Boots:
                    EquipmentCustomizer.Instance.EquipBoots(_currentItemData.equipmentData.ArmorID);
                    break;
                case EquipmentType.Shield:
                    break;
                case EquipmentType.OffHand:
                    break;
                case EquipmentType.Weapon:
                    if (_currentItemData.name == PlayerController.Instance.GetComponentInChildren<Weapon>().name)
                        PlayerController.Instance.GetComponentInChildren<Weapon>().gameObject.GetComponent<MeshRenderer>().enabled = true;
                    break;
                case EquipmentType.Cape:
                    break;
                default:
                    break;
            }

            EventsManager.Instance.onEquipmentEquipped.Invoke();
            PlayerCharacterSheet.Instance.ApplyModifiers(_currentItemData.equipmentData.statModifiers);

            Debug.Log($"Equipped item: {_currentItemData.itemName} in {_slotType} slot.");
        }
        else
        {
            Debug.Log($"Invalid item type for this slot: {itemData.itemName} or Slot Occupied");
        }

        Debug.Log($"We got into Equip Item");
    }

    private bool IsValidEquipType(ItemData itemData)
    {
        if (itemData == null) { return false; }

        return itemData.equipmentData.equipmentType == _slotType;
    }

    public void RemoveItem()
    {
        if (_currentItemData != null)
        {
            _icon.sprite = null;
            _icon.gameObject.SetActive(false);
            onItemUnequipped?.Invoke(_currentItemData);

            Debug.Log($"Unequipped item from {_slotType} slot.");

            switch (_slotType)
            {
                case EquipmentType.Head:
                    EquipmentCustomizer.Instance.UnequipClosedHelmet();
                    EquipmentCustomizer.Instance.UnequipOpenHelmet();
                    EquipmentCustomizer.Instance.EnableHead();
                    break;
                case EquipmentType.Shoulders:
                    EquipmentCustomizer.Instance.UnequipShoulderArmor();
                    break;
                case EquipmentType.Chest:
                    EquipmentCustomizer.Instance.UnequipChestArmor();
                    break;
                case EquipmentType.Gloves:
                    EquipmentCustomizer.Instance.UnequipGloves();
                    break;
                case EquipmentType.Legs:
                    EquipmentCustomizer.Instance.UnequipLegArmor();
                    break;
                case EquipmentType.Boots:
                    EquipmentCustomizer.Instance.UnequipBoots();
                    break;
                case EquipmentType.Shield:
                    break;
                case EquipmentType.OffHand:
                    break;
                case EquipmentType.Weapon:
                    if (_currentItemData.name == PlayerController.Instance.GetComponentInChildren<Weapon>().name)
                        PlayerController.Instance.GetComponentInChildren<Weapon>().gameObject.GetComponent<MeshRenderer>().enabled = false;
                    break;
                case EquipmentType.Cape:
                    break;
                default:
                    break;
            }

            PlayerCharacterSheet.Instance.RemoveModifiers(_currentItemData.equipmentData.statModifiers);
            EventsManager.Instance.onEquipmentUnequipped.Invoke();

            _currentItemData = null;
        }
    }

    public void UnequipItem()
    {
        if (_currentItemData != null)
        {
            RemoveItem();
            Debug.Log($"Unequipped item: {_currentItemData.itemName} from {_slotType} slot.");
        }
    }

    #region Dragging Methods
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_currentItemData != null)
        {
            _draggedItemImage = new GameObject("DraggedItemImage");
            Image itemImage = _draggedItemImage.AddComponent<Image>();
            itemImage.sprite = _icon.sprite;
            itemImage.rectTransform.sizeDelta = _icon.rectTransform.sizeDelta;

            Canvas canvas = FindFirstObjectByType<Canvas>();
            _draggedItemImage.transform.SetParent(canvas.transform, false);
            _draggedItemImage.transform.position = Input.mousePosition;

            CanvasGroup canvasGroup = _draggedItemImage.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0.6f;

            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_draggedItemImage != null)
        {
            _draggedItemImage.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_draggedItemImage != null)
        {
            Destroy(_draggedItemImage);
        }

        if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<InventorySlot>() != null && !eventData.pointerEnter.GetComponent<InventorySlot>().GetSlotOccupied())
        {
            InventorySlot inventorySlot = eventData.pointerEnter.GetComponent<InventorySlot>();
            inventorySlot.Initialize(_currentItemData);
            InventoryManager.Instance._items.Add(_currentItemData);
            RemoveItem();
        }
    }
    #endregion

    public bool GetSlotOccupied() => _currentItemData;
}
