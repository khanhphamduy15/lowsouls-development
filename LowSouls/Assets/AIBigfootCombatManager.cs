using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class AIBigfootCombatManager : AICharacterCombatManager
    {
        [Header("Damage Collider")]
        [SerializeField] BigfootHandDamageCollider rightHandDamageCollider;
        [SerializeField] BigfootHandDamageCollider leftHandDamageCollider;
        [SerializeField] Transform groundAttackHands;
        [SerializeField] float groundAttackRadius = 1.5f;


        [Header("Damage")]
        [SerializeField] int baseDamage = 25;
        [SerializeField] float attack01DamageModifier = 1.0f;
        [SerializeField] float attack02DamageModifier = 1.2f;
        [SerializeField] float attack03DamageModifier = 1.4f;
        [SerializeField] float attack04DamageModifier = 1.1f;
        [SerializeField] float attackGroundDamageModifier = 1.6f;
        [SerializeField] float groundAttackDamage = 25;

        public void SetAttack01Damage()
        {
            leftHandDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;
        }

        public void SetAttack02Damage()
        {
            rightHandDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
            leftHandDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
        }

        public void SetAttack03Damage()
        {
            rightHandDamageCollider.physicalDamage = baseDamage * attack03DamageModifier;
            leftHandDamageCollider.physicalDamage = baseDamage * attack03DamageModifier;
        }

        public void SetAttack04Damage()
        {
            rightHandDamageCollider.physicalDamage = baseDamage * attack04DamageModifier;
            leftHandDamageCollider.physicalDamage = baseDamage * attack04DamageModifier;
        }

        public void SetAttackGroundDamage()
        {
            rightHandDamageCollider.physicalDamage = baseDamage * attackGroundDamageModifier;
            leftHandDamageCollider.physicalDamage = baseDamage * attackGroundDamageModifier;
        }

        public void OpenRightHandCollider()
        {
            aiCharacter.characterSoundFXManager.PlayAttackGrunts();
            rightHandDamageCollider.EnableDamageCollider();
        }

        public void CloseRightHandCollider()
        {
            rightHandDamageCollider.DisableDamageCollider();
        }

        public void OpenLeftHandCollider()
        {
            aiCharacter.characterSoundFXManager.PlayAttackGrunts();
            leftHandDamageCollider.EnableDamageCollider();
        }

        public void CloseLeftHandCollider()
        {
            leftHandDamageCollider.DisableDamageCollider();
        }

        public void ActivateGroundAttack()
        {
            Collider[] colliders = Physics.OverlapSphere(groundAttackHands.position, groundAttackRadius, WorldUtilityManager.instance.GetCharacterLayers());
            List<CharacterManager> characterDamaged = new List<CharacterManager>();
            foreach (var collider in colliders)
            {
                CharacterManager character = collider.GetComponentInParent<CharacterManager>();

                if (character != null)
                {
                    if (characterDamaged.Contains(character)) continue;
                    characterDamaged.Add(character);

                    if (character.IsOwner)
                    {
                        TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
                        damageEffect.physicalDamage = groundAttackDamage;
                        damageEffect.poiseDamage = groundAttackDamage;

                        character.characterEffectsManager.ProcessInstantEffects(damageEffect);
                    }
                }
            }
        }
        public override void PivotTowardsTarget(AICharacterManager aiCharacter)
        {
            if (aiCharacter.isPerformingAction) return;

            if (viewableAngle >= 61 && viewableAngle <= 110)
            {
                aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Right_90", true);
            }
            else if (viewableAngle <= -61 && viewableAngle >= -110)
            {
                aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Left_90", true);
            }
            else if (viewableAngle >= 146 && viewableAngle <= 180)
            {
                aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Right_180", true);
            }
            else if (viewableAngle <= -146 && viewableAngle >= -180)
            {
                aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Left_180", true);
            }
        }
    }
}
