using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class PlayerEquipmentManager : CharacterEquipmentManager
    {
        PlayerManager player;

        [Header("Weapon Model Instantiation Slots")]
        [HideInInspector] public WeaponModelInstantiationSlot rightHandWeaponSlot;
        [HideInInspector] public WeaponModelInstantiationSlot leftHandWeaponSlot;
        [HideInInspector] public WeaponModelInstantiationSlot leftHandShieldSlot;
        [HideInInspector] public WeaponModelInstantiationSlot backSlot;

        [Header("Weapon Managers")]
        WeaponManager rightWeaponManager;
        WeaponManager leftWeaponManager;

        [Header("Weapon Models")]
        [HideInInspector] public GameObject rightHandWeaponModel;
        [HideInInspector] public GameObject leftHandWeaponModel;

        [Header("General Equipment Models")]
        public GameObject halfHelmetObject;
        [HideInInspector] public GameObject[] halfHelmets;
        public GameObject hoodObject;
        [HideInInspector] public GameObject[] hoods;
        public GameObject faceCoverObject;
        [HideInInspector] public GameObject[] faceCovers;
        public GameObject helmetAccessoriesObject;
        [HideInInspector] public GameObject[] helmetAccessories;
        public GameObject backAccessoriesObject;
        [HideInInspector] public GameObject[] backAccessories;
        public GameObject hipAccessoriesObject;
        [HideInInspector] public GameObject[] hipAccessories;
        public GameObject rightShoulderObject;
        [HideInInspector] public GameObject[] rightShoulders;
        public GameObject rightElbowObject;
        [HideInInspector] public GameObject[] rightElbows;
        public GameObject rightKneeObject;
        [HideInInspector] public GameObject[] rightKnees;
        public GameObject leftShoulderObject;
        [HideInInspector] public GameObject[] leftShoulders;
        public GameObject leftElbowObject;
        [HideInInspector] public GameObject[] leftElbows;
        public GameObject leftKneeObject;
        [HideInInspector] public GameObject[] leftKnees;

        [Header("Equipment Models")]
        public GameObject fullHelmetObject;
        [HideInInspector] public GameObject[] fullHelmets;
        public GameObject fullBodyObject;
        [HideInInspector] public GameObject[] fullBodies;
        public GameObject rightUpperArmObject;
        [HideInInspector] public GameObject[] rightUpperArms;
        public GameObject rightLowerArmObject;
        [HideInInspector] public GameObject[] rightLowerArms;
        public GameObject rightHandObject;
        [HideInInspector] public GameObject[] rightHands;
        public GameObject leftUpperArmObject;
        [HideInInspector] public GameObject[] leftUpperArms;
        public GameObject leftLowerArmObject;
        [HideInInspector] public GameObject[] leftLowerArms;
        public GameObject leftHandObject;
        [HideInInspector] public GameObject[] leftHands;
        public GameObject hipObject;
        [HideInInspector] public GameObject[] hips;
        public GameObject rightLegObject;
        [HideInInspector] public GameObject[] rightLegs;
        public GameObject leftLegObject;
        [HideInInspector] public GameObject[] leftLegs;

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

            //full helmet
            List<GameObject> fullHelmetList = new List<GameObject>();
            foreach (Transform child in fullHelmetObject.transform)
            {
                fullHelmetList.Add(child.gameObject);
            }
            fullHelmets = fullHelmetList.ToArray();

            //half helmet
            List<GameObject> halfHelmetList = new List<GameObject>();
            foreach (Transform child in halfHelmetObject.transform)
            {
                halfHelmetList.Add(child.gameObject);
            }
            halfHelmets = halfHelmetList.ToArray();

            //hood
            List<GameObject> hoodList = new List<GameObject>();
            foreach (Transform child in hoodObject.transform)
            {
                hoodList.Add(child.gameObject);
            }
            hoods = hoodList.ToArray();

            //face cover
            List<GameObject> faceCoverList = new List<GameObject>();
            foreach (Transform child in faceCoverObject.transform)
            {
                faceCoverList.Add(child.gameObject);
            }
            faceCovers = faceCoverList.ToArray();

            //helmet accessories 
            List<GameObject> helmetAccessoriesList = new List<GameObject>();
            foreach (Transform child in helmetAccessoriesObject.transform)
            {
                helmetAccessoriesList.Add(child.gameObject);
            }
            helmetAccessories = helmetAccessoriesList.ToArray();

            //back accessories 
            List<GameObject> backAccessoriesList = new List<GameObject>();
            foreach (Transform child in backAccessoriesObject.transform)
            {
                backAccessoriesList.Add(child.gameObject);
            }
            backAccessories = backAccessoriesList.ToArray();

            //hip accessories 
            List<GameObject> hipAccessoriesList = new List<GameObject>();
            foreach (Transform child in hipAccessoriesObject.transform)
            {
                hipAccessoriesList.Add(child.gameObject);
            }
            hipAccessories = hipAccessoriesList.ToArray();

            //right shoulder 
            List<GameObject> rightShoulderList = new List<GameObject>();
            foreach (Transform child in rightShoulderObject.transform)
            {
                rightShoulderList.Add(child.gameObject);
            }
            rightShoulders = rightShoulderList.ToArray();

            //right elbow 
            List<GameObject> rightElbowList = new List<GameObject>();
            foreach (Transform child in rightElbowObject.transform)
            {
                rightElbowList.Add(child.gameObject);
            }
            rightElbows = rightElbowList.ToArray();

            //right knee 
            List<GameObject> rightKneeList = new List<GameObject>();
            foreach (Transform child in rightKneeObject.transform)
            {
                rightKneeList.Add(child.gameObject);
            }
            rightKnees = rightKneeList.ToArray();

            //left shoulder 
            List<GameObject> leftShoulderList = new List<GameObject>();
            foreach (Transform child in leftShoulderObject.transform)
            {
                leftShoulderList.Add(child.gameObject);
            }
            leftShoulders = leftShoulderList.ToArray();

            //left elbow 
            List<GameObject> leftElbowList = new List<GameObject>();
            foreach (Transform child in leftElbowObject.transform)
            {
                leftElbowList.Add(child.gameObject);
            }
            leftElbows = leftElbowList.ToArray();

            //left knee 
            List<GameObject> leftKneeList = new List<GameObject>();
            foreach (Transform child in leftKneeObject.transform)
            {
                leftKneeList.Add(child.gameObject);
            }
            leftKnees = leftKneeList.ToArray();

            //body
            List<GameObject> bodiesList = new List<GameObject>();
            foreach (Transform child in fullBodyObject.transform)
            {
                bodiesList.Add(child.gameObject);
            }
            fullBodies = bodiesList.ToArray();

            //right upper arm
            List<GameObject> rightUpperArmList = new List<GameObject>();
            foreach (Transform child in rightUpperArmObject.transform)
            {
                rightUpperArmList.Add(child.gameObject);
            }
            rightUpperArms = rightUpperArmList.ToArray();

            //right lower arm
            List<GameObject> rightLowerArmList = new List<GameObject>();
            foreach (Transform child in rightLowerArmObject.transform)
            {
                rightLowerArmList.Add(child.gameObject);
            }
            rightLowerArms = rightLowerArmList.ToArray();

            //right hand
            List<GameObject> rightHandList = new List<GameObject>();
            foreach (Transform child in rightHandObject.transform)
            {
                rightHandList.Add(child.gameObject);
            }
            rightHands = rightHandList.ToArray();

            //left upper arm
            List<GameObject> leftUpperArmList = new List<GameObject>();
            foreach (Transform child in leftUpperArmObject.transform)
            {
                leftUpperArmList.Add(child.gameObject);
            }
            leftUpperArms = leftUpperArmList.ToArray();

            //left lower arm
            List<GameObject> leftLowerArmList = new List<GameObject>();
            foreach (Transform child in leftLowerArmObject.transform)
            {
                leftLowerArmList.Add(child.gameObject);
            }
            leftLowerArms = leftLowerArmList.ToArray();

            //left hand
            List<GameObject> leftHandList = new List<GameObject>();
            foreach (Transform child in leftHandObject.transform)
            {
                leftHandList.Add(child.gameObject);
            }
            leftHands = leftHandList.ToArray();

            //hips
            List<GameObject> hipList = new List<GameObject>();
            foreach (Transform child in hipObject.transform)
            {
                hipList.Add(child.gameObject);
            }
            hips = hipList.ToArray();

            //right leg
            List<GameObject> rightLegList = new List<GameObject>();
            foreach (Transform child in rightLegObject.transform)
            {
                rightLegList.Add(child.gameObject);
            }
            rightLegs = rightLegList.ToArray();

            //left leg
            List<GameObject> leftLegList = new List<GameObject>();
            foreach (Transform child in leftLegObject.transform)
            {
                leftLegList.Add(child.gameObject);
            }
            leftLegs = leftLegList.ToArray();
        }

        //Quick Slot
        public void SwitchQuickSlotItem()
        {
            if (!player.IsOwner) 
                return;

            QuickSlotItem selectedItem = null;

            //go to the next item
            player.playerInventoryManager.quickSlotItemIndex += 1;

            //if index is out of bounds, go back to idx 0 (pos1)
            if (player.playerInventoryManager.quickSlotItemIndex < 0 || player.playerInventoryManager.quickSlotItemIndex > 2)
            {
                player.playerInventoryManager.quickSlotItemIndex = 0;
                //check if holding more than one weap
                float itemCount = 0;
                QuickSlotItem firstItem = null;
                int firstItemPosition = 0;

                for (int i = 0; i < player.playerInventoryManager.quickSlotItemInSlots.Length; i++)
                {
                    if (player.playerInventoryManager.quickSlotItemInSlots[i] != null)
                    {
                        itemCount += 1;
                        if (firstItem == null)
                        {
                            firstItem = player.playerInventoryManager.quickSlotItemInSlots[i];
                            firstItemPosition = i;
                        }
                    }
                }
                if (itemCount <= 1)
                {
                    player.playerInventoryManager.quickSlotItemIndex = -1;
                    selectedItem = null;
                    player.playerNetworkManager.currentQuickSlotItemID.Value = -1;
                }
                else
                {
                    player.playerInventoryManager.quickSlotItemIndex = firstItemPosition;
                    player.playerNetworkManager.currentQuickSlotItemID.Value = firstItem.itemID;
                }
                return;
            }

            if (player.playerInventoryManager.quickSlotItemInSlots[player.playerInventoryManager.quickSlotItemIndex] != null)
            {
                selectedItem = player.playerInventoryManager.quickSlotItemInSlots[player.playerInventoryManager.quickSlotItemIndex];
                //assign network weapon id to sync
                player.playerNetworkManager.currentQuickSlotItemID.Value = player.playerInventoryManager.quickSlotItemInSlots[player.playerInventoryManager.quickSlotItemIndex].itemID;
            }
            else
            {
                player.playerNetworkManager.currentQuickSlotItemID.Value = -1;
            }

            if (selectedItem == null && player.playerInventoryManager.quickSlotItemIndex <= 2)
            {
                SwitchQuickSlotItem();
            }
        }

        //Equipment
        public void LoadHeadEquipment(HeadEquipmentItem equipment)
        {
            //unload old models
            UnloadHeadEquipmentModels();

            if (equipment == null)
            {
                if (player.IsOwner)
                    player.playerNetworkManager.headEquipmentID.Value = -1;

                player.playerInventoryManager.headEquipment = null;
                return;
            }

            player.playerInventoryManager.headEquipment = equipment;

            switch (equipment.headEquipmentType)
            {
                case HeadEquipmentType.FullHelmet:
                    player.playerBodyManager.DisableHair();
                    player.playerBodyManager.DisableHead();
                    break;
                case HeadEquipmentType.HalfHelmet:
                    break;
                case HeadEquipmentType.Hood:
                    player.playerBodyManager.DisableHair();
                    break;
                case HeadEquipmentType.FaceCover:
                    player.playerBodyManager.DisableFacialHair();
                    break;
                default:
                    break;
            }

            foreach (var model in equipment.equipmentModels)
            {
                model.LoadModel(player);
            }

            //calc total armor absorption
            player.playerStatsManager.CalculateTotalArmorAbsorption();

            if (player.IsOwner)
                player.playerNetworkManager.headEquipmentID.Value = equipment.itemID;
        }

        public void UnloadHeadEquipmentModels()
        {
            foreach (var model in fullHelmets)
            {
                model.SetActive(false);
            }
            foreach (var model in halfHelmets)
            {
                model.SetActive(false);
            }
            foreach (var model in faceCovers)
            {
                model.SetActive(false);
            }
            foreach (var model in hoods)
            {
                model.SetActive(false);
            }
            foreach (var model in helmetAccessories)
            {
                model.SetActive(false);
            }

            //re enable head
            player.playerBodyManager.EnableHead();

            player.playerBodyManager.EnableHair();

        }

        public void LoadBodyEquipment(BodyEquipmentItem equipment)
        {
            //unload old models
            UnloadBodyEquipmentModels();

            if (equipment == null)
            {
                if (player.IsOwner)
                    player.playerNetworkManager.bodyEquipmentID.Value = -1;

                player.playerInventoryManager.bodyEquipment = null;
                return;
            }

            player.playerInventoryManager.bodyEquipment = equipment;

            player.playerBodyManager.DisableBody();

            foreach (var model in equipment.equipmentModels)
            {
                model.LoadModel(player);
            }

            //calc total armor absorption
            player.playerStatsManager.CalculateTotalArmorAbsorption();

            if (player.IsOwner)
                player.playerNetworkManager.bodyEquipmentID.Value = equipment.itemID;
        }

        public void UnloadBodyEquipmentModels()
        {
            foreach (var model in rightShoulders)
            {
                model.SetActive(false);
            }
            foreach (var model in rightElbows)
            {
                model.SetActive(false);
            }
            foreach (var model in leftShoulders)
            {
                model.SetActive(false);
            }
            foreach (var model in leftElbows)
            {
                model.SetActive(false);
            }
            foreach (var model in backAccessories)
            {
                model.SetActive(false);
            }
            foreach (var model in fullBodies)
            {
                model.SetActive(false);
            }
            foreach (var model in rightUpperArms)
            {
                model.SetActive(false);
            }
            foreach (var model in leftUpperArms)
            {
                model.SetActive(false);
            }
            player.playerBodyManager.EnableBody();
        }

        public void LoadLegEquipment(LegEquipmentItem equipment)
        {
            //unload old models
            UnloadLegEquipmentModels();

            if (equipment == null)
            {
                if (player.IsOwner)
                    player.playerNetworkManager.legEquipmentID.Value = -1;

                player.playerInventoryManager.legEquipment = null;
                return;
            }

            player.playerInventoryManager.legEquipment = equipment;

            player.playerBodyManager.DisableLowerBody();


            foreach (var model in equipment.equipmentModels)
            {
                model.LoadModel(player);
            }

            //calc total armor absorption
            player.playerStatsManager.CalculateTotalArmorAbsorption();

            if (player.IsOwner)
                player.playerNetworkManager.legEquipmentID.Value = equipment.itemID;
        }

        public void UnloadLegEquipmentModels()
        {
            foreach (var model in hips)
            {
                model.SetActive(false);
            }
            foreach (var model in rightKnees)
            {
                model.SetActive(false);
            }
            foreach (var model in leftKnees)
            {
                model.SetActive(false);
            }
            foreach (var model in leftLegs)
            {
                model.SetActive(false);
            }
            foreach (var model in rightLegs)
            {
                model.SetActive(false);
            }
            player.playerBodyManager.EnableLowerBody();
        }

        public void LoadHandEquipment(HandEquipmentItem equipment)
        {
            //unload old models
            UnloadHandEquipmentModels();

            if (equipment == null)
            {
                if (player.IsOwner)
                    player.playerNetworkManager.handEquipmentID.Value = -1;

                player.playerInventoryManager.handEquipment = null;
                return;
            }

            player.playerInventoryManager.handEquipment = equipment;

            player.playerBodyManager.DisableArms();


            foreach (var model in equipment.equipmentModels)
            {
                model.LoadModel(player);
            }

            //calc total armor absorption
            player.playerStatsManager.CalculateTotalArmorAbsorption();

            if (player.IsOwner)
                player.playerNetworkManager.handEquipmentID.Value = equipment.itemID;
        }

        public void UnloadHandEquipmentModels()
        {
            foreach (var model in rightLowerArms)
            {
                model.SetActive(false);
            }
            foreach (var model in leftLowerArms)
            {
                model.SetActive(false);
            }
            foreach (var model in rightHands)
            {
                model.SetActive(false);
            }
            foreach (var model in leftHands)
            {
                model.SetActive(false);
            }
            player.playerBodyManager.EnableArms();
        }

        public void EquipArmor()
        {
            LoadHeadEquipment(player.playerInventoryManager.headEquipment);
            LoadBodyEquipment(player.playerInventoryManager.bodyEquipment);
            LoadLegEquipment(player.playerInventoryManager.legEquipment);
            LoadHandEquipment(player.playerInventoryManager.handEquipment);
        }

        private void InitializeWeaponSlots()
        {
            WeaponModelInstantiationSlot[] weaponSlots = GetComponentsInChildren<WeaponModelInstantiationSlot>();
            foreach (var weaponSlot in weaponSlots)
            {
                if (weaponSlot.weaponSlot == WeaponModelSlot.RightHand)
                {
                    rightHandWeaponSlot = weaponSlot;
                }
                else if (weaponSlot.weaponSlot == WeaponModelSlot.LeftHandWeaponSlot)
                {
                    leftHandWeaponSlot = weaponSlot;
                }
                else if (weaponSlot.weaponSlot == WeaponModelSlot.LeftHandShieldSlot)
                {
                    leftHandShieldSlot = weaponSlot;
                }
                else if (weaponSlot.weaponSlot == WeaponModelSlot.BackSlot)
                {
                    backSlot = weaponSlot;
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
                rightHandWeaponSlot.UnloadWeapon();
                //get new weapon clone
                rightHandWeaponModel = Instantiate(player.playerInventoryManager.currentRightHandWeapon.weaponModel);
                rightHandWeaponSlot.PlaceWeaponModelIntoSlot(rightHandWeaponModel);
                rightWeaponManager = rightHandWeaponModel.GetComponent<WeaponManager>();
                rightWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
                player.playerAnimatorManager.UpdateAnimatorController(player.playerInventoryManager.currentRightHandWeapon.weaponAnimator);
            }
        }

        public void SwitchRightWeapon()
        {
            if (!player.IsOwner) 
                return;

            player.playerNetworkManager.isTwoHandingWeapon.Value = false;

            player.playerAnimatorManager.PlayTargetActionAnimation("Swap_Right_Weapon_01", false, false, true, true);

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
                            firstWeapon = player.playerInventoryManager.weaponInRightHandSlots[i];
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
                if (leftHandWeaponSlot.currentWeaponModel != null)
                    leftHandWeaponSlot.UnloadWeapon();

                if (leftHandShieldSlot.currentWeaponModel != null)
                    leftHandShieldSlot.UnloadWeapon();

                //get new weapon clone
                leftHandWeaponModel = Instantiate(player.playerInventoryManager.currentLeftHandWeapon.weaponModel);

                switch (player.playerInventoryManager.currentLeftHandWeapon.weaponModelType)
                {
                    case WeaponModelType.Weapon:
                        leftHandWeaponSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);
                        break;
                    case WeaponModelType.Shield:
                        leftHandShieldSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);
                        break;
                    default:
                        break;
                }

                leftWeaponManager = leftHandWeaponModel.GetComponent<WeaponManager>();
                leftWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
            }
        }

        //Two Hand
        public void UnTwoHandWeapon()
        {
            //update animator controller
            player.playerAnimatorManager.UpdateAnimatorController(player.playerInventoryManager.currentRightHandWeapon.weaponAnimator);

            //remove strength bonus

            //un-two hand

            //Left hand
            if (player.playerInventoryManager.currentLeftHandWeapon.weaponModelType == WeaponModelType.Weapon)
            {
                leftHandWeaponSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);
            }
            else if (player.playerInventoryManager.currentLeftHandWeapon.weaponModelType == WeaponModelType.Shield)
            {
                leftHandShieldSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);

            }

            //Right hand
            rightHandWeaponSlot.PlaceWeaponModelIntoSlot(rightHandWeaponModel);

            //refresh dmg collider
            rightWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
            leftWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
        }

        public void TwoHandRightWeapon()
        {
            //check untwohandable weapon
            if (player.playerInventoryManager.currentRightHandWeapon == WorldItemDatabase.instance.unarmedWeapon)
            {
                if (player.IsOwner)
                {
                    player.playerNetworkManager.isTwoHandingRightWeapon.Value = false;
                    player.playerNetworkManager.isTwoHandingWeapon.Value = false;
                }
                return;
            }
            //update animator
            player.playerAnimatorManager.UpdateAnimatorController(player.playerInventoryManager.currentRightHandWeapon.weaponAnimator);
            backSlot.PlaceWeaponModelInUnequippedSlot(leftHandWeaponModel, player.playerInventoryManager.currentLeftHandWeapon.weaponClass, player);

            //add strength bonus
            rightHandWeaponSlot.PlaceWeaponModelIntoSlot(rightHandWeaponModel);

            rightWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
            leftWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
        }

        public void TwoHandLeftWeapon()
        {
            //check untwohandable weapon
            if (player.playerInventoryManager.currentLeftHandWeapon == WorldItemDatabase.instance.unarmedWeapon)
            {
                if (player.IsOwner)
                {
                    player.playerNetworkManager.isTwoHandingLeftWeapon.Value = false;
                    player.playerNetworkManager.isTwoHandingWeapon.Value = false;
                }
                return;
            }
            //update animator
            player.playerAnimatorManager.UpdateAnimatorController(player.playerInventoryManager.currentLeftHandWeapon.weaponAnimator);
            backSlot.PlaceWeaponModelInUnequippedSlot(rightHandWeaponModel, player.playerInventoryManager.currentRightHandWeapon.weaponClass, player);

            //add strength bonus
            rightHandWeaponSlot.PlaceWeaponModelIntoSlot(leftHandWeaponModel);

            rightWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
            leftWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
        }

        public void SwitchLeftWeapon()
        {
            if (!player.IsOwner) 
                return;

            player.playerNetworkManager.isTwoHandingWeapon.Value = false;

            player.playerAnimatorManager.PlayTargetActionAnimation("Swap_Left_Weapon_01", false, false, true, true);

            WeaponItem selectedWeapon = null;

            //go to the next weapon
            player.playerInventoryManager.leftHandWeaponIndex += 1;

            //if index is out of bounds, go back to idx 0 (pos1)
            if (player.playerInventoryManager.leftHandWeaponIndex < 0 || player.playerInventoryManager.leftHandWeaponIndex > 2)
            {
                player.playerInventoryManager.leftHandWeaponIndex = 0;
                //check if holding more than one weap
                float weaponCount = 0;
                WeaponItem firstWeapon = null;
                int firstWeaponPosition = 0;

                for (int i = 0; i < player.playerInventoryManager.weaponInLeftHandSlots.Length; i++)
                {
                    if (player.playerInventoryManager.weaponInLeftHandSlots[i].itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
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
                    player.playerInventoryManager.leftHandWeaponIndex = -1;
                    selectedWeapon = WorldItemDatabase.instance.unarmedWeapon;
                    player.playerNetworkManager.currentLeftHandWeaponID.Value = selectedWeapon.itemID;
                }
                else
                {
                    player.playerInventoryManager.leftHandWeaponIndex = firstWeaponPosition;
                    player.playerNetworkManager.currentLeftHandWeaponID.Value = firstWeapon.itemID;
                }
                return;
            }

            foreach (WeaponItem weapon in player.playerInventoryManager.weaponInLeftHandSlots)
            {
                //check if not the unarmed weapon
                if (player.playerInventoryManager.weaponInLeftHandSlots[player.playerInventoryManager.leftHandWeaponIndex].itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                {
                    selectedWeapon = player.playerInventoryManager.weaponInLeftHandSlots[player.playerInventoryManager.leftHandWeaponIndex];
                    //assign network weapon id to sync
                    player.playerNetworkManager.currentLeftHandWeaponID.Value = player.playerInventoryManager.weaponInLeftHandSlots[player.playerInventoryManager.leftHandWeaponIndex].itemID;
                    return;
                }
            }

            if (selectedWeapon == null && player.playerInventoryManager.leftHandWeaponIndex <= 2)
            {
                SwitchLeftWeapon();
            }
        }

        //Damage Colliders
        public void OpenDamageCollider()
        {
            if (player.playerNetworkManager.isUsingRightHand.Value)
            {
                rightWeaponManager.meleeDamageCollider.EnableDamageCollider();
                player.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(player.playerInventoryManager.currentRightHandWeapon.whooshes));
            }
            else if (player.playerNetworkManager.isUsingLeftHand.Value)
            {
                leftWeaponManager.meleeDamageCollider.EnableDamageCollider();
                player.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(player.playerInventoryManager.currentLeftHandWeapon.whooshes));
            }

            //play sfx
        }

        public void CloseDamageCollider()
        {
            if (player.playerNetworkManager.isUsingRightHand.Value)
            {
                rightWeaponManager.meleeDamageCollider.DisableDamageCollider();
            }
            else if (player.playerNetworkManager.isUsingLeftHand.Value)
            {
                leftWeaponManager.meleeDamageCollider.DisableDamageCollider();
            }
        }

        //unhide weapon
        public void UnhideWeapon()
        {
            if (player.playerEquipmentManager.rightHandWeaponModel != null)
                player.playerEquipmentManager.rightHandWeaponModel.SetActive(true);

            if (player.playerEquipmentManager.leftHandWeaponModel != null)
                player.playerEquipmentManager.leftHandWeaponModel.SetActive(true);
        }
    }
}
