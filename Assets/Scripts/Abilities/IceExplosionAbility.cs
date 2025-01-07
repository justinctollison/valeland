using UnityEngine;

public class IceExplosionAbility : EquippableAbility
{
    public override void RunAbilityClicked(PlayerController player)
    {
        _myPlayer = player;

        if (CanCastIceExplosion())
        {
            SpawnEquippedAttack(MouseWorld.Instance.GetMousePosition());
            _myPlayer.GetMovement().MoveToLocation(_myPlayer.transform.position);
            //AudioManager.Instance.PlayPilotLaserSFX();
            _myPlayer.GetCombat().SpendMana(_manaCost);
        }
    }

    private bool CanCastIceExplosion()
    {
        return _myPlayer.GetCombat().GetMana() >= _manaCost;
    }

    protected override void SpawnEquippedAttack(Vector3 location)
    {
        _myPlayer.GetAnimator().TriggerAttack();

        Vector3 spawnPosition = _myPlayer.transform.position + Vector3.down * .7f; // Spawn at player's feet

        GameObject newAttack = Instantiate(_spawnablePrefab, spawnPosition, Quaternion.identity);
        EffectsManager.Instance.PlayIceExplosion(spawnPosition, 4);

        var iceExplosionAttack = newAttack.gameObject.GetComponent<IceExplosionAttack>();
        iceExplosionAttack.SetFactionID(_myPlayer.GetFactionID());

        float calculatedDamge = iceExplosionAttack.GetBaseDamage() + (2 * classSkill.skillLevel);
        iceExplosionAttack.InitializeDamage(calculatedDamge);
    }
}
