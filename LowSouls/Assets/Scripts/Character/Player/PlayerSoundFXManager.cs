using UnityEngine;

namespace LS
{
    public class PlayerSoundFXManager : CharacterSoundFXManager
    {
        PlayerManager player;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        public override void PlayBlockSFX()
        {
            PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(player.playerCombatManager.currentWeaponBeingUsed.blocking));
        }
    }
}
