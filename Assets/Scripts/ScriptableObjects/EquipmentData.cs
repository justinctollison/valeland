using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Equipment", menuName = "New Equipment")]
public class EquipmentData : ScriptableObject
{
    public ItemRarity itemRarity;
    public EquipmentType equipmentType;
    public WeaponType weaponType;

    public int ArmorID;
    public int durability;
    public int baseDamage;
    public int armorValue;


    public string description;

    public List<StatModifier> statModifiers;
}

[System.Serializable]
public class StatModifier
{
    public string statName;
    public float value;
}

public enum EquipmentType { Head, Shoulders, Chest, Gloves, Legs, Boots, Shield, OffHand, Weapon };
public enum WeaponType { None, Sword, Bow, Axe, Staff, Dagger, Mace, OffHand };

