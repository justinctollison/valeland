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
    [Range(0, 100)]
    public float attackChanceToChangeAttackState; // The chance that the AI will change to a different attack state after an attack
    public int maxRepeatAttacks; // The maximum number of times the AI will repeat the same attack before changing to a different attack
    public GameObject AttackPrefab;
}
public enum AttackType { Melee, Projectile, AreaSpell, MultiProjectile };
