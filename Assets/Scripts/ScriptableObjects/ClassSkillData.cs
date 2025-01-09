using UnityEngine;

[CreateAssetMenu(fileName = "Class Skill", menuName = "New Class Skill")]
public class ClassSkillData : ScriptableObject
{
    public int skillLevel = 0;
    public string skillName = "";
    public string skillDescription = "";
    public Sprite skillIcon;

    public GameObject spawnablePrefab;
    public float attackRange;
    public float manaCost;
    public float cooldown;
}
