using UnityEngine;

public class BossAI : BasicAI
{
    protected override void Start()
    {
        base.Start();

        EventsManager.Instance.onDialogueEnded.AddListener(ConversationEnded);
    }

    public void OnTriggerStay(Collider other)
    {
        if (_currentTarget != null) { return; }

        var target = other.gameObject.GetComponent<CombatReceiver>();
        if (target && !other.isTrigger)
        {
            if (target.GetFactionID() != this.GetComponent<CombatReceiver>().GetFactionID() && _currentTarget == null)
            {
                AddToTargetsList(target);
                _stateMachine.ChangeState(_stateMachine.GetEngageState());
            }
        }
    }

    private void ConversationEnded()
    {
        _factionID = FactionID.Evil;
        GetComponent<EnemyCombat>().SetFactionID(_factionID);
        GetComponent<ChattableNPC>().enabled = false;
    }
}
