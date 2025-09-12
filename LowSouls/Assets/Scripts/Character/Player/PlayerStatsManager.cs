using UnityEngine;

namespace LS
{
    public class PlayerStatsManager : CharacterStatsManager
    {
        PlayerManager player;
        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        protected override void Start()
        {
            base.Start();

            CalculateHealthBasedOnVitalityLevel(player.playerNetworkManager.vitality.Value);
            CalculateStaminaBasedOnEnduranceLevel(player.playerNetworkManager.endurance.Value);
        }

        public void CalculateTotalArmorAbsorption()
        {
            //reset values to 0
            armorPhysicalDamageAbsoprtion = 0;
            armorMagicDamageAbsoprtion = 0;
            armorFireDamageAbsoprtion = 0;
            armorLightningDamageAbsoprtion = 0;
            armorHolyDamageAbsoprtion = 0;

            armorRobustness = 0;
            armorVitality = 0;
            armorImmunity = 0;
            armorFocus = 0;

            basePoiseDefense = 0;

            //head equipment
            if (player.playerInventoryManager.headEquipment != null)
            {
                //dmg absoprtion
                armorPhysicalDamageAbsoprtion += player.playerInventoryManager.headEquipment.physicalDamageAbsoprtion;
                armorMagicDamageAbsoprtion += player.playerInventoryManager.headEquipment.magicDamageAbsoprtion;
                armorFireDamageAbsoprtion += player.playerInventoryManager.headEquipment.fireDamageAbsoprtion;
                armorLightningDamageAbsoprtion += player.playerInventoryManager.headEquipment.lightningDamageAbsoprtion;
                armorHolyDamageAbsoprtion += player.playerInventoryManager.headEquipment.holyDamageAbsoprtion;

                //resistance
                armorRobustness += player.playerInventoryManager.headEquipment.robustness;
                armorVitality += player.playerInventoryManager.headEquipment.vitality;
                armorImmunity += player.playerInventoryManager.headEquipment.immunity;
                armorFocus += player.playerInventoryManager.headEquipment.focus;

                //poise
                basePoiseDefense += player.playerInventoryManager.headEquipment.poise;
            }

            //body equipment
            if (player.playerInventoryManager.bodyEquipment != null)
            {
                //dmg absoprtion
                armorPhysicalDamageAbsoprtion += player.playerInventoryManager.bodyEquipment.physicalDamageAbsoprtion;
                armorMagicDamageAbsoprtion += player.playerInventoryManager.bodyEquipment.magicDamageAbsoprtion;
                armorFireDamageAbsoprtion += player.playerInventoryManager.bodyEquipment.fireDamageAbsoprtion;
                armorLightningDamageAbsoprtion += player.playerInventoryManager.bodyEquipment.lightningDamageAbsoprtion;
                armorHolyDamageAbsoprtion += player.playerInventoryManager.bodyEquipment.holyDamageAbsoprtion;

                //resistance
                armorRobustness += player.playerInventoryManager.bodyEquipment.robustness;
                armorVitality += player.playerInventoryManager.bodyEquipment.vitality;
                armorImmunity += player.playerInventoryManager.bodyEquipment.immunity;
                armorFocus += player.playerInventoryManager.bodyEquipment.focus;

                //poise
                basePoiseDefense += player.playerInventoryManager.bodyEquipment.poise;
            }

            //leg equipment
            if (player.playerInventoryManager.legEquipment != null)
            {
                //dmg absoprtion
                armorPhysicalDamageAbsoprtion += player.playerInventoryManager.legEquipment.physicalDamageAbsoprtion;
                armorMagicDamageAbsoprtion += player.playerInventoryManager.legEquipment.magicDamageAbsoprtion;
                armorFireDamageAbsoprtion += player.playerInventoryManager.legEquipment.fireDamageAbsoprtion;
                armorLightningDamageAbsoprtion += player.playerInventoryManager.legEquipment.lightningDamageAbsoprtion;
                armorHolyDamageAbsoprtion += player.playerInventoryManager.legEquipment.holyDamageAbsoprtion;

                //resistance
                armorRobustness += player.playerInventoryManager.legEquipment.robustness;
                armorVitality += player.playerInventoryManager.legEquipment.vitality;
                armorImmunity += player.playerInventoryManager.legEquipment.immunity;
                armorFocus += player.playerInventoryManager.legEquipment.focus;

                //poise
                basePoiseDefense += player.playerInventoryManager.legEquipment.poise;
            }

            //hand equipment
            if (player.playerInventoryManager.handEquipment != null)
            {
                //dmg absoprtion
                armorPhysicalDamageAbsoprtion += player.playerInventoryManager.handEquipment.physicalDamageAbsoprtion;
                armorMagicDamageAbsoprtion += player.playerInventoryManager.handEquipment.magicDamageAbsoprtion;
                armorFireDamageAbsoprtion += player.playerInventoryManager.handEquipment.fireDamageAbsoprtion;
                armorLightningDamageAbsoprtion += player.playerInventoryManager.handEquipment.lightningDamageAbsoprtion;
                armorHolyDamageAbsoprtion += player.playerInventoryManager.handEquipment.holyDamageAbsoprtion;

                //resistance
                armorRobustness += player.playerInventoryManager.handEquipment.robustness;
                armorVitality += player.playerInventoryManager.handEquipment.vitality;
                armorImmunity += player.playerInventoryManager.handEquipment.immunity;
                armorFocus += player.playerInventoryManager.handEquipment.focus;

                //poise
                basePoiseDefense += player.playerInventoryManager.handEquipment.poise;
            }
        }
    }
}
