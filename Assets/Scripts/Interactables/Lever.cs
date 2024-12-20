using UnityEngine;

public class Lever : Interactable
{
    [SerializeField] private GameObject _connectedInteractable;

    public void Interact()
    {
        var unlockable = _connectedInteractable.GetComponent<IUnlockable>();

        if (unlockable.GetIsLocked)
        {
            unlockable.Unlock();
        }
        else
        {
            Debug.Log($"The Door is already unlocked!");
        }
    }
}
