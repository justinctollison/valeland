using UnityEngine;

public class Item : MonoBehaviour
{
    //TODO: Droppable Items in the world

    public ItemData itemData;
    public int currentStackSize;

    private void Start()
    {
        currentStackSize = 1;
    }

    public void PickUpItem()
    {
        InventoryManager.Instance.AddItem(itemData);
        Destroy(gameObject); 
    }

    public void DropItem(Vector3 dropPosition)
    {
        // Instantiate the item at the specified position
        GameObject droppedItem = Instantiate(gameObject, dropPosition, Quaternion.identity);
        Item droppedItemComponent = droppedItem.GetComponent<Item>();

        // Transfer ItemData to the dropped item
        droppedItemComponent.itemData = this.itemData;
        droppedItemComponent.currentStackSize = this.currentStackSize;  // Set the correct stack size if needed
    }
}
