using UnityEngine;

[System.Serializable]
public class LootTableEntry
{
    public ItemData item;
    [Range(0f, 100f)] public float dropChance;
    public int minQuantity = 1;
    public int maxQuantity = 1;
}