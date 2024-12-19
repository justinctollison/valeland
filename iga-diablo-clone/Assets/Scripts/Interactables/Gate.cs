using UnityEngine;

public class Gate : MonoBehaviour, IUnlockable
{
    [SerializeField] private bool _isLocked;
    [SerializeField] private bool _requireKey;
    [SerializeField] private bool _isOpen;
    [SerializeField] private GameObject _gate;

    public bool IsLocked => _isLocked;
    public bool RequireKey => _requireKey;
    public GameObject MovableObject => _gate;

    private void Start()
    {
        _isOpen = false;
    }

    private void Update()
    {
        Toggle();
    }

    public void Close()
    {
        _gate.transform.position = new Vector3(_gate.transform.position.x, 0, _gate.transform.position.z);
        _isOpen = false;

        Debug.Log($"Gate is now Closing!");
    }

    public void Open()
    {
        if (!_isLocked)
        {
            _gate.transform.position = new Vector3(_gate.transform.position.x, 2f, _gate.transform.position.z);
            _isOpen = true;

            Debug.Log($"Gate is now Opening!");
        }
        else
        {
            Debug.Log($"The Gate is currently locked!");
        }
    }

    public void Unlock()
    {
        _isLocked = false;
        Debug.Log($"Gate is unlocking!");
    }

    public void Toggle()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!_isOpen)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
    }
}
