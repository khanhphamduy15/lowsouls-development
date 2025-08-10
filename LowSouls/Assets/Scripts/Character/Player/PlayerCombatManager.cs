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
    }
}
