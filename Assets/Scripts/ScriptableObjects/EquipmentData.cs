using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "New Equipment")]
public class EquipmentData : ScriptableObject
{
    public ItemRarity itemRarity;
    public EquipmentType equipmentType;
    public WeaponType weaponType;
    public ArmorType armorType;
    public int durability;
    public int baseDamage;
    public int armorValue;
    public string equipmentDescription;
}

public enum EquipmentType { Head, Chest, Gloves, Legs, Boots, Shield, OffHand, Weapon };
