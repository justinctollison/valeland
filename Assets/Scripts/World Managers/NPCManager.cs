using UnityEngine;
using System.Collections.Generic;
public class NPCManager : MonoBehaviour
{
    List<CombatReceiver> npcs = new List<CombatReceiver>();
    void Start()
    {
        
        npcs = new List<CombatReceiver>(GetComponentsInChildren<CombatReceiver>());
    }

    private void OnEnable()
    {
        EventsManager.Instance.onPlayerRevived.AddListener(ReviveAll);
    }
    private void OnDisable()
    {
        EventsManager.Instance.onPlayerRevived.RemoveListener(ReviveAll);
    }

    void ReviveAll()
    {
       foreach (CombatReceiver npc in npcs)
        {
            npc.Revive();
        }
    }
}
