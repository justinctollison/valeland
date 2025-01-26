using UnityEngine;
using System;
using System.Collections.Generic;

public class ClassSkillManager : MonoBehaviour
{
    [SerializeField] ClassSkillData basicMelee;
    [SerializeField] ClassSkillTree skillTree; 

    public ClassSkillTree GetSkillTree()
    {
        return skillTree;
    }
    private void Start()
    {
        foreach (EquippableAbility ability in skillTree.list)
            ability.classSkill.skillLevel = 0;
    }
}

[Serializable]
public class ClassSkillTree
{
    public List<EquippableAbility> list;
}