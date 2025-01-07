using UnityEngine;
using UnityEngine.UIElements;

public class FireballEquippableAbility : EquippableAbility
{
    public override void RunAbilityClicked(PlayerController player)
    {
        _myPlayer = player;
        _targetedReceiver = null;

        var clickable = MouseWorld.Instance.GetClickable() as CombatReceiver;
        if (CanCastFireball(clickable) && clickable.GetFactionID() != player.GetFactionID())
        {
            SpawnEquippedAttack(MouseWorld.Instance.GetMousePosition());
            _myPlayer.GetMovement().MoveToLocation(_myPlayer.transform.position);
            //AudioManager.Instance.PlayPilotLaserSFX();
            _myPlayer.GetCombat().SpendMana(_manaCost);
        }
    }

    private bool CanCastFireball(CombatReceiver clickable)
    {
        return _myPlayer.GetCombat().GetMana() >= _manaCost && (clickable != null || Input.GetKey(KeyCode.LeftShift));
    }

    protected override void SpawnEquippedAttack(Vector3 location)
    {
        _myPlayer.GetAnimator().TriggerAttack();

        _myPlayer.transform.LookAt(new Vector3(location.x, _myPlayer.transform.position.y, location.z));

        Vector3 spawnPosition = _myPlayer.transform.position + _myPlayer.transform.forward;

        GameObject newAttack = Instantiate(_spawnablePrefab, spawnPosition, Quaternion.identity);

        var fireballCA = newAttack.gameObject.GetComponent<FireballCA>();
        fireballCA.SetFactionID(_myPlayer.GetFactionID());
        fireballCA.SetShootDirection(_myPlayer.transform.forward);

        float calculatedDamage = fireballCA.GetBaseDamage() + (2 * classSkill.skillLevel);
        fireballCA.InitializeDamage(calculatedDamage);
    }
}
