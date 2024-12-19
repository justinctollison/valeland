using UnityEngine;

public class PlayerAnimator : BasicAnimator
{
    protected virtual void Update()
    {
        DeltaMovement();
    }
}
