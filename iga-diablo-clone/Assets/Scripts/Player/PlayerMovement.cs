using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void RunClickMovement()
    {
        MoveToLocation(MouseWorld.Instance.GetMousePosition());
    }

    public void MoveToLocation(Vector3 location)
    {
        _agent.ResetPath();
        _agent.destination = location;
    }
}
