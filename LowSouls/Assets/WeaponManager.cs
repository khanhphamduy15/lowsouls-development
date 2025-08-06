using UnityEngine;

namespace LS
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] MeleeWeaponDamageCollider meleeDamageCollider;

        private void Awake()
        {
            meleeDamageCollider = GetComponentInChildren<MeleeWeaponDamageCollider>();
        }

        public void SetWeaponDamage(CharacterManager characterWieldingWeapon, WeaponItem weapon)
        {
            meleeDamageCollider.characterCausingDamage = characterWieldingWeapon;
            meleeDamageCollider.physicalDamage = weapon.physDmg;
            meleeDamageCollider.magicDamage = weapon.magicDmg;
            meleeDamageCollider.fireDamage = weapon.fireDmg;
            meleeDamageCollider.lightningDamage = weapon.lightningDmg;
            meleeDamageCollider.holyDamage = weapon.holyDmg;
        }
    }   
}
