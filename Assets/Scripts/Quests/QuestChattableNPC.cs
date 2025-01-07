using UnityEngine;
using System;
using System.Collections.Generic;
public class QuestChattableNPC : ChattableNPC
{
    [SerializeField] List<QuestDialogue> questDialogues;
    QuestDialogue activeStep;

    protected override void StartConversation()
    {
        foreach (QuestDialogue questDialogue in questDialogues)
        {
            if (questDialogue.newStep.QuestIsOnThisStep())
            {
                EventsManager.Instance.onDialogueEnded.AddListener(EndOfDialogueQuestTrigger);
                activeStep = questDialogue;
                DialogueManager.Instance.TriggerDialogue(npcName, questDialogue.questDialogueDatas);
            }
        }

        if (activeStep == null)
        {
            DialogueManager.Instance.TriggerDialogue(npcName, dialogueDatas);
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