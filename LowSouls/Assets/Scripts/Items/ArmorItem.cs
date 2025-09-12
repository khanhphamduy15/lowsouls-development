using UnityEngine;

namespace LS {
    public class ArmorItem : EquipmentItem
    {
        [Header("Equipment Absorption Bonus")]
        public float physicalDamageAbsoprtion;
        public float magicDamageAbsoprtion;
        public float fireDamageAbsoprtion;
        public float lightningDamageAbsoprtion;
        public float holyDamageAbsoprtion;

        [Header("Equipment Resistance Bonus")]
        public float immunity;
        public float robustness;
        public float focus;
        public float vitality;

        [Header("Poise")]
        public float poise;

        //Armor Models

    }
}
