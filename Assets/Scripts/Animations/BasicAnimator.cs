using System.Collections;
using UnityEngine;

public class BasicAnimator : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    protected Vector3 oldPosition = Vector3.zero;
    protected Vector3 deltaPosition = Vector3.zero;

    protected virtual void DeltaMovement()
    {
        deltaPosition = transform.position - oldPosition;

        if (deltaPosition.sqrMagnitude > 0.001f * Time.deltaTime)
        {
            SetWalking(true);
        }
        else
        {
            SetWalking(false);
        }

        oldPosition = transform.position;
    }

    public virtual void SetWalking(bool value)
    {
        _animator.SetBool("Walking", value);
    }

    public virtual void TriggerAttack()
    {
        _animator.SetTrigger("Attack");
    }

    public virtual void TriggerDeath()
    {
        _animator.SetTrigger("Death");
    }

    public virtual void TriggerRevive()
    {
        _animator.SetTrigger("Revive");
    }
    private IEnumerator PauseAnimation(float duration)
    {
        _animator.enabled = false;
        yield return new WaitForSeconds(duration);
        _animator.enabled = true;
    }
    public void MultiplySpeed(float speed)
    {
        _animator.speed *= speed;
    }
}
