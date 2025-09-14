using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

namespace LS
{
    public class UI_EquipmentInventorySlot : MonoBehaviour
    {
        public Image itemIcon;
        public Image hightlightedIcon;
        [SerializeField] public Item currentItem;
        
        public void AddItem(Item item)
        {
            if (item == null)
            {
                itemIcon.enabled = false;
                return;
            }
            itemIcon.enabled = true;
             currentItem = item;
            itemIcon.sprite = item.itemIcon;
        }

        public void SelectSlot()
        {
            hightlightedIcon.enabled = true;
        }

        public void DeselectSlot()
        {
            hightlightedIcon.enabled = false;
        }

        public void EquipItem()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();

            switch (PlayerUIManager.instance.playerUIEquipmentMenuManager.currentSelectedEquipmentSlot)
            {
                case EquipmentSlotType.RightWeapon01:
                    WeaponItem currentWeapon = player.playerInventoryManager.weaponInRightHandSlots[0];
                    if (currentWeapon.itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemToInventory(currentWeapon);
                    }
                    player.playerInventoryManager.weaponInRightHandSlots[0] = currentItem as WeaponItem;
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);

                    //Re equip
                    if (player.playerInventoryManager.rightHandWeaponIndex == 0)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = currentItem.itemID;
                    //Refresh
                    PlayerUIManager.instance.playerUIEquipmentMenuManager.OpenEquipmentManagerMenu();
                    break;
                case EquipmentSlotType.RightWeapon02:
                    break;
                case EquipmentSlotType.RightWeapon03:
                    break;
                case EquipmentSlotType.LeftWeapon01:
                    break;
                case EquipmentSlotType.LeftWeapon02:
                    break;
                case EquipmentSlotType.LeftWeapon03:
                    break;
                default:
                    break;
            }
        }
    }
}
