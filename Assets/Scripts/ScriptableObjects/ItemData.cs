using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    public EquipmentData equipmentData;

    public string itemName;
    public ItemType itemType;
    public Sprite itemIcon;
    public string itemDescription;

    public int maxStackSize = 1;
}

public enum ItemType { Weapon, Armor, Consumable, Trinket, QuestItem };
public enum ArmorType { Head, Chest, Gloves, Legs, Boots, Shield };
public enum WeaponType { Sword, Bow, Axe, Staff, Dagger, Mace, OffHand };
public enum ItemRarity { Common, Uncommon, Rare, Epic, Unique };
