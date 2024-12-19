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
}

[Serializable]
public class ClassSkillTree
{
    public List<EquippableAbility> list;
}