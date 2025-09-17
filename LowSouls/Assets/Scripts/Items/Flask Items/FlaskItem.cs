using UnityEngine;

namespace LS
{
    [CreateAssetMenu(menuName = "Items/Consumables/Flask")]
    public class FlaskItem : QuickSlotItem
    {
        [Header("Restoration Value")]
        [SerializeField] int flaskRestoration = 50;

        [Header("Empty Item")]
        [SerializeField] GameObject emptyFlaskItem;
        [SerializeField] string emptyFlaskAnimation;


        [Header("FX")]
        [SerializeField] GameObject healingVFX;

        public override bool CanIUseThisItem(PlayerManager player)
        {
            if (!player.playerCombatManager.isUsingItem && player.isPerformingAction)
                return false;

            if (player.playerNetworkManager.isAttacking.Value)
                return false;

            return true;
        }

        public override void AttemptToUseItem(PlayerManager player)
        {
            if (!CanIUseThisItem(player))
                return;


            if (player.playerNetworkManager.remainingHealthFlask.Value <= 0)
            {
                if (player.playerCombatManager.isUsingItem)
                    return;

                player.playerCombatManager.isUsingItem = true;

                if (player.IsOwner)
                {
                    player.playerAnimatorManager.PlayTargetActionAnimation(emptyFlaskAnimation, false, false, true, true, false);
                    player.playerNetworkManager.HideWeaponServerRpc();
                }

                Destroy(player.playerEffectsManager.activeQuickSlotItemFX);
                GameObject emptyFlask = Instantiate(emptyFlaskItem, player.playerEquipmentManager.leftHandWeaponSlot.transform);
                player.playerEffectsManager.activeQuickSlotItemFX = emptyFlask;
                return;
            }

            player.playerEffectsManager.activeQuickSlotItemFX = Instantiate(itemModel, player.playerEquipmentManager.leftHandWeaponSlot.transform);

            if (player.IsOwner)
            {
                player.playerAnimatorManager.PlayTargetActionAnimation(useItemAnimation, false, false, true, true, false);
                player.playerNetworkManager.HideWeaponServerRpc();
            }
        }

        public override void SuccessfullyUseItem(PlayerManager player)
        {
            base.SuccessfullyUseItem(player);

            if (player.IsOwner && player.playerNetworkManager.remainingHealthFlask.Value > 0)
            {
                player.playerNetworkManager.currentHealth.Value += flaskRestoration;
                player.playerNetworkManager.remainingHealthFlask.Value -= 1;
                PlayHealFX(player);
                PlayerUIManager.instance.playerUIHudManager.SetQuickSlotItemIcon(player.playerInventoryManager.currentQuickSlotItem);

            }

            if (player.playerNetworkManager.remainingHealthFlask.Value <= 0)
            {
                Destroy(player.playerEffectsManager.activeQuickSlotItemFX);
                GameObject emptyFlask = Instantiate(emptyFlaskItem, player.playerEquipmentManager.leftHandWeaponSlot.transform);
                player.playerEffectsManager.activeQuickSlotItemFX = emptyFlask;
            }
        }

        public void PlayHealFX(PlayerManager player)
        {
            GameObject fx = Instantiate(WorldCharacterEffectsManager.instance.healingVFX, player.transform);
            Destroy(fx, 2f);
            player.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.instance.healingSFX);
        }

        public override int GetCurrentAmount(PlayerManager player)
        {
            int currentAmount = player.playerNetworkManager.remainingHealthFlask.Value;

            return currentAmount;
        }
    }
}
