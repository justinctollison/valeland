using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance;

    public UnityEvent<float> onExperienceGranted;
    public UnityEvent<float> onExperienceUpdated;
    public UnityEvent<float> onHealthChanged;
    public UnityEvent<float> onManaChanged;
    public UnityEvent onPlayerDied;
    public UnityEvent onPlayerRevived;
    public UnityEvent onPlayerLeveledUp;

    public UnityEvent onStatPointSpent;
    public UnityEvent onSkillPointSpent;

    public UnityEvent<EquippableAbility> onNewAbility2Equipped;

    public UnityEvent onCurrentTargetHealthChanged;

    public UnityEvent onHoverEnterEvent; // Event fired when mouse hovers over this target
    public UnityEvent onHoverExitEvent; // Event fired when mouse stops hovering

    public UnityEvent onDialogueStarted;
    public UnityEvent onDialogueEnded;


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
}

