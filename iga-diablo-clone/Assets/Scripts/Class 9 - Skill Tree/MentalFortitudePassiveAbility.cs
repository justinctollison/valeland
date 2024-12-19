using UnityEngine;

public class MentalFortitudePassiveAbility : EquippableAbility
{
    public override void LevelUp()
    {
        if(PlayerCharacterSheet.Instance.SkillPointSpendSuccessful())
        {
            classSkill.skillLevel++;

            float manaRegenMod = 1 + .5f * classSkill.skillLevel;
            PlayerController.Instance.GetCombat().SetManaRegenMod(manaRegenMod);
        }
    }
}
