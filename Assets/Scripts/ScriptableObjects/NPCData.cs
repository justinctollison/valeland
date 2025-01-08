using UnityEngine;
using System.Collections.Generic;
using System;
[CreateAssetMenu(fileName = "NPC", menuName = "New NPC")]
public class NPCData : ScriptableObject
{
    public FactionID factionID;
    public NPCType enemyType;

    public float maxHealth;
    public float maxMana;

    public float wanderDistance;
    public float engageDistance;

    public float experienceValue;
    public List<EnemyAttack> attacks;

    //Navmesh Agent Data
    // TODO add in all Navmesh Agent data and change it on Start based on the different type of Enemy we are using

}

public enum FactionID { Good, Evil, Neutral };

public enum NPCType { Goblin, Skeleton, Phantom, Human };

[Serializable]
[CreateAssetMenu(fileName = "EnemyAttack", menuName = "New EnemyAttack")]
public class EnemyAttack : ScriptableObject
{ 
    public AttackType attackType;
    public float attackRange;
    public float attackCooldown;
    public float minDamage;
    public float maxDamage;

    public float maxTimeOutSeconds; // The time it takes for the attack to time out and a new attack to be chosen if left in the engage state for too long
    public GameObject AttackPrefab;
}
public enum AttackType { Melee, Projectile, AreaSpell };