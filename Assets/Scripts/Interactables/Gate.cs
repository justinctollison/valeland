using UnityEngine;

public class Gate : MonoBehaviour, IUnlockable
{
    [SerializeField] private bool _isLocked;
    [SerializeField] private bool _requireKey;
    [SerializeField] private bool _isOpen;
    [SerializeField] private GameObject _gate;

    SphereCollider _sphereCollider;

    private void Start()
    {
        _isOpen = false;

        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CombatReceiver>().GetFactionID() == FactionID.Good)
        {
            Toggle();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CombatReceiver>().GetFactionID() == FactionID.Good)
        {
            Toggle();
        }
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
        if (!_isOpen)
        {
            Open();
        }
        else
        {
            Close();
        }
    }


    public bool GetIsLocked => _isLocked;
    public bool GetRequireKey => _requireKey;
    public GameObject GetMovableObject => _gate;

}
