using UnityEngine;

namespace LS
{
    public class PlayerEquipmentManager : CharacterEquipmentManager
    {
        PlayerManager player;

        public WeaponModelInstantiationSlot rightHandSlot;
        public WeaponModelInstantiationSlot leftHandSlot;

        [SerializeField] WeaponManager rightWeaponManager;
        [SerializeField] WeaponManager leftWeaponManager;

        public GameObject rightHandWeaponModel;
        public GameObject leftHandWeaponModel;

        protected override void Start()
        {
            base.Start();

            LoadWeaponOnBothHand();
        }

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
            InitializeWeaponSlots();
        }

        private void InitializeWeaponSlots()
        {
            WeaponModelInstantiationSlot[] weaponSlots = GetComponentsInChildren<WeaponModelInstantiationSlot>();
            foreach (var weaponSlot in weaponSlots)
            {
                if (weaponSlot.weaponSlot == WeaponModelSlot.RightHand)
                {
                    rightHandSlot = weaponSlot;
                }
                else if (weaponSlot.weaponSlot == WeaponModelSlot.LeftHand)
                {
                    leftHandSlot = weaponSlot;
                }
            }
        }

        public void LoadWeaponOnBothHand()
        {
            LoadRightWeapon();
            LoadLeftWeapon();
        }

        public void LoadRightWeapon()
        {
            if (player.playerInventoryManager.currentRightHandWeapon != null)
            {
                //remove old weapon clone
                rightHandSlot.UnloadWeapon();
                //get new weapon clone
                rightHandWeaponModel = Instantiate(player.playerInventoryManager.currentRightHandWeapon.weaponModel);
                rightHandSlot.LoadWeapon(rightHandWeaponModel);
                rightWeaponManager = rightHandWeaponModel.GetComponent<WeaponManager>();
                rightWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
            }
        }

        public void SwitchRightWeapon()
        {
            if (!player.IsOwner) return;
            player.playerAnimatorManager.PlayTargetActionAnimation("Swap_Right_Weapon_01", false, true, true, true);

            WeaponItem selectedWeapon = null;

            //go to the next weapon
            player.playerInventoryManager.rightHandWeaponIndex += 1;

            //if index is out of bounds, go back to idx 0 (pos1)
            if (player.playerInventoryManager.rightHandWeaponIndex < 0 ||player.playerInventoryManager.rightHandWeaponIndex > 2)
            {
                player.playerInventoryManager.rightHandWeaponIndex = 0;
                //check if holding more than one weap
                float weaponCount = 0;
                WeaponItem firstWeapon = null;
                int firstWeaponPosition = 0;

                for (int i = 0; i < player.playerInventoryManager.weaponInRightHandSlots.Length; i++)
                {
                    if (player.playerInventoryManager.weaponInRightHandSlots[i].itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                    {
                        weaponCount += 1;
                        if (firstWeapon == null)
                        {
                            firstWeapon = player.playerInventoryManager.weaponInLeftHandSlots[i];
                            firstWeaponPosition = i;
                        }
                    }
                }
                if (weaponCount <= 1)
                {
                    player.playerInventoryManager.rightHandWeaponIndex = -1;
                    selectedWeapon = WorldItemDatabase.instance.unarmedWeapon;
                    player.playerNetworkManager.currentRightHandWeaponID.Value = selectedWeapon.itemID;
                }
                else
                {
                    player.playerInventoryManager.rightHandWeaponIndex = firstWeaponPosition;
                    player.playerNetworkManager.currentRightHandWeaponID.Value = firstWeapon.itemID;
                }
                return;
            }

            foreach (WeaponItem weapon in player.playerInventoryManager.weaponInRightHandSlots)
            {
                //check if not the unarmed weapon
                if (player.playerInventoryManager.weaponInRightHandSlots[player.playerInventoryManager.rightHandWeaponIndex].itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                {
                    selectedWeapon = player.playerInventoryManager.weaponInRightHandSlots[player.playerInventoryManager.rightHandWeaponIndex];
                    //assign network weapon id to sync
                    player.playerNetworkManager.currentRightHandWeaponID.Value = player.playerInventoryManager.weaponInRightHandSlots[player.playerInventoryManager.rightHandWeaponIndex].itemID;
                    return;
                }
            }

            if (selectedWeapon == null && player.playerInventoryManager.rightHandWeaponIndex <= 2)
            {
                SwitchRightWeapon();
            }
        }
        public void LoadLeftWeapon()
        {
            if (player.playerInventoryManager.currentLeftHandWeapon != null)
            {
                //remove old weapon clone
                leftHandSlot.UnloadWeapon();
                //get new weapon clone
                leftHandWeaponModel = Instantiate(player.playerInventoryManager.currentLeftHandWeapon.weaponModel);
                leftHandSlot.LoadWeapon(leftHandWeaponModel);
                leftWeaponManager = leftHandWeaponModel.GetComponent<WeaponManager>();
                leftWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
            }
        }

        public void SwitchLeftWeapon()
        {

        }


    }
}
