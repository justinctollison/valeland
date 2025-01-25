using UnityEngine;
using UnityEngine.UI;
public class CooldownIndicator : MonoBehaviour
{
    [SerializeField] Image AbilityIcon1;
    [SerializeField] Image AbilityIcon2;
    [SerializeField] Image AbilityIcon3;
    [SerializeField] GameObject cooldownFillAbility1;
    [SerializeField] GameObject cooldownFillAbility2;
    [SerializeField] GameObject cooldownFillAbility3;

    private void Update()
    {
        if (PlayerController.Instance.GetAbility1() != null)
        {
            cooldownFillAbility1.transform.localScale = new Vector3(1, 1 - PlayerController.Instance.GetAbility1CooldownPercentage(), 1);
        }
        if (PlayerController.Instance.GetAbility2() != null)
        {
            cooldownFillAbility2.transform.localScale = new Vector3(1, 1 - PlayerController.Instance.GetAbility2CooldownPercentage(), 1);
        }
        if (PlayerController.Instance.GetAbility3() != null)
        {
            cooldownFillAbility3.transform.localScale = new Vector3(1, 1 - PlayerController.Instance.GetAbility3CooldownPercentage(), 1);
        }
    }
    void UpdateCooldownImages(EquippableAbility ability)
    {
        AbilityIcon1.sprite = PlayerController.Instance.GetAbility1().classSkill.skillIcon;
        AbilityIcon2.sprite = PlayerController.Instance.GetAbility2().classSkill.skillIcon;
        AbilityIcon3.sprite = PlayerController.Instance.GetAbility3().classSkill.skillIcon;
    }
    private void Start()
    {
        UpdateCooldownImages(null);
    }
    private void OnEnable()
    {
        EventsManager.Instance.onNewAbility2Equipped.AddListener(UpdateCooldownImages);
    }
    private void OnDisable()
    {
        EventsManager.Instance.onNewAbility2Equipped.RemoveListener(UpdateCooldownImages);
    }
}
