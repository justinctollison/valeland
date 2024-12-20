using UnityEngine;

public interface IUnlockable
{
    // Open Function
    // Closed Function
    // isLocked Bool

    public bool GetIsLocked { get; }
    public bool GetRequireKey { get; }

    public void Open();
    public void Close();
    public void Toggle();
    public void Unlock();
}
