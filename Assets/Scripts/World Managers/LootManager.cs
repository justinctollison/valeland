using System.Collections.Generic;
using UnityEngine;

public static class LootManager
{
    public static List<ItemData> GenerateLoot(LootTable lootTable)
    {
        List<ItemData> droppedItems = new List<ItemData>();

        foreach (LootTableEntry entry in lootTable.lootEntries)
        {
            float roll = Random.Range(0f, 100f);

            if (roll <= entry.dropChance)
            {
                int quantity = Random.Range(entry.minQuantity, entry.maxQuantity + 1);

                for (int i = 0; i < quantity; i++)
                {
                    droppedItems.Add(entry.item);
                }
            }
        }

        return droppedItems;
    }
}
