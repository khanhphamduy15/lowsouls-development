using Unity.Netcode;
using UnityEngine;

namespace LS 
{
    public class PlayerCombatManager : CharacterCombatManager
    {
        public WeaponItem currentWeaponBeingUsed;
        PlayerManager player;

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        public void PerformWeaponBasedAction(WeaponItemAction weaponAction, WeaponItem weaponPerformingAction)
        {
            //perform action
            weaponAction.AttemptToPerformAction(player, weaponPerformingAction);
            player.playerNetworkManager.NotifyTheServerOfWeaponActionServerRpc(NetworkManager.Singleton.LocalClientId, weaponAction.actionID, weaponPerformingAction.itemID);
        }
        
        public void DrainStaminaBasedOnAttack()
        {
            if (!player.IsOwner) return;
            if (currentWeaponBeingUsed == null) return;

            float staminaDeducted = 0;
            switch (currentAttackType)
            {
                case AttackType.LightAttack01:
                    staminaDeducted = currentWeaponBeingUsed.baseStaminaCost * currentWeaponBeingUsed.lightAttackStaminaCostMultiplier;
                    break;
                default:
                    break;
            }
            player.playerNetworkManager.currentStamina.Value -= Mathf.RoundToInt(staminaDeducted);
             
        }
    }
}
