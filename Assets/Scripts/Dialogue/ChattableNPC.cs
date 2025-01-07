using UnityEngine;
using System.Collections.Generic;

public class ChattableNPC : MonoBehaviour, IClickable
{
    [SerializeField] protected string npcName = "";
    [SerializeField] protected List<DialogueData> dialogueDatas = new List<DialogueData>();
    bool clicked = false;
    bool buttonReleased = true;

    private void Update()
    {
        if (clicked) RunClicked();
    }

    void RunClicked()
    {
        if (Input.GetMouseButtonDown(0) && buttonReleased)
        {
            clicked = false;
        }
        else
        {
            buttonReleased = true;
        }

        if (Vector3.Distance(PlayerController.Instance.transform.position, transform.position) < 1.5f)
        {
            StartConversation();
        }
    }

    protected virtual void StartConversation()
    {
        clicked = false;
        DialogueManager.Instance.TriggerDialogue(npcName, dialogueDatas);
        PlayerController.Instance.GetMovement().MoveToLocation(PlayerController.Instance.transform.position);
    }
    private void OnMouseDown()
    {
        PlayerController.Instance.GetMovement().MoveToLocation(transform.position);
        clicked = true;
        buttonReleased = false;
    }

    public bool CanBeClicked()
    {
        return true;
    }

    public void OnClick()
    {
    }

    public void OnHover()
    {
    }

    public void OnHoverExit()
    {
    }
}
