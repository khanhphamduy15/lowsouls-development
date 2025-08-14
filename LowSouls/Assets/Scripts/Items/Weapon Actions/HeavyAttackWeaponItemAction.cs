using UnityEngine;

namespace LS {
    [CreateAssetMenu(menuName = "Character Action/Weapon Action/Heavy Attack Action")]
    public class HeavyAttackWeaponItemAction : WeaponItemAction
    {
        [SerializeField] string heavy_Attack_01 = "Main_Heavy_Attack_01"; //right hand
        public override void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);

            if (!playerPerformingAction.IsOwner) return;

            if (playerPerformingAction.playerNetworkManager.currentStamina.Value <= 0) return;

            if (!playerPerformingAction.isGrounded) return;

            PerformHeavyAttack(playerPerformingAction, weaponPerformingAction);


        }

        private void PerformHeavyAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            if (playerPerformingAction.playerNetworkManager.isUsingRightHand.Value)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.HeavyAttack01, heavy_Attack_01, true);
            }
            if (playerPerformingAction.playerNetworkManager.isUsingLeftHand.Value)
            {
            }
        }
    }
}