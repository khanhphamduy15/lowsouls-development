using UnityEngine;

namespace LS
{
    [CreateAssetMenu(menuName = "Character Action/Weapon Action/Light Attack Action")]
    public class LightAttackWeaponItemAction : WeaponItemAction
    {
        //Main hand
        [Header("Light attack")]
        [SerializeField] string light_Attack_01 = "Main_Light_Attack_01"; //right hand
        [SerializeField] string light_Attack_02 = "Main_Light_Attack_02"; //right hand

        [Header("Running attack")]
        [SerializeField] string running_Attack_01 = "Main_Run_Attack_01"; //right hand

        [Header("Rolling attack")]
        [SerializeField] string rolling_Attack_01 = "Main_Roll_Attack_01";

        //Two hand
        [Header("Light attack")]
        [SerializeField] string th_Light_Attack_01 = "TH_Light_Attack_01"; //right hand
        [SerializeField] string th_Light_Attack_02 = "TH_Light_Attack_02"; //right hand

        [Header("Running attack")]
        [SerializeField] string th_Running_Attack_01 = "TH_Run_Attack_01"; //right hand

        [Header("Rolling attack")]
        [SerializeField] string th_Rolling_Attack_01 = "TH_Roll_Attack_01";

        public override void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);

            if (!playerPerformingAction.IsOwner) return;

            if (playerPerformingAction.playerNetworkManager.currentStamina.Value <= 0) return;

            if (!playerPerformingAction.playerLocomotionManager.isGrounded) return;

            if (playerPerformingAction.IsOwner)
                playerPerformingAction.playerNetworkManager.isAttacking.Value = true;

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
            if (playerPerformingAction.playerNetworkManager.isTwoHandingWeapon.Value)
            {
                PerformTwoHandLightAttack(playerPerformingAction, weaponPerformingAction);
            }
            else
            {
                PerformMainHandLightAttack(playerPerformingAction, weaponPerformingAction);
            }

        }

        private void PerformMainHandLightAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            //if can combo and is attacking, do combo
            if (playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon && playerPerformingAction.isPerformingAction)
            {
                playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon = false;
                //perform attack based on prev attack
                if (playerPerformingAction.playerCombatManager.lastAttackAnimation == light_Attack_01)
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.LightAttack02, light_Attack_02, true);
                }
                else
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.LightAttack01, light_Attack_01, true);
                }
            }
            //else do normal attack
            else if (!playerPerformingAction.isPerformingAction)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.LightAttack01, light_Attack_01, true);
            }
        }

        private void PerformTwoHandLightAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            //if can combo and is attacking, do combo
            if (playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon && playerPerformingAction.isPerformingAction)
            {
                playerPerformingAction.playerCombatManager.canComboWithMainHandWeapon = false;
                //perform attack based on prev attack
                if (playerPerformingAction.playerCombatManager.lastAttackAnimation == th_Light_Attack_01)
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.LightAttack02, th_Light_Attack_02, true);
                }
                else
                {
                    playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.LightAttack01, th_Light_Attack_01, true);
                }
            }
            //else do normal attack
            else if (!playerPerformingAction.isPerformingAction)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.LightAttack01, th_Light_Attack_01, true);
            }
        }

        private void PerformRunningAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            //two-handed version 
            //else play 1 hand
            if (playerPerformingAction.playerNetworkManager.isTwoHandingWeapon.Value)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.RunningAttack01, th_Running_Attack_01, true);
            }
            else
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.RunningAttack01, running_Attack_01, true);
            }
        }
        private void PerformRollingAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            //two-handed version 
            //else play 1 hand
            playerPerformingAction.playerCombatManager.canPerformRollAttack = false;

            if (playerPerformingAction.playerNetworkManager.isTwoHandingWeapon.Value)
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.RollingAttack01, th_Rolling_Attack_01, true);
            }
            else
            {
                playerPerformingAction.playerAnimatorManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.RollingAttack01, rolling_Attack_01, true);

            }
        }
    }
}
