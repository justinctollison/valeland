using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    public EquipmentData equipmentData;

    public string itemName;
    public ItemType itemType;
    public Sprite itemIcon;
    public string itemDescription;

    // TODO max stack size does nothing, need implementation and UI
    public int maxStackSize = 1;
}

public enum ItemType { Weapon, Armor, Consumable, Trinket, QuestItem };
public enum ItemRarity { Common, Uncommon, Rare, Epic, Unique };
