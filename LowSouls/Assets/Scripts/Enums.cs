using UnityEngine;

namespace LS
{
    public class Enums : MonoBehaviour
    {

    }

    public enum CharacterSlot
    {
        CharacterSlot_01,
        CharacterSlot_02,
        CharacterSlot_03,
        CharacterSlot_04,
        CharacterSlot_05,
        NO_SLOT
    }

    public enum WeaponModelSlot
    {
        RightHand,
        LeftHandWeaponSlot,
        LeftHandShieldSlot,
        BackSlot
    }

    public enum WeaponModelType
    {
        Weapon,
        Shield
    }

    public enum EquipmentModelType
    {
        FullHelmet,
        HalfHelmet,
        HelmetAccessories,
        Hood,
        FaceCover,
        Torso,
        Back,
        RightShoulder,
        RightUpperArm,
        RightElbow,
        RightLowerArm,
        RightHand,
        LeftShoulder,
        LeftUpperArm,
        LeftElbow,
        LeftLowerArm,
        LeftHand,
        Hips,
        HipsAttachment,
        RightLeg,
        RightKnee,
        LeftLeg,
        LeftKnee
    }

    public enum EquipmentSlotType
    {
        RightWeapon01,
        RightWeapon02,
        RightWeapon03,
        LeftWeapon01,
        LeftWeapon02,
        LeftWeapon03,
        Head,
        Body,
        Legs,
        Hands
        
    }

    public enum HeadEquipmentType
    {
        FullHelmet,
        HalfHelmet,
        Hood,
        FaceCover
    }

    public enum WeaponClass
    {
        StraightSword,
        Claymore,
        MediumShield,
        Fist
    }

    public enum AttackType
    {
        LightAttack01,
        LightAttack02,
        HeavyAttack01,
        HeavyAttack02,
        ChargeAttack01,
        ChargeAttack02,
        RunningAttack01,
        RollingAttack01
    }

    public enum DamageIntensity
    {
        Ping,
        Light,
        Medium,
        Heavy,
        Colossal
    }

    public enum CharacterGroup
    {
        Team01,
        Team02,
    }

    public enum ItemPickUpType
    {
        WorldSpawn,
        CharacterDrop
    }
}