using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LootTable", menuName = "Loot/LootTable")]
public class LootTable : ScriptableObject
{
    public List<LootTableEntry> lootEntries = new List<LootTableEntry>();
}
