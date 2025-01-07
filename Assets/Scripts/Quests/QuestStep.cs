using UnityEngine;
using System;

[Serializable]
public class QuestStep : MonoBehaviour
{
    [SerializeField] protected string questName = "";
    [SerializeField] protected int questStep = -1;
    [SerializeField] bool initialStep = false;
    [SerializeField] bool lastStep = false;

    #region QuestStep Utility
    public bool QuestExistsInQuestManager() => QuestManager.Instance.GetQuest(questName) != null;

    public bool QuestIsOnThisStep()
    {
        bool questOnStep = (QuestManager.Instance.GetQuest(questName)?.GetCurrentQuestStep() == questStep);
        bool questStartable = (QuestManager.Instance.GetQuest(questName) == null && initialStep);

        return questOnStep || questStartable;
    }
    #endregion

    protected virtual void InitializeQuest()
    {
        if (!QuestExistsInQuestManager())
        {
            QuestManager.Instance.AddQuest(questName, questStep + 1);
        }
    }

    public virtual void ProgressQuest()
    {
        if (initialStep) { InitializeQuest(); }
        else if (lastStep) { QuestManager.Instance.CompleteQuest(questName); }
        else
        {
            QuestManager.Instance.ProgressQuestToNextStep(questName);
        }
    }
}
