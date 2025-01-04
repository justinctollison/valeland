using UnityEngine;

public class EquippableAbility : MonoBehaviour
{
    [SerializeField] public ClassSkillData classSkill;

    protected GameObject _spawnablePrefab;
    protected float _attackRange;
    protected float _manaCost;

    private int _classSkillLevel;

    protected CombatReceiver _targetedReceiver;
    protected PlayerController _myPlayer;

    protected virtual void Start()
    {
        _attackRange = classSkill.attackRange;
        _manaCost = classSkill.manaCost;
        _classSkillLevel = classSkill.skillLevel;
        _spawnablePrefab = classSkill.spawnablePrefab;
    }

    protected virtual void Update()
    {
        if (_targetedReceiver != null)
        {
            RunTargetAttack();
        }
    }

    public virtual void RunAbilityClicked(PlayerController player)
    {
        if (MouseIsOverUI()) { return; }

        _myPlayer = player;
        _targetedReceiver = null;

        var clickable = MouseWorld.Instance.GetClickable() as CombatReceiver;
        if (clickable != null && clickable.GetFactionID() != player.GetFactionID())
        {
            _targetedReceiver = (CombatReceiver)MouseWorld.Instance.GetClickable();
        }

        RunTargetAttack();
    }

    protected virtual void SuccessfulRaycastFunctionality(ref RaycastHit hit)
    {
        _myPlayer.GetMovement().MoveToLocation(hit.point);

        if (hit.collider.gameObject.GetComponent<IClickable>() != null)
        {
            _targetedReceiver = hit.collider.gameObject.GetComponent<CombatReceiver>();
        }
    }

    protected virtual void SpawnEquippedAttack(Vector3 location)
    {
        GameObject newAttack = Instantiate(_spawnablePrefab, location, Quaternion.identity);
        newAttack.GetComponent<CombatActor>().SetFactionID(_myPlayer.GetFactionID());

        float critMod = 1f;
        int random = Random.Range(0, 100);
        float playerDexertiy = PlayerCharacterSheet.Instance.GetDexterity();
        if (random < playerDexertiy) { critMod = 2; }

        float playerStrength = PlayerCharacterSheet.Instance.GetStrength();
        float calculatedDamage = (playerStrength / 5) * critMod;

        newAttack.GetComponent<CombatActor>().InitializeDamage(calculatedDamage);
    }

    public virtual void CancelAbility()
    {
        _targetedReceiver = null;
    }

    protected virtual void RunTargetAttack()
    {
        if (_targetedReceiver != null)
        {
            if (Vector3.Distance(_myPlayer.transform.position, _targetedReceiver.transform.position) <= _attackRange && _targetedReceiver.GetFactionID() != _myPlayer.GetFactionID())
            {
                _myPlayer.GetMovement().MoveToLocation(_myPlayer.transform.position);

                Vector3 lookPosition = new Vector3(_targetedReceiver.transform.position.x,
                                                    _myPlayer.transform.position.y,
                                                    _targetedReceiver.transform.position.z);

                _myPlayer.transform.LookAt(lookPosition);

                SpawnEquippedAttack(_myPlayer.transform.position + _myPlayer.transform.forward);
                _myPlayer.GetAnimator().TriggerAttack();

                _targetedReceiver = null;
            }
            else
            {
                _myPlayer.GetMovement().MoveToLocation(_targetedReceiver.transform.position);
            }
        }
        else
        {
            _myPlayer.GetMovement().MoveToLocation(MouseWorld.Instance.GetMousePosition());
        }
    }

    public bool MouseIsOverUI()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }

    public virtual void LevelUp()
    {
        if (PlayerCharacterSheet.Instance.SkillPointSpendSuccessful())
        {
            _classSkillLevel++;
        }
    }

    public int GetClassSkillLevel() => _classSkillLevel;
}
