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

        [Header("Attack Modifier")]
        //Weapon Modifier
        public float light_Attack_01_Modifier = 1.2f;
        public float heavy_Attack_01_Modifier = 1.6f;
        public float charge_Attack_01_Modifier = 2.2f;

        [Header("Stamina Costs Modifier")]
        public int baseStaminaCost = 20;
        public float lightAttackStaminaCostMultiplier = 0.8f;
        public float heavyAttackStaminaCostMultiplier = 1.2f;
        public float chargeAttackStaminaCostMultiplier = 2f;


        [Header("Actions")]
        public WeaponItemAction oh_RB_Action; //one handed right bumper action
        public WeaponItemAction oh_RT_Action; //one handed right trigger action (Charge)




    }
}
