using UnityEngine;

namespace LS
{
    [CreateAssetMenu(menuName = "Character Action/Weapon Action/Light Attack Action")]
    public class LightAttackWeaponItemAction : WeaponItemAction
    {
        [SerializeField] string light_Attack_01 = "Main_Light_Attack_01"; //right hand
        public override void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);

            if (!playerPerformingAction.IsOwner) return;

            if (playerPerformingAction.playerNetworkManager.currentStamina.Value <= 0) return;

            if (!playerPerformingAction.isGrounded) return;

            PerformLightAttack(playerPerformingAction, weaponPerformingAction);


        }

        private void PerformLightAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            if (playerPerformingAction.playerNetworkManager.isUsingRightHand.Value)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.LightAttack01,light_Attack_01, true);
            }
            if (playerPerformingAction.playerNetworkManager.isUsingLeftHand.Value)
            {
            }
        }
    }
}
