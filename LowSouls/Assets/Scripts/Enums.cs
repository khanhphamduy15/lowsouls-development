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
        LeftHand
    }

    public enum AttackType
    {
        LightAttack01,
        LightAttack02,
        HeavyAttack01,
        ChargeAttack01,
        ChargeAttack02

    }
}