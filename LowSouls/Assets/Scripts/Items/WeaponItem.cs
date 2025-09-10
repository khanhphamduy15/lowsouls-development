using UnityEngine;

namespace LS
{
    public class WeaponItem : Item
    {
        // Animator controller overrides (change attack animation based on weapons)
        [Header("Animations")]
        public AnimatorOverrideController weaponAnimator;

        [Header("Model Instantiation")]
        public WeaponModelType weaponModelType;

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
        public float light_Attack_01_Modifier = 1.0f;
        public float light_Attack_02_Modifier = 1.2f;

        public float heavy_Attack_01_Modifier = 1.5f;
        public float heavy_Attack_02_Modifier = 1.7f;

        public float charge_Attack_01_Modifier = 2.0f;
        public float charge_Attack_02_Modifier = 2.2f;

        public float running_Attack_01_Modifier = 1.5f;

        public float rolling_Attack_01_Modifier = 2.5f;



        [Header("Stamina Costs Modifier")]
        public int baseStaminaCost = 20;
        public float lightAttackStaminaCostMultiplier = 0.8f;
        public float heavyAttackStaminaCostMultiplier = 1.2f;
        public float chargeAttackStaminaCostMultiplier = 2f;
        public float runningAttackStaminaCostMultiplier = 1;
        public float rollingAttackStaminaCostMultiplier = 1.5f;

        [Header("Blocking Absorption")]
        public float physicalDmgAbsorption = 50;
        public float fireDmgAbsorption = 50;
        public float magicDmgAbsorption = 50;
        public float holyDmgAbsorption = 50;
        public float lightningDmgAbsorption = 50;
        public float stability = 50; //reduce stamina loss from blocking

        [Header("Actions")]
        public WeaponItemAction oh_RB_Action; //one handed right bumper action
        public WeaponItemAction oh_RT_Action; //one handed right trigger action (Charge)

        public WeaponItemAction oh_LB_Action; //one handed left bumper action


        [Header("SFX")]
        public AudioClip[] whooshes;
        public AudioClip[] blocking;



    }
}
