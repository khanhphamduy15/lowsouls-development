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

        [SerializeField] Image headEquipmentSlot;
        [SerializeField] Image bodyEquipmentSlot;
        [SerializeField] Image legEquipmentSlot;
        [SerializeField] Image handEquipmentSlot;


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
            RefreshEquipmentSlotIcon();
        }

        public void RefreshMenu()
        {
            ClearEquipmentInventory();
            RefreshEquipmentSlotIcon();
        }

        public void SelectLastSelectedEquipmentSlot()
        {
            Button lastSelectedButton = null;

            switch (currentSelectedEquipmentSlot)
            {
                case EquipmentSlotType.RightWeapon01:
                    lastSelectedButton = rightHandSlot01.GetComponentInParent<Button>();
                    break;
                case EquipmentSlotType.RightWeapon02:
                    lastSelectedButton = rightHandSlot02.GetComponentInParent<Button>();
                    break;
                case EquipmentSlotType.RightWeapon03:
                    lastSelectedButton = rightHandSlot03.GetComponentInParent<Button>();
                    break;
                case EquipmentSlotType.LeftWeapon01:
                    lastSelectedButton = leftHandSlot01.GetComponentInParent<Button>();
                    break;
                case EquipmentSlotType.LeftWeapon02:
                    lastSelectedButton = leftHandSlot02.GetComponentInParent<Button>();
                    break;
                case EquipmentSlotType.LeftWeapon03:
                    lastSelectedButton = leftHandSlot03.GetComponentInParent<Button>();
                    break;
                case EquipmentSlotType.Head:
                    lastSelectedButton = headEquipmentSlot.GetComponentInParent<Button>();
                    break;
                case EquipmentSlotType.Body:
                    lastSelectedButton = bodyEquipmentSlot.GetComponentInParent<Button>();
                    break;
                case EquipmentSlotType.Legs:
                    lastSelectedButton = legEquipmentSlot.GetComponentInParent<Button>();
                    break;
                case EquipmentSlotType.Hands:
                    lastSelectedButton = handEquipmentSlot.GetComponentInParent<Button>();
                    break;
                default:
                    break;
            }

            if (lastSelectedButton != null)
            {
                lastSelectedButton.Select();
                lastSelectedButton.OnSelect(null);
            }
        }

        public void CloseEquipmentManagerMenu()
        {
            PlayerUIManager.instance.menuWindowIsOpen = false;
            menu.SetActive(false);
        }

        private void RefreshEquipmentSlotIcon()
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

            //head
            HeadEquipmentItem headEquipmentItem = player.playerInventoryManager.headEquipment;

            if (headEquipmentItem != null)
            {
                headEquipmentSlot.enabled = true;
                headEquipmentSlot.sprite = headEquipmentItem.itemIcon;
            }
            else
            {
                headEquipmentSlot.enabled = false;
            }

            //body
            BodyEquipmentItem bodyEquipmentItem = player.playerInventoryManager.bodyEquipment;

            if (bodyEquipmentItem != null)
            {
                bodyEquipmentSlot.enabled = true;
                bodyEquipmentSlot.sprite = bodyEquipmentItem.itemIcon;
            }
            else
            {
                bodyEquipmentSlot.enabled = false;
            }

            //leg
            LegEquipmentItem legEquipmentItem = player.playerInventoryManager.legEquipment;

            if (legEquipmentItem != null)
            {
                legEquipmentSlot.enabled = true;
                legEquipmentSlot.sprite = legEquipmentItem.itemIcon;
            }
            else
            {
                legEquipmentSlot.enabled = false;
            }

            //hand
            HandEquipmentItem handEquipmentItem = player.playerInventoryManager.handEquipment;

            if (handEquipmentItem != null)
            {
                handEquipmentSlot.enabled = true;
                handEquipmentSlot.sprite = handEquipmentItem.itemIcon;
            }
            else
            {
                handEquipmentSlot.enabled = false;
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
                case EquipmentSlotType.Head:
                    LoadHeadEquipmentInventory();
                    break;
                case EquipmentSlotType.Body:
                    LoadBodyEquipmentInventory();
                    break;
                case EquipmentSlotType.Legs:
                    LoadLegEquipmentInventory();
                    break;
                case EquipmentSlotType.Hands:
                    LoadHandEquipmentInventory();
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
                RefreshMenu();
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

        private void LoadHeadEquipmentInventory()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();

            List<HeadEquipmentItem> equipmentsInInventory = new List<HeadEquipmentItem>();

            for (int i = 0; i < player.playerInventoryManager.itemsInInventory.Count; i++)
            {
                HeadEquipmentItem equipment = player.playerInventoryManager.itemsInInventory[i] as HeadEquipmentItem;

                if (equipment != null)
                    equipmentsInInventory.Add(equipment);
            }

            if (equipmentsInInventory.Count <= 0)
            {
                RefreshMenu();
                return;
            }

            bool hasSelectedFirstInventorySlot = false;

            for (int i = 0; i < equipmentsInInventory.Count; i++)
            {
                GameObject inventorySlotGameObject = Instantiate(equipmentInventorySlotPrefab, equipmentInventoryContentWindow);
                UI_EquipmentInventorySlot equipmentInventorySlot = inventorySlotGameObject.GetComponent<UI_EquipmentInventorySlot>();
                equipmentInventorySlot.AddItem(equipmentsInInventory[i]);

                if (!hasSelectedFirstInventorySlot)
                {
                    hasSelectedFirstInventorySlot = true;
                    Button inventorySlotButton = inventorySlotGameObject.GetComponent<Button>();
                    inventorySlotButton.Select();
                    inventorySlotButton.OnSelect(null);
                }
            }
        }

        private void LoadBodyEquipmentInventory()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();

            List<BodyEquipmentItem> equipmentsInInventory = new List<BodyEquipmentItem>();

            for (int i = 0; i < player.playerInventoryManager.itemsInInventory.Count; i++)
            {
                BodyEquipmentItem equipment = player.playerInventoryManager.itemsInInventory[i] as BodyEquipmentItem;

                if (equipment != null)
                    equipmentsInInventory.Add(equipment);
            }

            if (equipmentsInInventory.Count <= 0)
            {
                RefreshMenu();
                return;
            }

            bool hasSelectedFirstInventorySlot = false;

            for (int i = 0; i < equipmentsInInventory.Count; i++)
            {
                GameObject inventorySlotGameObject = Instantiate(equipmentInventorySlotPrefab, equipmentInventoryContentWindow);
                UI_EquipmentInventorySlot equipmentInventorySlot = inventorySlotGameObject.GetComponent<UI_EquipmentInventorySlot>();
                equipmentInventorySlot.AddItem(equipmentsInInventory[i]);

                if (!hasSelectedFirstInventorySlot)
                {
                    hasSelectedFirstInventorySlot = true;
                    Button inventorySlotButton = inventorySlotGameObject.GetComponent<Button>();
                    inventorySlotButton.Select();
                    inventorySlotButton.OnSelect(null);
                }
            }
        }

        private void LoadLegEquipmentInventory()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();

            List<LegEquipmentItem> equipmentsInInventory = new List<LegEquipmentItem>();

            for (int i = 0; i < player.playerInventoryManager.itemsInInventory.Count; i++)
            {
                LegEquipmentItem equipment = player.playerInventoryManager.itemsInInventory[i] as LegEquipmentItem;

                if (equipment != null)
                    equipmentsInInventory.Add(equipment);
            }

            if (equipmentsInInventory.Count <= 0)
            {
                RefreshMenu();
                return;
            }

            bool hasSelectedFirstInventorySlot = false;

            for (int i = 0; i < equipmentsInInventory.Count; i++)
            {
                GameObject inventorySlotGameObject = Instantiate(equipmentInventorySlotPrefab, equipmentInventoryContentWindow);
                UI_EquipmentInventorySlot equipmentInventorySlot = inventorySlotGameObject.GetComponent<UI_EquipmentInventorySlot>();
                equipmentInventorySlot.AddItem(equipmentsInInventory[i]);

                if (!hasSelectedFirstInventorySlot)
                {
                    hasSelectedFirstInventorySlot = true;
                    Button inventorySlotButton = inventorySlotGameObject.GetComponent<Button>();
                    inventorySlotButton.Select();
                    inventorySlotButton.OnSelect(null);
                }
            }
        }

        private void LoadHandEquipmentInventory()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();

            List<HandEquipmentItem> equipmentsInInventory = new List<HandEquipmentItem>();

            for (int i = 0; i < player.playerInventoryManager.itemsInInventory.Count; i++)
            {
                HandEquipmentItem equipment = player.playerInventoryManager.itemsInInventory[i] as HandEquipmentItem;

                if (equipment != null)
                    equipmentsInInventory.Add(equipment);
            }

            if (equipmentsInInventory.Count <= 0)
            {
                RefreshMenu();
                return;
            }

            bool hasSelectedFirstInventorySlot = false;

            for (int i = 0; i < equipmentsInInventory.Count; i++)
            {
                GameObject inventorySlotGameObject = Instantiate(equipmentInventorySlotPrefab, equipmentInventoryContentWindow);
                UI_EquipmentInventorySlot equipmentInventorySlot = inventorySlotGameObject.GetComponent<UI_EquipmentInventorySlot>();
                equipmentInventorySlot.AddItem(equipmentsInInventory[i]);

                if (!hasSelectedFirstInventorySlot)
                {
                    hasSelectedFirstInventorySlot = true;
                    Button inventorySlotButton = inventorySlotGameObject.GetComponent<Button>();
                    inventorySlotButton.Select();
                    inventorySlotButton.OnSelect(null);
                }
            }
        }

        public void SelectEquipmentSlot(int equipmentSlot)
        {
            currentSelectedEquipmentSlot = (EquipmentSlotType)equipmentSlot;
        }

        public void UnequipSelectedItem()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();
            Item unequippedItem;
            switch (currentSelectedEquipmentSlot)
            {
                case EquipmentSlotType.RightWeapon01:

                    unequippedItem = player.playerInventoryManager.weaponInRightHandSlots[0];
                    if (unequippedItem != null)
                    {
                        player.playerInventoryManager.weaponInRightHandSlots[0] = Instantiate(WorldItemDatabase.instance.unarmedWeapon);

                        if (unequippedItem.itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                            player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    }
                    if (player.playerInventoryManager.rightHandWeaponIndex == 0)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = WorldItemDatabase.instance.unarmedWeapon.itemID;

                    break;
                case EquipmentSlotType.RightWeapon02:

                    unequippedItem = player.playerInventoryManager.weaponInRightHandSlots[1];
                    if (unequippedItem != null)
                    {
                        player.playerInventoryManager.weaponInRightHandSlots[1] = Instantiate(WorldItemDatabase.instance.unarmedWeapon);

                        if (unequippedItem.itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                            player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    }
                    if (player.playerInventoryManager.rightHandWeaponIndex == 1)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = WorldItemDatabase.instance.unarmedWeapon.itemID;

                    break;
                case EquipmentSlotType.RightWeapon03:

                    unequippedItem = player.playerInventoryManager.weaponInRightHandSlots[2];
                    if (unequippedItem != null)
                    {
                        player.playerInventoryManager.weaponInRightHandSlots[2] = Instantiate(WorldItemDatabase.instance.unarmedWeapon);

                        if (unequippedItem.itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                            player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    }
                    if (player.playerInventoryManager.rightHandWeaponIndex == 2)
                        player.playerNetworkManager.currentRightHandWeaponID.Value = WorldItemDatabase.instance.unarmedWeapon.itemID;

                    break;
                case EquipmentSlotType.LeftWeapon01:

                    unequippedItem = player.playerInventoryManager.weaponInLeftHandSlots[0];
                    if (unequippedItem != null)
                    {
                        player.playerInventoryManager.weaponInLeftHandSlots[0] = Instantiate(WorldItemDatabase.instance.unarmedWeapon);

                        if (unequippedItem.itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                            player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    }
                    if (player.playerInventoryManager.leftHandWeaponIndex == 0)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = WorldItemDatabase.instance.unarmedWeapon.itemID;

                    break;
                case EquipmentSlotType.LeftWeapon02:

                    unequippedItem = player.playerInventoryManager.weaponInLeftHandSlots[1];
                    if (unequippedItem != null)
                    {
                        player.playerInventoryManager.weaponInLeftHandSlots[1] = Instantiate(WorldItemDatabase.instance.unarmedWeapon);

                        if (unequippedItem.itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                            player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    }
                    if (player.playerInventoryManager.leftHandWeaponIndex == 1)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = WorldItemDatabase.instance.unarmedWeapon.itemID;

                    break;
                case EquipmentSlotType.LeftWeapon03:

                    unequippedItem = player.playerInventoryManager.weaponInLeftHandSlots[2];
                    if (unequippedItem != null)
                    {
                        player.playerInventoryManager.weaponInLeftHandSlots[2] = Instantiate(WorldItemDatabase.instance.unarmedWeapon);

                        if (unequippedItem.itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                            player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    }
                    if (player.playerInventoryManager.leftHandWeaponIndex == 2)
                        player.playerNetworkManager.currentLeftHandWeaponID.Value = WorldItemDatabase.instance.unarmedWeapon.itemID;

                    break;
                case EquipmentSlotType.Head:

                    unequippedItem = player.playerInventoryManager.headEquipment;
                    
                    if (unequippedItem != null)
                        player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    player.playerInventoryManager.headEquipment = null;
                    player.playerEquipmentManager.LoadHeadEquipment(player.playerInventoryManager.headEquipment);
                    break;
                case EquipmentSlotType.Body:

                    unequippedItem = player.playerInventoryManager.bodyEquipment;
                    
                    if (unequippedItem != null)
                        player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    player.playerInventoryManager.bodyEquipment = null;
                    player.playerEquipmentManager.LoadBodyEquipment(player.playerInventoryManager.bodyEquipment);
                    break;
                case EquipmentSlotType.Legs:

                    unequippedItem = player.playerInventoryManager.legEquipment;
                    
                    if (unequippedItem != null)
                        player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    player.playerInventoryManager.legEquipment = null;
                    player.playerEquipmentManager.LoadLegEquipment(player.playerInventoryManager.legEquipment);
                    break;
                case EquipmentSlotType.Hands:

                    unequippedItem = player.playerInventoryManager.handEquipment;
                    
                    if (unequippedItem != null)
                        player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    player.playerInventoryManager.handEquipment = null;
                    player.playerEquipmentManager.LoadHandEquipment(player.playerInventoryManager.handEquipment);
                    break;
                default:
                    break;
            }

            //refresh
            RefreshMenu();
        }
    }
}
