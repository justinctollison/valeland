using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "EnemyAttack", menuName = "New EnemyAttack")]
public class EnemyAttack : ScriptableObject
{
    public AttackType attackType;
    public float attackRange;
    public float attackCooldown;
    public float minDamage;
    public float maxDamage;
    public float windUpTime;
    public string animatorTriggerAttackName;
    public string animatorTriggerWindUpName;

    public float maxTimeOutSeconds; // The time it takes for the attack to time out and a new attack to be chosen if left in the engage state for too long
    public GameObject AttackPrefab;
}
public enum AttackType { Melee, Projectile, AreaSpell };
