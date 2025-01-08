using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "New NPC")]
public class NPCData : ScriptableObject
{
    public FactionID factionID;
    public NPCType enemyType;
    public Sprite portrait;

    public float maxHealth;
    public float maxMana;

    public float attackRange;
    public float attackCooldown;

    public float minDamage;
    public float maxDamage;

    public float wanderDistance;
    public float engageDistance;

    public float experienceValue;

    public LootTable lootTable;

    //Navmesh Agent Data
    // TODO add in all Navmesh Agent data and change it on Start based on the different type of Enemy we are using

}

public enum FactionID { Good, Evil, Neutral };

public enum NPCType { Goblin, Skeleton, Phantom, Human };
