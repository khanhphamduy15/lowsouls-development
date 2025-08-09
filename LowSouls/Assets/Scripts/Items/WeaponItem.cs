using UnityEngine;

namespace LS
{
    public class WeaponItem : Item
    {
        // Animator controller overrides (change attack animation based on weapons)
        [Header("Weapon Model")]
        public GameObject weaponModel;

        [Header("Weapon Requirements")]
        public int strengthREQ = 0;
        public int dexREQ = 0;
        public int faithREQ = 0;
        public int intelREQ = 0;
        public int hpREQ = 0;

        [Header("Weapon Base Damage")]
        public int physDmg = 0;
        public int magicDmg = 0;
        public int fireDmg = 0;
        public int holyDmg = 0;
        public int lightningDmg = 0;


        [Header("Weapon Base Poise Damage")]
        public float poiseDmg = 10;

        [Header("Stamina Costs")]
        public int baseStaminaCost = 20;

        [Header("Actions")]
        public WeaponItemAction oh_RB_Action; //one handed action



    }
}
