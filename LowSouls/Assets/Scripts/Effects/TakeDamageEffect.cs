using UnityEngine;

namespace LS
{
    [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Damage")]
    public class TakeDamageEffect : InstantCharacterEffects
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
            base.ProcessEffect(character);

            //if is dead, no additional dmg fx is processed
            if (character.isDead.Value) return;
            //check for "invulnerability"
            //calc dmg
            CalculateDamage(character);
            //check dmg taken direction

            //play dmg animation
            PlayDirectionalBasedDamageAnimation(character);

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

            //flat def subtract

            //apply all dmg after calc
            finalDamageDealt = Mathf.RoundToInt(physicalDamage + magicDamage + lightningDamage + fireDamage + holyDamage);
            if (finalDamageDealt <= 0)
            {
                finalDamageDealt = 1;
            }
            character.characterNetworkManager.currentHealth.Value -= finalDamageDealt;
            
            //calc poise dmg to determine character state (stunned or not)
        }

        private void PlayDamageVFX(CharacterManager character)
        {
            //fire dmg => fire particles
            character.characterEffectsManager.PlayBloodSplatterVFX(contactPoint);
        }

        private void PlayDamageSFX(CharacterManager character)
        {
            AudioClip physDmgSFX = WorldSoundFXManager.instance.ChooseRandomSFXFromArray(WorldSoundFXManager.instance.physDmgSFX);
            character.characterSoundFXManager.PlaySoundFX(physDmgSFX);
        }

        private void PlayDirectionalBasedDamageAnimation(CharacterManager character)
        {
            if (!character.IsOwner) return;

            //calc if poise is broken
            poiseIsBroken = true;
            if (angleHitFrom >= 145 && angleHitFrom <= 180)
            {
                //play front animation
                damageAnimation = character.characterAnimatorManager.GetRandomAnimationFromList(character.characterAnimatorManager.forward_Medium_Damage);
            }
            else if (angleHitFrom <= -145 && angleHitFrom >= -180)
            {
                //play front animation
                damageAnimation = character.characterAnimatorManager.GetRandomAnimationFromList(character.characterAnimatorManager.forward_Medium_Damage);
            }
            else if (angleHitFrom >= -45 && angleHitFrom <= 45)
            {
                //play back animation
                damageAnimation = character.characterAnimatorManager.GetRandomAnimationFromList(character.characterAnimatorManager.backward_Medium_Damage);
            }
            else if (angleHitFrom >= -144 && angleHitFrom <= -45)
            {
                //play left animation
                damageAnimation = character.characterAnimatorManager.GetRandomAnimationFromList(character.characterAnimatorManager.left_Medium_Damage); 
            }
            else if (angleHitFrom >= 45 && angleHitFrom <= 144)
            {
                //play right animation
                damageAnimation = character.characterAnimatorManager.GetRandomAnimationFromList(character.characterAnimatorManager.right_Medium_Damage);
            }
            //if poise is broken, play this animation
            if (poiseIsBroken)
            {
                character.characterAnimatorManager.lastDamageAnimationPlayed = damageAnimation;
                character.characterAnimatorManager.PlayTargetActionAnimation(damageAnimation, true);
            }
        }
    }
}
