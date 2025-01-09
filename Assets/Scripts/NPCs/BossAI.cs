using UnityEngine;

public class BossAI : BasicAI
{
    protected override void Start()
    {
        base.Start();

        EventsManager.Instance.onDialogueEnded.AddListener(ConversationEnded);
    }

    private void ConversationEnded()
    {
        _data.factionID = FactionID.Evil;
        GetComponent<ChattableNPC>().enabled = false;
    }
}
