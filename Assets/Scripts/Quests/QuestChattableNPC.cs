using System;
using System.Collections.Generic;
using UnityEngine;
public class QuestChattableNPC : ChattableNPC
{
    [SerializeField] List<QuestDialogue> questDialogues;
    QuestDialogue activeStep;

    protected override void StartConversation()
    {
        Sprite portrait = GetComponent<BasicAI>().GetNPCData().portrait;
        string npcName = GetComponent<BasicAI>().GetNPCData().name;

        foreach (QuestDialogue questDialogue in questDialogues)
        {
            if (questDialogue.newStep.QuestIsOnThisStep())
            {
                EventsManager.Instance.onDialogueEnded.AddListener(EndOfDialogueQuestTrigger);
                activeStep = questDialogue;

                DialogueManager.Instance.TriggerDialogue(npcName, questDialogue.questDialogueDatas, portrait);
            }
        }

        if (activeStep == null)
        {
            DialogueManager.Instance.TriggerDialogue(npcName, dialogueDatas, portrait);
        }
    }

    private void EndOfDialogueQuestTrigger()
    {
        activeStep?.newStep.ProgressQuest();
        EventsManager.Instance.onDialogueEnded.RemoveListener(EndOfDialogueQuestTrigger);
        activeStep = null;
    }
}

[Serializable]
public class QuestDialogue
{
    public QuestStep newStep;
    public List<DialogueData> questDialogueDatas = new List<DialogueData>();
}