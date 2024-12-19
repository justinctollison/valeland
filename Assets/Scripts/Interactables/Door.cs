using UnityEngine;

public class Door : MonoBehaviour, IUnlockable
{
    [SerializeField] private bool _isLocked;
    [SerializeField] private bool _requireKey;
    [SerializeField] private GameObject _door;
    [SerializeField] private bool _isOpen;

    public bool IsLocked => _isLocked;
    public bool RequireKey => _requireKey;

    private void Start()
    {
        _isLocked = true;
        _isOpen = false;
    }

    private void Update()
    {
        Toggle();
    }

    public void Close()
    {
        _door.transform.rotation = Quaternion.Euler(0, 0, 0);
        _isOpen = false;
        _isLocked = true;

        Debug.Log($"Door is now closing and locking!");
    }

    public void Open()
    {
        if (!_isLocked)
        {
            _door.transform.rotation = Quaternion.Euler(0, 90, 0);
            _isOpen = true;

            Debug.Log($"Door is now opening");
        }
        else
        {
            Debug.Log($"The Door is currently locked!");
        }
    }

    public void Unlock()
    {
        _isLocked = false;
        Debug.Log($"Door is unlocking!");
    }

    public void Toggle()
    {
        if (Input.GetKeyDown(KeyCode.O))
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
