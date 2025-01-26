using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] ClassSkillManager _skillManager;
    [SerializeField] private EquippableAbility _ability1;
    [SerializeField] private EquippableAbility _ability2;
    [SerializeField] private EquippableAbility _ability3;

    float ability1Timer;
    float ability2Timer;
    float ability3Timer;

    private FactionID _factionID = FactionID.Good;
    private bool _isAlive = true;
    private bool _inDialogue;

    public static PlayerController Instance;
    public Vector3 spawnLocation;

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
        camera.SetFollowTarget(gameObject);

        InitailizeGearVisuals();

        EventsManager.Instance.onDialogueStarted.AddListener(StartDialogueMode);
        EventsManager.Instance.onDialogueEnded.AddListener(EndDialogueMode);
    }

    
    private void Update()
    {
        ability1Timer += Time.deltaTime;
        ability2Timer += Time.deltaTime;
        ability3Timer += Time.deltaTime;

        if (!_isAlive || _inDialogue) { return; }

        if (Input.GetMouseButtonDown(0) && _ability1 != null && ability1Timer > _ability1.classSkill.cooldown)
        {
            UseAbility1();
            var clickable = MouseWorld.Instance.GetClickable() as CombatReceiver; //prevents cooldown from starting if the player doesn't click on a combat receiver
            if (clickable != null)
                ability1Timer = 0;
        }
        if (Input.GetMouseButtonDown(1) && _ability2 != null && ability2Timer > _ability2.classSkill.cooldown)
        {
            UseAbility2();
            ability2Timer = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && _ability3 != null && ability3Timer > _ability3.classSkill.cooldown)
        {
            UseAbility3();
            ability3Timer = 0;
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

    public EquippableAbility GetAbility1() => _ability1;
    public EquippableAbility GetAbility2() => _ability2;
    public EquippableAbility GetAbility3() => _ability3;

    public float GetAbility1CooldownPercentage() => ability1Timer / _ability1.classSkill.cooldown;
    public float GetAbility2CooldownPercentage() => ability2Timer / _ability2.classSkill.cooldown;
    public float GetAbility3CooldownPercentage() => ability3Timer / _ability3.classSkill.cooldown;
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
        EventsManager.Instance.onPlayerDied?.Invoke();
    }

    public void TriggerRevive()
    {
        _isAlive = true;
        GetAnimator().TriggerRevive();
        EventsManager.Instance.onPlayerRevived?.Invoke();
        GetMovement().WarpToLocation(spawnLocation);
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

    #region Initialize Gear Visuals
    private void InitailizeGearVisuals()
    {
        EquipmentCustomizer.Instance.UnequipChestArmor();
        EquipmentCustomizer.Instance.UnequipClosedHelmet();
        EquipmentCustomizer.Instance.UnequipOpenHelmet();
        EquipmentCustomizer.Instance.UnequipShoulderArmor();
        EquipmentCustomizer.Instance.UnequipCape();
        EquipmentCustomizer.Instance.EnableHead();
        EquipmentCustomizer.Instance.UnequipLegArmor();
        EquipmentCustomizer.Instance.UnequipGloves();
        EquipmentCustomizer.Instance.UnequipBoots();
    }
    #endregion
}
