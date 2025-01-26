using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
public abstract class State
{
    protected Statemachine stateMachine;
    protected BasicAI basicAI;
    protected CombatReceiver combatReceiver;
    protected NavMeshAgent agent;
    protected NPCData data;

    protected FactionID factionID;
    protected bool isAlive;

    private System.Random random;

    public State(Statemachine stateMachine)
    {
        this.stateMachine = stateMachine;
        combatReceiver = stateMachine.GetComponent<CombatReceiver>();
        agent = stateMachine.GetComponent<NavMeshAgent>();
        basicAI = stateMachine.GetComponent<BasicAI>();

        data = basicAI.GetNPCData();
        factionID = stateMachine.GetComponent<CombatReceiver>().GetFactionID();
        isAlive = stateMachine.GetComponent<CombatReceiver>().GetIsAlive();

        random = new System.Random(); // Initialize the random number generator
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();

    protected void SetRandomActiveAttack()
    {
        stateMachine.activeAttack = data.attacks[random.Next(data.attacks.Count)];
    }
    protected void SetRandomDifferentAttack()
    {
        List<EnemyAttack> attacks = new List<EnemyAttack>(data.attacks);
        attacks.Remove(stateMachine.activeAttack);
        if(attacks.Count > 0)
            stateMachine.activeAttack = attacks[random.Next(data.attacks.Count)];
    }
    protected void SetRandomActiveAttackWithGreaterRange()
    {
        if (stateMachine.activeAttack == null)
        {
            SetRandomActiveAttack();
            return;
        }
        EnemyAttack newAttack = data.attacks.Find(attack => attack.attackRange > stateMachine.activeAttack.attackRange);
        if (newAttack != null)
        {
            stateMachine.activeAttack = newAttack;
        }
        else
        {
            SetRandomActiveAttack();
        }
    }
}
