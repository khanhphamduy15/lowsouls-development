using UnityEngine;

namespace LS
{
    [CreateAssetMenu(menuName = "Character Action/Weapon Action/Light Attack Action")]
    public class LightAttackWeaponItemAction : WeaponItemAction
    {
        [Header("Light attack")]
        [SerializeField] string light_Attack_01 = "Main_Light_Attack_01"; //right hand
        [SerializeField] string light_Attack_02 = "Main_Light_Attack_02"; //right hand

        [Header("Running attack")]
        [SerializeField] string running_Attack_01 = "Main_Run_Attack_01"; //right hand

        [Header("Rolling attack")]
        [SerializeField] string rolling_Attack_01 = "Main_Roll_Attack_01";


        public override void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);

            if (!playerPerformingAction.IsOwner) return;

            if (playerPerformingAction.playerNetworkManager.currentStamina.Value <= 0) return;

            if (!playerPerformingAction.playerLocomotionManager.isGrounded) return;

            //running attack if sprinting
            if (playerPerformingAction.playerNetworkManager.isSprinting.Value)
            {
                PerformRunningAttack(playerPerformingAction, weaponPerformingAction);
                return;
            }

            //rolling attack if rolling
            if (playerPerformingAction.characterCombatManager.canPerformRollAttack)
            {
                PerformRollingAttack(playerPerformingAction, weaponPerformingAction);
                return;
            }

            PerformLightAttack(playerPerformingAction, weaponPerformingAction);


        }

        private void PerformLightAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            //if can combo and is attacking, do combo
            if (playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon && playerPerformingAction.isPerformingAction)
            {
                playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon = false;
                //perform attack based on prev attack
                if (playerPerformingAction.playerCombatManager.lastAttackAnimation == light_Attack_01)
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.LightAttack02, light_Attack_02, true);
                }
                else
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.LightAttack01, light_Attack_01, true);
                }
            }
            //else do normal attack
            else if (!playerPerformingAction.isPerformingAction)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.LightAttack01, light_Attack_01, true);
            }
        }

        private void PerformRunningAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            //two-handed version 
            //else play 1 hand

            playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.RunningAttack01, running_Attack_01, true);
           
        }
        private void PerformRollingAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            //two-handed version 
            //else play 1 hand
            playerPerformingAction.playerCombatManager.canPerformRollAttack = false;
            playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(AttackType.RollingAttack01, rolling_Attack_01, true);
           
        }
    }
}
