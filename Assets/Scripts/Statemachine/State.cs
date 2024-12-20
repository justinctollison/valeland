using UnityEngine;
using UnityEngine.AI;

public abstract class State
{
    protected Statemachine stateMachine;
    protected BasicAI basicAI;
    protected CombatReceiver combatReceiver;
    protected NavMeshAgent agent;
    protected NPCData data;

    protected FactionID factionID;
    protected bool isAlive;

    public State(Statemachine stateMachine)
    {
        this.stateMachine = stateMachine;
        combatReceiver = stateMachine.GetComponent<CombatReceiver>();
        agent = stateMachine.GetComponent<NavMeshAgent>();
        basicAI = stateMachine.GetComponent<BasicAI>();

        data = basicAI.GetNPCData();
        factionID = stateMachine.GetComponent<CombatReceiver>().GetFactionID();
        isAlive = stateMachine.GetComponent<CombatReceiver>().GetIsAlive();
    }

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
}
