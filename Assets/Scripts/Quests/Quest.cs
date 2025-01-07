using UnityEngine;

public class Quest
{
    [SerializeField] private string questName;
    private int currentQuestStep = 0;

    public Quest(string questName, int currentQuestStep)
    {
        this.questName = questName;
        this.currentQuestStep = currentQuestStep;
    }

    public virtual void ProgressToNextStep()
    {
        currentQuestStep++;
        EventsManager.Instance.onQuestStatusChanged.Invoke();
    }

    public string GetQuestName() => questName;
    public int GetCurrentQuestStep() => currentQuestStep;
}
