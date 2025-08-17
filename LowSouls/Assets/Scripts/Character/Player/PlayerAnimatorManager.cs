using UnityEngine;

namespace LS
{
    public class PlayerAnimatorManager : CharacterAnimatorManager
    {
        PlayerManager player;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        public override void EnableCanDoCombo()
        {
            if (player.playerNetworkManager.isUsingRightHand.Value)
            {
                player.playerCombatManager.canComboWithMainHandWeapon = true;
            }
            else
            {

            }
        }

        public override void DisableCanDoCombo()
        {
            player.playerCombatManager.canComboWithMainHandWeapon = false;

        }
    }
}
