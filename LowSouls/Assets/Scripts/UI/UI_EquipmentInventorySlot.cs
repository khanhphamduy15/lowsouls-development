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
            Item equippedItem;

            switch (PlayerUIManager.instance.playerUIEquipmentMenuManager.currentSelectedEquipmentSlot)
            {
                case EquipmentSlotType.RightWeapon01:

                    equippedItem = player.playerInventoryManager.weaponInRightHandSlots[0];

                    if (equippedItem.itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    player.playerInventoryManager.weaponInRightHandSlots[0] = currentItem as WeaponItem;
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);


                    //Re equip
                    if (player.playerInventoryManager.rightHandWeaponIndex == 0)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = currentItem.itemID;

                    //Refresh
                    PlayerUIManager.instance.playerUIEquipmentMenuManager.RefreshMenu();

                    break;
                case EquipmentSlotType.RightWeapon02:

                    equippedItem = player.playerInventoryManager.weaponInRightHandSlots[1];

                    if (equippedItem.itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    player.playerInventoryManager.weaponInRightHandSlots[1] = currentItem as WeaponItem;
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);


                    //Re equip
                    if (player.playerInventoryManager.rightHandWeaponIndex == 1)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = currentItem.itemID;

                    //Refresh
                    PlayerUIManager.instance.playerUIEquipmentMenuManager.RefreshMenu();

                    break;
                case EquipmentSlotType.RightWeapon03:

                    equippedItem = player.playerInventoryManager.weaponInRightHandSlots[2];

                    if (equippedItem.itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    player.playerInventoryManager.weaponInRightHandSlots[2] = currentItem as WeaponItem;
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);


                    //Re equip
                    if (player.playerInventoryManager.rightHandWeaponIndex == 2)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = currentItem.itemID;

                    //Refresh
                    PlayerUIManager.instance.playerUIEquipmentMenuManager.RefreshMenu();

                    break;
                case EquipmentSlotType.LeftWeapon01:

                    equippedItem = player.playerInventoryManager.weaponInLeftHandSlots[0];

                    if (equippedItem.itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    player.playerInventoryManager.weaponInLeftHandSlots[0] = currentItem as WeaponItem;
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);


                    //Re equip
                    if (player.playerInventoryManager.leftHandWeaponIndex == 0)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = currentItem.itemID;

                    //Refresh
                    PlayerUIManager.instance.playerUIEquipmentMenuManager.RefreshMenu();

                    break;
                case EquipmentSlotType.LeftWeapon02:

                    equippedItem = player.playerInventoryManager.weaponInLeftHandSlots[1];

                    if (equippedItem.itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    player.playerInventoryManager.weaponInLeftHandSlots[1] = currentItem as WeaponItem;
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);


                    //Re equip
                    if (player.playerInventoryManager.leftHandWeaponIndex == 1)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = currentItem.itemID;

                    //Refresh
                    PlayerUIManager.instance.playerUIEquipmentMenuManager.RefreshMenu();

                    break;
                case EquipmentSlotType.LeftWeapon03:

                    equippedItem = player.playerInventoryManager.weaponInLeftHandSlots[2];

                    if (equippedItem.itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    player.playerInventoryManager.weaponInLeftHandSlots[2] = currentItem as WeaponItem;
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);


                    //Re equip
                    if (player.playerInventoryManager.leftHandWeaponIndex == 2)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = currentItem.itemID;

                    //Refresh
                    PlayerUIManager.instance.playerUIEquipmentMenuManager.RefreshMenu();

                    break;
                case EquipmentSlotType.Head:

                    equippedItem = player.playerInventoryManager.headEquipment;

                    if (equippedItem != null)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    player.playerInventoryManager.headEquipment = currentItem as HeadEquipmentItem;
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);


                    //Re equip
                    player.playerEquipmentManager.LoadHeadEquipment(player.playerInventoryManager.headEquipment);

                    //Refresh
                    PlayerUIManager.instance.playerUIEquipmentMenuManager.RefreshMenu();

                    break;
                case EquipmentSlotType.Body:

                    equippedItem = player.playerInventoryManager.bodyEquipment;

                    if (equippedItem != null)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    player.playerInventoryManager.bodyEquipment = currentItem as BodyEquipmentItem;
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);


                    //Re equip
                    player.playerEquipmentManager.LoadBodyEquipment(player.playerInventoryManager.bodyEquipment);

                    //Refresh
                    PlayerUIManager.instance.playerUIEquipmentMenuManager.RefreshMenu();

                    break;
                case EquipmentSlotType.Legs:

                    equippedItem = player.playerInventoryManager.legEquipment;

                    if (equippedItem != null)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    player.playerInventoryManager.legEquipment = currentItem as LegEquipmentItem;
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);


                    //Re equip
                    player.playerEquipmentManager.LoadLegEquipment(player.playerInventoryManager.legEquipment);

                    //Refresh
                    PlayerUIManager.instance.playerUIEquipmentMenuManager.RefreshMenu();

                    break;
                case EquipmentSlotType.Hands:

                    equippedItem = player.playerInventoryManager.handEquipment;

                    if (equippedItem != null)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    player.playerInventoryManager.handEquipment = currentItem as HandEquipmentItem;
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);


                    //Re equip
                    player.playerEquipmentManager.LoadHandEquipment(player.playerInventoryManager.handEquipment);

                    //Refresh
                    PlayerUIManager.instance.playerUIEquipmentMenuManager.RefreshMenu();

                    break;
                case EquipmentSlotType.QuickSlot01:

                    equippedItem = player.playerInventoryManager.quickSlotItemInSlots[0];

                    if (equippedItem != null)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    player.playerInventoryManager.quickSlotItemInSlots[0] = currentItem as QuickSlotItem;
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);


                    //Re equip
                    if (player.playerInventoryManager.quickSlotItemIndex == 0)
                        player.playerNetworkManager.currentQuickSlotItemID.Value = currentItem.itemID;

                    //Refresh
                    PlayerUIManager.instance.playerUIEquipmentMenuManager.RefreshMenu();

                    break;
                case EquipmentSlotType.QuickSlot02:

                    equippedItem = player.playerInventoryManager.quickSlotItemInSlots[1];

                    if (equippedItem != null)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    player.playerInventoryManager.quickSlotItemInSlots[1] = currentItem as QuickSlotItem;
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);


                    //Re equip
                    if (player.playerInventoryManager.quickSlotItemIndex == 1)
                        player.playerNetworkManager.currentQuickSlotItemID.Value = currentItem.itemID;

                    //Refresh
                    PlayerUIManager.instance.playerUIEquipmentMenuManager.RefreshMenu();

                    break;
                case EquipmentSlotType.QuickSlot03:

                    equippedItem = player.playerInventoryManager.quickSlotItemInSlots[2];

                    if (equippedItem != null)
                    {
                        player.playerInventoryManager.AddItemToInventory(equippedItem);
                    }

                    player.playerInventoryManager.quickSlotItemInSlots[2] = currentItem as QuickSlotItem;
                    player.playerInventoryManager.RemoveItemFromInventory(currentItem);


                    //Re equip
                    if (player.playerInventoryManager.quickSlotItemIndex == 2)
                        player.playerNetworkManager.currentQuickSlotItemID.Value = currentItem.itemID;

                    //Refresh
                    PlayerUIManager.instance.playerUIEquipmentMenuManager.RefreshMenu();

                    break;
                default:
                    break;
            }

            PlayerUIManager.instance.playerUIEquipmentMenuManager.SelectLastSelectedEquipmentSlot();
        }
    }
}
