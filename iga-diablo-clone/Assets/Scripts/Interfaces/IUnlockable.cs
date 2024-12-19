using UnityEngine;

public interface IUnlockable
{
    // Open Function
    // Closed Function
    // isLocked Bool

    public bool IsLocked { get; }
    public bool RequireKey { get; }

    public void Open();
    public void Close();
    public void Toggle();
    public void Unlock();
}
