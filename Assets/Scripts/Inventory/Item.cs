using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    public int currentStackSize;

    private void Start()
    {
        currentStackSize = 1;
    }

    public void PickUpItem()
    {
        // Handle what happens when the player picks up the item
        InventoryManager.Instance.AddItem(itemData);  // Example: Add item to inventory (adjust depending on your setup)
        Destroy(gameObject);  // Destroy the item from the world once picked up
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
