using UnityEngine;
using UnityEngine.UI;

public class ActiveAbilityIndicator : MonoBehaviour
{
    private void Start()
    {
        EventsManager.Instance.onNewAbility2Equipped.AddListener(UpdateImage);
        UpdateImage(PlayerController.Instance.GetAbility2());
    }

    private void OnDestroy()
    {
        EventsManager.Instance.onNewAbility2Equipped.RemoveListener(UpdateImage);
    }

    void UpdateImage(EquippableAbility newAbility)
    {
        GetComponent<Image>().sprite = newAbility.classSkill.skillIcon;
    }
}
