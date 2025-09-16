using UnityEngine;

namespace LS {
    [CreateAssetMenu(menuName = "Character Action/Weapon Action/Off Hand Melee Action")]
    public class OffHandMeleeAction : WeaponItemAction
    {
        public override void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);

            if (playerPerformingAction.playerCombatManager.isUsingItem)
                return;

            //can block check
            if (!playerPerformingAction.playerCombatManager.canBlock)
                return;

            //attack status check
            if (playerPerformingAction.playerNetworkManager.isAttacking.Value)
            {
                //disable blocking
                if (playerPerformingAction.IsOwner)
                    playerPerformingAction.playerNetworkManager.isBlocking.Value = false;

                return;
            }

            if (playerPerformingAction.playerNetworkManager.isBlocking.Value)
                return;

            if (playerPerformingAction.IsOwner)
                playerPerformingAction.playerNetworkManager.isBlocking.Value = true;
        }
    }
}
