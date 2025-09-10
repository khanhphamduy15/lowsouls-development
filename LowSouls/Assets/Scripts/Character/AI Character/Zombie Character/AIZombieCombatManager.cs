using UnityEngine;

namespace LS
{
    public class AIZombieCombatManager : AICharacterCombatManager
    {
        [Header("Damage Collider")]
        [SerializeField] ZombieHandDamageCollider rightHandDamageCollider;
        [SerializeField] ZombieHandDamageCollider leftHandDamageCollider;

        [Header("Damage")]
        [SerializeField] int baseDamage = 25;
        [SerializeField] int basePoiseDamage = 25;
        [SerializeField] float attack01DamageModifier = 1.0f;
        [SerializeField] float attack02DamageModifier = 1.2f;
        [SerializeField] float attack03DamageModifier = 1.4f;

        public void SetAttack01Damage()
        {
            rightHandDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;
            leftHandDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;
            rightHandDamageCollider.poiseDamage = basePoiseDamage * attack01DamageModifier;
            leftHandDamageCollider.poiseDamage = basePoiseDamage * attack01DamageModifier;
        }

        public void SetAttack02Damage()
        {
            rightHandDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
            rightHandDamageCollider.poiseDamage = basePoiseDamage * attack02DamageModifier;
        }

        public void SetAttack03Damage()
        {
            leftHandDamageCollider.physicalDamage = baseDamage * attack03DamageModifier;
            leftHandDamageCollider.poiseDamage = basePoiseDamage * attack03DamageModifier;
        }

        public void OpenRightHandCollider()
        {
            aiCharacter.characterSoundFXManager.PlayAttackGruntsSFX();
            rightHandDamageCollider.EnableDamageCollider(); 
        }

        public void CloseRightHandCollider()
        {
            rightHandDamageCollider.DisableDamageCollider();
        }

        public void OpenLeftHandCollider()
        {
            aiCharacter.characterSoundFXManager.PlayAttackGruntsSFX();
            leftHandDamageCollider.EnableDamageCollider();
        }
        public void CloseLeftHandCollider()
        {
            leftHandDamageCollider.DisableDamageCollider();
        }
    }
}
