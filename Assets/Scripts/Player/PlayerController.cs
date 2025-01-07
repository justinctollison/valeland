using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] ClassSkillManager _skillManager;
    [SerializeField] private EquippableAbility _ability1;
    [SerializeField] private EquippableAbility _ability2;
    [SerializeField] private EquippableAbility _ability3;

    private FactionID _factionID = FactionID.Good;
    private bool _isAlive = true;
    private bool _inDialogue;

    public static PlayerController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        var camera = Camera.main.gameObject.AddComponent<CameraController>();
        camera.SetFollowTarget(this.gameObject);

        EventsManager.Instance.onDialogueStarted.AddListener(StartDialogueMode);
        EventsManager.Instance.onDialogueEnded.AddListener(EndDialogueMode);
    }

    private void Update()
    {
        if (!_isAlive || _inDialogue) { return; }

        if (Input.GetMouseButtonDown(0) && _ability1 != null)
        {
            UseAbility1();
        }
        
        if (Input.GetMouseButtonDown(1) && _ability2 != null)
        {
            UseAbility2();
        }
        if (Input.GetKeyDown(KeyCode.Space) && _ability3 != null)
        {
            UseAbility3();
        }

    }

    private void OnDestroy()
    {
        EventsManager.Instance.onDialogueStarted.RemoveListener(StartDialogueMode);
        EventsManager.Instance.onDialogueEnded.RemoveListener(EndDialogueMode);
    }

    #region Ability Stuff
    private void UseAbility1()
    {
        _ability1.RunAbilityClicked(this);
    }

    private void UseAbility2()
    {
        _ability2.RunAbilityClicked(this);
    }
    private void UseAbility3()
    {
        _ability3.RunAbilityClicked(this);
    }

    public void SetAbility2(EquippableAbility newAbility)
    {
        _ability2 = newAbility;
        EventsManager.Instance.onNewAbility2Equipped.Invoke(_ability2);
    }

    public EquippableAbility GetAbility2() => _ability2;
    #endregion

    #region Utility
    public PlayerMovement GetMovement() => GetComponent<PlayerMovement>();
    public PlayerAnimator GetAnimator() => GetComponent<PlayerAnimator>();
    public PlayerCharacterSheet GetCharacterSheet() => GetComponent<PlayerCharacterSheet>();
    public PlayerCombat GetCombat() => GetComponent<PlayerCombat>();
    public FactionID GetFactionID() => _factionID;
    public ClassSkillManager GetSkillManager()
    {
        return _skillManager;
    }
    #endregion

    public void TriggerDeath()
    {
        _isAlive = false;
        GetAnimator().TriggerDeath();
    }

    public void TriggerRevive()
    {
        _isAlive = true;
        GetAnimator().TriggerRevive();
    }

    #region Dialogue Mode Listeners
    public void StartDialogueMode()
    {
        _inDialogue = true;
    }

    public void EndDialogueMode()
    {
        _inDialogue = false;
    }
    #endregion
}
