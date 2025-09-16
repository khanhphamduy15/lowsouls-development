using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

namespace LS {
    public class PlayerUIEquipmentMenuManager : MonoBehaviour
    {
        [Header("Menu")]
        [SerializeField] GameObject menu;

        [Header("Weapon Slot")]
        [SerializeField] Image rightHandSlot01;
        private Button rightHandSlot01Button;
        [SerializeField] Image rightHandSlot02;
        private Button rightHandSlot02Button;
        [SerializeField] Image rightHandSlot03;
        private Button rightHandSlot03Button;


        [SerializeField] Image leftHandSlot01;
        private Button leftHandSlot01Button;
        [SerializeField] Image leftHandSlot02;
        private Button leftHandSlot02Button;
        [SerializeField] Image leftHandSlot03;
        private Button leftHandSlot03Button;


        [Header("Armor Slot")]
        [SerializeField] Image headEquipmentSlot;
        private Button headEquipmentSlotButton;
        [SerializeField] Image bodyEquipmentSlot;
        private Button bodyEquipmentSlotButton;
        [SerializeField] Image legEquipmentSlot;
        private Button legEquipmentSlotButton;
        [SerializeField] Image handEquipmentSlot;
        private Button handEquipmentSlotButton;


        [Header("Quick Slot")]
        [SerializeField] Image quickSlot01;
        [SerializeField] TextMeshProUGUI quickSlot01Count;
        private Button quickSlot01Button;
        [SerializeField] Image quickSlot02;
        [SerializeField] TextMeshProUGUI quickSlot02Count;
        private Button quickSlot02Button;
        [SerializeField] Image quickSlot03;
        [SerializeField] TextMeshProUGUI quickSlot03Count;
        private Button quickSlot03Button;

        [Header("Equipment Inventory")]
        public EquipmentSlotType currentSelectedEquipmentSlot;
        [SerializeField] GameObject equipmentInventoryWindow;
        [SerializeField] Transform equipmentInventoryContentWindow;
        [SerializeField] GameObject equipmentInventorySlotPrefab;
        [SerializeField] Item currentSelectedItem;

        private void Awake()
        {
            rightHandSlot01Button = rightHandSlot01.GetComponentInParent<Button>(true);
            rightHandSlot02Button = rightHandSlot02.GetComponentInParent<Button>(true);
            rightHandSlot03Button = rightHandSlot03.GetComponentInParent<Button>(true);

            leftHandSlot01Button = leftHandSlot01.GetComponentInParent<Button>(true);
            leftHandSlot02Button = leftHandSlot02.GetComponentInParent<Button>(true);
            leftHandSlot03Button = leftHandSlot03.GetComponentInParent<Button>(true);

            headEquipmentSlotButton = headEquipmentSlot.GetComponentInParent<Button>(true);
            bodyEquipmentSlotButton = bodyEquipmentSlot.GetComponentInParent<Button>(true);
            legEquipmentSlotButton = legEquipmentSlot.GetComponentInParent<Button>(true);
            handEquipmentSlotButton = handEquipmentSlot.GetComponentInParent<Button>(true);

            quickSlot01Button = quickSlot01.GetComponentInParent<Button>(true);
            quickSlot02Button = quickSlot02.GetComponentInParent<Button>(true);
            quickSlot03Button = quickSlot03.GetComponentInParent<Button>(true);
        }

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

        private void ToggleEquipmentButtons(bool isEnabled)
        {
            rightHandSlot01Button.enabled = isEnabled;
            rightHandSlot02Button.enabled = isEnabled;
            rightHandSlot03Button.enabled = isEnabled;

            leftHandSlot01Button.enabled = isEnabled;
            leftHandSlot02Button.enabled = isEnabled;
            leftHandSlot03Button.enabled = isEnabled;

            headEquipmentSlotButton.enabled = isEnabled;
            bodyEquipmentSlotButton.enabled = isEnabled;
            legEquipmentSlotButton.enabled = isEnabled;
            handEquipmentSlotButton.enabled = isEnabled;

            quickSlot01Button.enabled = isEnabled;
            quickSlot02Button.enabled = isEnabled;
            quickSlot03Button.enabled = isEnabled;
        }
        public void SelectLastSelectedEquipmentSlot()
        {
            Button lastSelectedButton = null;

            ToggleEquipmentButtons(true);

            switch (currentSelectedEquipmentSlot)
            {
                case EquipmentSlotType.RightWeapon01:
                    lastSelectedButton = rightHandSlot01Button;
                    break;
                case EquipmentSlotType.RightWeapon02:
                    lastSelectedButton = rightHandSlot02Button;
                    break;
                case EquipmentSlotType.RightWeapon03:
                    lastSelectedButton = rightHandSlot03Button;
                    break;
                case EquipmentSlotType.LeftWeapon01:
                    lastSelectedButton = leftHandSlot01Button;
                    break;
                case EquipmentSlotType.LeftWeapon02:
                    lastSelectedButton = leftHandSlot02Button;
                    break;
                case EquipmentSlotType.LeftWeapon03:
                    lastSelectedButton = leftHandSlot03Button;
                    break;
                case EquipmentSlotType.Head:
                    lastSelectedButton = headEquipmentSlotButton;
                    break;
                case EquipmentSlotType.Body:
                    lastSelectedButton = bodyEquipmentSlotButton;
                    break;
                case EquipmentSlotType.Legs:
                    lastSelectedButton = legEquipmentSlotButton;
                    break;
                case EquipmentSlotType.Hands:
                    lastSelectedButton = handEquipmentSlotButton;
                    break;
                case EquipmentSlotType.QuickSlot01:
                    lastSelectedButton = quickSlot01Button;
                    break;
                case EquipmentSlotType.QuickSlot02:
                    lastSelectedButton = quickSlot02Button;
                    break;
                case EquipmentSlotType.QuickSlot03:
                    lastSelectedButton = quickSlot03Button;
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

            //quick slot 01
            QuickSlotItem quickSlotItem01 = player.playerInventoryManager.quickSlotItemInSlots[0];

            if (quickSlotItem01 != null)
            {
                quickSlot01.enabled = true;
                quickSlot01.sprite = quickSlotItem01.itemIcon;
                if (quickSlotItem01.isConsumable)
                {
                    quickSlot01Count.enabled = true;
                    quickSlot01Count.text = quickSlotItem01.GetCurrentAmount(player).ToString();
                }
                else
                {
                    quickSlot01Count.enabled = false;
                }
            }
            else
            {
                quickSlot01.enabled = false;
                quickSlot01Count.enabled = false;
            }

            //quick slot 02
            QuickSlotItem quickSlotItem02 = player.playerInventoryManager.quickSlotItemInSlots[1];

            if (quickSlotItem02 != null)
            {
                quickSlot02.enabled = true;
                quickSlot02.sprite = quickSlotItem02.itemIcon;
                if (quickSlotItem02.isConsumable)
                {
                    quickSlot02Count.enabled = true;
                    quickSlot02Count.text = quickSlotItem02.GetCurrentAmount(player).ToString();
                }
                else
                {
                    quickSlot02Count.enabled = false;
                }
            }
            else
            {
                quickSlot02.enabled = false;
                quickSlot02Count.enabled = false;
            }

            //quick slot 03
            QuickSlotItem quickSlotItem03 = player.playerInventoryManager.quickSlotItemInSlots[2];

            if (quickSlotItem03 != null)
            {
                quickSlot03.enabled = true;
                quickSlot03.sprite = quickSlotItem03.itemIcon;
                if (quickSlotItem03.isConsumable)
                {
                    quickSlot03Count.enabled = true;
                    quickSlot03Count.text = quickSlotItem02.GetCurrentAmount(player).ToString();
                }
                else
                {
                    quickSlot03Count.enabled = false;
                }
            }
            else
            {
                quickSlot03.enabled = false;
                quickSlot03Count.enabled = false;
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
            ToggleEquipmentButtons(false);
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
                case EquipmentSlotType.QuickSlot01:
                    LoadQuickSlottInventory();
                    break;
                case EquipmentSlotType.QuickSlot02:
                    LoadQuickSlottInventory();
                    break;
                case EquipmentSlotType.QuickSlot03:
                    LoadQuickSlottInventory();
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
                case EquipmentSlotType.QuickSlot01:

                    unequippedItem = player.playerInventoryManager.quickSlotItemInSlots[0];
                    
                    if (unequippedItem != null)
                        player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    player.playerInventoryManager.quickSlotItemInSlots[0] = null;
                    if (player.playerInventoryManager.rightHandWeaponIndex == 0)
                        player.playerNetworkManager.currentQuickSlotItemID.Value = -1; 
                    break;
                case EquipmentSlotType.QuickSlot02:

                    unequippedItem = player.playerInventoryManager.quickSlotItemInSlots[1];
                    
                    if (unequippedItem != null)
                        player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    player.playerInventoryManager.quickSlotItemInSlots[1] = null;
                    if (player.playerInventoryManager.rightHandWeaponIndex == 0)
                        player.playerNetworkManager.currentQuickSlotItemID.Value = -1; 
                    break;
                case EquipmentSlotType.QuickSlot03:

                    unequippedItem = player.playerInventoryManager.quickSlotItemInSlots[2];
                    
                    if (unequippedItem != null)
                        player.playerInventoryManager.AddItemToInventory(unequippedItem);
                    player.playerInventoryManager.quickSlotItemInSlots[2] = null;
                    if (player.playerInventoryManager.rightHandWeaponIndex == 0)
                        player.playerNetworkManager.currentQuickSlotItemID.Value = -1; 
                    break;
                default:
                    break;
            }

            //refresh
            RefreshMenu();
        }

        private void LoadQuickSlottInventory()
        {
            PlayerManager player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<PlayerManager>();

            List<QuickSlotItem> itemsInInventory = new List<QuickSlotItem>();

            for (int i = 0; i < player.playerInventoryManager.itemsInInventory.Count; i++)
            {
                QuickSlotItem items = player.playerInventoryManager.itemsInInventory[i] as QuickSlotItem;

                if (items != null)
                    itemsInInventory.Add(items);
            }

            if (itemsInInventory.Count <= 0)
            {
                equipmentInventoryWindow.SetActive(false);
                ToggleEquipmentButtons(false);
                RefreshMenu();
                return;
            }

            bool hasSelectedFirstInventorySlot = false;

            for (int i = 0; i < itemsInInventory.Count; i++)
            {
                GameObject inventorySlotGameObject = Instantiate(equipmentInventorySlotPrefab, equipmentInventoryContentWindow);
                UI_EquipmentInventorySlot equipmentInventorySlot = inventorySlotGameObject.GetComponent<UI_EquipmentInventorySlot>();
                equipmentInventorySlot.AddItem(itemsInInventory[i]);

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
