using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

namespace LS {
    public class PlayerUIEquipmentMenuManager : MonoBehaviour
    {
        [Header("Menu")]
        [SerializeField] GameObject menu;

        [Header("Weapon Slot")]
        [SerializeField] Image rightHandSlot01;
        [SerializeField] Image rightHandSlot02;
        [SerializeField] Image rightHandSlot03;

        [SerializeField] Image leftHandSlot01;
        [SerializeField] Image leftHandSlot02;
        [SerializeField] Image leftHandSlot03;

        [Header("Equipment Inventory")]
        public EquipmentSlotType currentSelectedEquipmentSlot;
        [SerializeField] GameObject equipmentInventoryWindow;
        [SerializeField] Transform equipmentInventoryContentWindow;
        [SerializeField] GameObject equipmentInventorySlotPrefab;
        [SerializeField] Item currentSelectedItem;

        public void OpenEquipmentManagerMenu()
        {
            PlayerUIManager.instance.menuWindowIsOpen = true;
            menu.SetActive(true);
            equipmentInventoryWindow.SetActive(false);
            ClearEquipmentInventory();
            RefreshWeaponSlotIcon();
        }

        public void CloseEquipmentManagerMenu()
        {
            PlayerUIManager.instance.menuWindowIsOpen = false;
            menu.SetActive(false);
        }

        private void RefreshWeaponSlotIcon()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();

            //right weapon 01
            WeaponItem rightWeapon01 = player.playerInventoryManager.weaponInRightHandSlots[0];

            if (rightWeapon01.itemIcon != null)
            {
                rightHandSlot01.enabled = true;
                rightHandSlot01.sprite = rightWeapon01.itemIcon;
            }
            else
            {
                rightHandSlot01.enabled = false;
            }

            //right weapon 02
            WeaponItem rightWeapon02 = player.playerInventoryManager.weaponInRightHandSlots[1];

            if (rightWeapon02.itemIcon != null)
            {
                rightHandSlot02.enabled = true;
                rightHandSlot02.sprite = rightWeapon02.itemIcon;
            }
            else
            {
                rightHandSlot02.enabled = false;
            }

            //right weapon 03
            WeaponItem rightWeapon03 = player.playerInventoryManager.weaponInRightHandSlots[2];

            if (rightWeapon03.itemIcon != null)
            {
                rightHandSlot03.enabled = true;
                rightHandSlot03.sprite = rightWeapon03.itemIcon;
            }
            else
            {
                rightHandSlot03.enabled = false;
            }

            //left weapon 01
            WeaponItem leftWeapon01 = player.playerInventoryManager.weaponInLeftHandSlots[0];

            if (leftWeapon01.itemIcon != null)
            {
                leftHandSlot01.enabled = true;
                leftHandSlot01.sprite = leftWeapon01.itemIcon;
            }
            else
            {
                leftHandSlot01.enabled = false;
            }

            //left weapon 02
            WeaponItem leftWeapon02 = player.playerInventoryManager.weaponInLeftHandSlots[1];

            if (leftWeapon02.itemIcon != null)
            {
                leftHandSlot02.enabled = true;
                leftHandSlot02.sprite = leftWeapon02.itemIcon;
            }
            else
            {
                leftHandSlot02.enabled = false;
            }

            //left weapon 03
            WeaponItem leftWeapon03 = player.playerInventoryManager.weaponInLeftHandSlots[2];

            if (leftWeapon03.itemIcon != null)
            {
                leftHandSlot03.enabled = true;
                leftHandSlot03.sprite = leftWeapon03.itemIcon;
            }
            else
            {
                leftHandSlot03.enabled = false;
            }
        }

        private void ClearEquipmentInventory()
        {
            foreach (Transform item in equipmentInventoryContentWindow)
            {
                Destroy(item.gameObject);
            }
        }
        public void LoadEquipmentInventory()
        {
            equipmentInventoryWindow.SetActive(true);

            switch (currentSelectedEquipmentSlot)
            {
                case EquipmentSlotType.RightWeapon01:
                    LoadWeaponInventory();
                    break;
                case EquipmentSlotType.RightWeapon02:
                    LoadWeaponInventory();
                    break;
                case EquipmentSlotType.RightWeapon03:
                    LoadWeaponInventory();
                    break;
                case EquipmentSlotType.LeftWeapon01:
                    LoadWeaponInventory();
                    break;
                case EquipmentSlotType.LeftWeapon02:
                    LoadWeaponInventory();
                    break;
                case EquipmentSlotType.LeftWeapon03:
                    LoadWeaponInventory();
                    break;
                default:
                    break;
            }
        }

        private void LoadWeaponInventory()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();

            List<WeaponItem> weaponsInInventory = new List<WeaponItem>();

            for (int i = 0; i < player.playerInventoryManager.itemsInInventory.Count; i++)
            {
                WeaponItem weapon = player.playerInventoryManager.itemsInInventory[i] as WeaponItem;

                if (weapon != null)
                    weaponsInInventory.Add(weapon);
            }

            if (weaponsInInventory.Count <= 0)
            {
                OpenEquipmentManagerMenu();
                return;
            }

            bool hasSelectedFirstInventorySlot = false;

            for (int i = 0; i < weaponsInInventory.Count; i++)
            {
                GameObject inventorySlotGameObject = Instantiate(equipmentInventorySlotPrefab, equipmentInventoryContentWindow);
                UI_EquipmentInventorySlot  equipmentInventorySlot = inventorySlotGameObject.GetComponent<UI_EquipmentInventorySlot>();
                equipmentInventorySlot.AddItem(weaponsInInventory[i]);

                if (!hasSelectedFirstInventorySlot)
                {
                    hasSelectedFirstInventorySlot = true;
                    Button inventorySlotButton = inventorySlotGameObject.GetComponent<Button>();
                    inventorySlotButton.Select();
                    inventorySlotButton.OnSelect(null);
                }
            }
        }
    }
}
