using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "New NPC")]
public class NPCData : ScriptableObject
{
    public enum EnemyType { Goblin, Skeleton, Phantom, Human };
    public EnemyType enemyType;

    public float maxHealth;
    public float maxMana;

    public float attackRange;
    public float attackCooldown;

    public float minDamage;
    public float maxDamage;

    public float wanderDistance;
    public float engageDistance;

    public float experienceValue;

    //Navmesh Agent Data
    // TODO add in all Navmesh Agent data and change it on Start based on the different type of Enemy we are using

}
