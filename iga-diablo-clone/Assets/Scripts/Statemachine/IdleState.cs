using UnityEngine;

public class IdleState : State
{
    public IdleState(Statemachine stateMachine) : base(stateMachine) { }

    float maxWanderDistance;
    Vector3 startPosition;

    public override void Enter()
    {
        startPosition = stateMachine.transform.position;
        maxWanderDistance = data.wanderDistance;

        GetNewWanderDestination();
    }

    public override void Execute()
    {
        RunWandering();
    }

    public override void Exit()
    {
    }

    private void RunWandering()
    {
        float x = agent.destination.x;
        float y = stateMachine.transform.position.y;
        float z = agent.destination.z;

        Vector3 checkPosition = new Vector3(x, y, z);

        if (Vector3.Distance(stateMachine.transform.position, checkPosition) < 1)
        {
            GetNewWanderDestination();
        }
    }

    private void GetNewWanderDestination()
    {
        float randomX = Random.Range(-maxWanderDistance, +maxWanderDistance);
        float randomZ = Random.Range(-maxWanderDistance, +maxWanderDistance);

        float x = randomX + startPosition.x;
        float y = startPosition.y;
        float z = randomZ + startPosition.z;

        agent.destination = new Vector3(x, y, z);
    }
}
