using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTreeButton : MonoBehaviour
{
    [SerializeField] Image buttonImage;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;

    EquippableAbility equippableSkill;

    public void UpdateButton(EquippableAbility skill)
    {
        equippableSkill = skill;
        buttonImage.sprite = skill.classSkill.skillIcon;
        nameText.text = skill.classSkill.skillName;
        levelText.text = skill.GetClassSkillLevel().ToString();
    }

    public void PurchaseUpgrade()
    {
        equippableSkill.LevelUp();
        EventsManager.Instance.onSkillPointSpent.Invoke();
    }

    public void EquipAbility()
    {
        if (equippableSkill.GetClassSkillLevel() > 0 && equippableSkill.GetComponent<EquippableAbility>() != null)
            PlayerController.Instance.SetAbility2(equippableSkill.GetComponent<EquippableAbility>());
    }
}
