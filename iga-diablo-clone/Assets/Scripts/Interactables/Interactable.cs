using UnityEngine;

public class Interactable : MonoBehaviour, IClickable
{
    // Need a UI pop-up for when clicking on an Interactable
    // Need data for the interactable, a SO

    protected virtual void Awake()
    {
        InteractableUIManager.Instance.HidePopup();
    }

    protected virtual void Update()
    {
        CheckIfNearPlayer();

        if (Input.GetMouseButtonDown(0) && MouseWorld.Instance.GetClickable() as Interactable != null)
        {
            OnClick();
        }
    }

    protected virtual void CheckIfNearPlayer()
    {
        bool playerIsNearby = false;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);
        foreach (Collider hit in hitColliders)
        {
            if (hit.GetComponent<PlayerController>())
            {
                playerIsNearby = true;
                break; // Exit loop early once we find the player
            }
        }

        // Set layer based on proximity to the player
        if (playerIsNearby)
        {
            SetLayer(LayerMask.NameToLayer("Clickable"));
            // Do a VFX, like a sparkle
        }
        else
        {
            SetLayer(LayerMask.NameToLayer("Default"));
        }
    }

    public void SetLayer(int layer)
    {
        gameObject.layer = layer;
    }

    public int GetLayer()
    {
        return gameObject.layer;
    }

    public bool CanBeClicked()
    {
        return true;
    }

    public virtual void OnClick()
    {

    }

    public void OnHover()
    {
    }

    public void OnHoverExit()
    {
    }
}
