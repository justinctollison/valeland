using System.Collections.Generic;
using UnityEngine;

public class SkillTreePanel : MonoBehaviour
{
    ClassSkillTree skillTree;

    [SerializeField] List<SkillTreeButton> skillTreeButtons = new List<SkillTreeButton>();

    private void Awake()
    {
        skillTree = PlayerController.Instance.GetSkillManager().GetSkillTree();
    }

    private void Start()
    {
        EventsManager.Instance.onSkillPointSpent.AddListener(UpdateSkillTree);
    }
    private void OnDestroy()
    {
        EventsManager.Instance.onSkillPointSpent.RemoveListener(UpdateSkillTree);
    }

    private void OnEnable()
    {
        if (skillTree != null) UpdateSkillTree();
    }

    void UpdateSkillTree()
    {
        int i = 0;
        foreach (SkillTreeButton button in skillTreeButtons)
        {
            if (button != null)
            {
                if (i < skillTree.list.Count && skillTree.list[i] != null)
                {
                    button.gameObject.SetActive(true);
                    button.UpdateButton(skillTree.list[i]);
                }
                else
                {
                    button.gameObject.SetActive(false);
                }
            }
            i++;
            if (i >= skillTreeButtons.Count)
            {
                Debug.LogWarning("More skills than button slots in the active Class Skill Manager");
                i--;
            }
        }
    }

    public void HideSkillTree()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.HideSkillTree();
        }
    }
}
