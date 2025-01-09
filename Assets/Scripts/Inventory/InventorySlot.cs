using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup _canvasGroup;
    private GameObject _draggedItemImage;

    public Image itemIcon;
    public Button slotButton;
    public ItemData currentItemData;

    public void Initialize(ItemData itemData)
    {
        currentItemData = itemData;

        if (itemData != null)
        {
            itemIcon.sprite = itemData.itemIcon;
            itemIcon.gameObject.SetActive(true);
        }
        else
        {
            itemIcon.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
        slotButton.onClick.AddListener(OnSlotClick);
    }

    #region Dragging Methods
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentItemData != null)
        {
            _canvasGroup.alpha = 0.6f;
            _canvasGroup.blocksRaycasts = false;

            _draggedItemImage = new GameObject("DraggedItemImage");
            Image itemImage = _draggedItemImage.AddComponent<Image>();
            itemImage.sprite = itemIcon.sprite;
            itemImage.rectTransform.sizeDelta = itemIcon.rectTransform.sizeDelta;

            itemImage.raycastTarget = false;

            Canvas canvas = FindFirstObjectByType<Canvas>();
            _draggedItemImage.transform.SetParent(canvas.transform, false);

            _draggedItemImage.transform.position = Input.mousePosition;
            Debug.Log(_draggedItemImage.transform.parent);
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
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        if (_draggedItemImage != null)
        {
            Destroy(_draggedItemImage);
        }

        if (eventData.pointerEnter != null)
        {
            EquipmentSlot equipmentSlot = eventData.pointerEnter.GetComponent<EquipmentSlot>();

            //TODO Fix bug where equipment is being removed from Inventory but not equipped
            if (equipmentSlot != null && !equipmentSlot.GetSlotOccupied())
            {
                equipmentSlot.EquipItem(currentItemData);
                Debug.Log($"We're equipping the item, removing it from inventory and clearing the slot. Our Current Item data is {currentItemData}.");
                InventoryManager.Instance.RemoveItem(currentItemData);
                ClearSlot();
            }
            else if (eventData.pointerEnter.GetComponent<InventorySlot>() != null && !eventData.pointerEnter.GetComponent<InventorySlot>().GetSlotOccupied())
            {
                InventorySlot inventorySlot = eventData.pointerEnter.GetComponent<InventorySlot>();
                inventorySlot.Initialize(currentItemData);
                ClearSlot();
            }

            Debug.Log($"Slot is occupied.");
        }

        Debug.Log($"Calling on Drag End");
    }
    #endregion

    public void ClearSlot()
    {
        currentItemData = null;
        itemIcon.gameObject.SetActive(false);
    }

    private void OnSlotClick()
    {
        if (currentItemData != null)
        {
            InventoryManager.Instance.UseItem(currentItemData);
        }
    }

    public bool GetSlotOccupied() => currentItemData;
}
