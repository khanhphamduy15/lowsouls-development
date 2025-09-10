using UnityEngine;

namespace LS
{
    [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Blocked Damage")]

    public class TakeBlockedDamageEffect : InstantCharacterEffects
    {
        [Header("Character Causing Damage")]
        public CharacterManager characterCausingDamage;

        [Header("Damage")]
        public float physicalDamage = 0;
        public float fireDamage = 0;
        public float magicDamage = 0;
        public float lightningDamage = 0;
        public float holyDamage = 0;

        [Header("Final Damage")]
        private int finalDamageDealt = 0; //dmg takes after All calc

        [Header("Poise")]
        public float poiseDamage = 0;
        public bool poiseIsBroken = false; //broken = stunned

        //build ups<to do>
        [Header("Animation")]
        public bool playDamageAnimation = true;
        public bool manuallySelectDamageAnimation = false;
        public string damageAnimation;

        [Header("Sound FX")]
        public bool willPlayDamageSFX = true;
        public AudioClip elementDamageSoundFX; //used on top regular sfx

        [Header("Direction Damage Taken From")]
        public float angleHitFrom;
        public Vector3 contactPoint;            //blood fx instantiate point

        public override void ProcessEffect(CharacterManager character)
        {
            //check for "invulnerability"
            if (character.characterNetworkManager.isInvulnerable.Value) return;

            base.ProcessEffect(character);

            Debug.Log("HIT WAS BLOCKED");

            //if is dead, no additional dmg fx is processed
            if (character.isDead.Value) return;

            //calc dmg
            CalculateDamage(character);

            //check dmg taken direction

            //play dmg animation
            PlayDirectionalBasedBlockedDamageAnimation(character);

            //build ups dmg check

            //play dmg sfx
            PlayDamageSFX(character);

            //play dmg vfx (blood particle)
            PlayDamageVFX(character);
        }

        private void CalculateDamage(CharacterManager character)
        {
            if (!character.IsOwner) return;
            if (characterCausingDamage != null)
            {
                //dmg modifier check and modify

            }
            Debug.Log("Original phys dmg: " + physicalDamage);

            //flat def subtract

            physicalDamage -= (physicalDamage * (character.characterStatsManager.blockingPhysicalAbsorption / 100));
            magicDamage -= (magicDamage * (character.characterStatsManager.blockingMagicAbsorption / 100));
            lightningDamage -= (lightningDamage * (character.characterStatsManager.blockingLightningAbsorption / 100));
            fireDamage -= (fireDamage * (character.characterStatsManager.blockingFireAbsorption / 100));
            holyDamage -= (holyDamage * (character.characterStatsManager.blockingHolyAbsorption / 100));

            //apply all dmg after calc
            finalDamageDealt = Mathf.RoundToInt(physicalDamage + magicDamage + lightningDamage + fireDamage + holyDamage);
            if (finalDamageDealt <= 0)
            {
                finalDamageDealt = 1;
            }

            Debug.Log("Final phys dmg: " + physicalDamage);

            character.characterNetworkManager.currentHealth.Value -= finalDamageDealt;
            //calc poise dmg to determine character state (stunned or not)
        }

        private void PlayDamageVFX(CharacterManager character)
        {
            //fire dmg => fire particles
            
        }

        private void PlayDamageSFX(CharacterManager character)
        {


        }

        private void PlayDirectionalBasedBlockedDamageAnimation(CharacterManager character)
        {
            if (!character.IsOwner) 
                return;

            if (character.isDead.Value) 
                return;

            //calc attack intensity based on poise dmg
            DamageIntensity damageIntensity = WorldUtilityManager.instance.GetDamageIntensityBasedOnPoiseDamage(poiseDamage);

            switch (damageIntensity)
            {
                case DamageIntensity.Ping:
                    damageAnimation = "Block_Ping_01";
                    break;
                case DamageIntensity.Light:
                    damageAnimation = "Block_Light_01";
                    break;
                case DamageIntensity.Medium:
                    damageAnimation = "Block_Medium_01";
                    break;
                case DamageIntensity.Heavy:
                    damageAnimation = "Block_Heavy_01";
                    break;
                case DamageIntensity.Colossal:
                    damageAnimation = "Block_Colossal_01";
                    break;
                default:
                    break;
            }

            character.characterAnimatorManager.lastDamageAnimationPlayed = damageAnimation;
            character.characterAnimatorManager.PlayTargetActionAnimation(damageAnimation, true);
        }
    }
}
