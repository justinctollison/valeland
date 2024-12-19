using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject _followTarget;
    [SerializeField] private Vector3 _offsetVector = new Vector3(5, 7, -4);

    void Update()
    {
        if (_followTarget != null)
        {
            Follow();
        }
    }

    public void SetFollowTarget(GameObject target)
    {
        _followTarget = target;
    }

    private void Follow()
    {
        transform.position = _followTarget.transform.position + _offsetVector;
        transform.LookAt(_followTarget.transform.position);
    }
}
