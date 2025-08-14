using UnityEngine;

namespace LS
{
    public class WeaponManager : MonoBehaviour
    {
        public MeleeWeaponDamageCollider meleeDamageCollider;

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
            meleeDamageCollider.holyDamage = weapon.holyDmg;
            meleeDamageCollider.lightningDamage = weapon.lightningDmg;

            meleeDamageCollider.light_Attack_01_Modifier = weapon.light_Attack_01_Modifier;
            //meleeDamageCollider.heavy_Attack_01_Modifier;
            //meleeDamageCollider.charge_Attack_01_Modifier
        }
    }   
}
