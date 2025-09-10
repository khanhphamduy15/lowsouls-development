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
        LeftHandShieldSlot
    }

    public enum WeaponModelType
    {
        Weapon,
        Shield
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
}