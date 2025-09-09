using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class AIBigfootCombatManager : AICharacterCombatManager
    {
        AIBigfootCharacterManager bigfootManager;

        [Header("Damage Collider")]
        [SerializeField] BigfootHandDamageCollider rightHandDamageCollider;
        [SerializeField] BigfootHandDamageCollider leftHandDamageCollider;
        [SerializeField] BigfootGroundAttackCollider groundAttackCollider;
        public float groundAttackRadius = 1.5f;

        [Header("Damage")]
        [SerializeField] int baseDamage = 25;
        [SerializeField] float attack01DamageModifier = 1.0f;
        [SerializeField] float attack02DamageModifier = 1.2f;
        [SerializeField] float attack03DamageModifier = 1.4f;
        [SerializeField] float attack04DamageModifier = 1.1f;
        [SerializeField] float attackGroundDamageModifier = 1.6f;
        public float groundAttackDamage = 25;

        [Header("VFX")]
        public GameObject bigfootImpactVFX;

        protected override void Awake()
        {
            base.Awake();
            bigfootManager = GetComponent<AIBigfootCharacterManager>();
        }

        public void SetAttack01Damage()
        {
            aiCharacter.characterSoundFXManager.PlayAttackGruntsSFX();
            leftHandDamageCollider.physicalDamage = baseDamage * attack01DamageModifier;
        }

        public void SetAttack02Damage()
        {
            aiCharacter.characterSoundFXManager.PlayAttackGruntsSFX();
            rightHandDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
            leftHandDamageCollider.physicalDamage = baseDamage * attack02DamageModifier;
        }

        public void SetAttack03Damage()
        {
            aiCharacter.characterSoundFXManager.PlayAttackGruntsSFX();
            rightHandDamageCollider.physicalDamage = baseDamage * attack03DamageModifier;
            leftHandDamageCollider.physicalDamage = baseDamage * attack03DamageModifier;
        }

        public void SetAttack04Damage()
        {
            aiCharacter.characterSoundFXManager.PlayAttackGruntsSFX();
            rightHandDamageCollider.physicalDamage = baseDamage * attack04DamageModifier;
            leftHandDamageCollider.physicalDamage = baseDamage * attack04DamageModifier;
        }

        public void SetAttackGroundDamage()
        {
            aiCharacter.characterSoundFXManager.PlayAttackGruntsSFX();
            rightHandDamageCollider.physicalDamage = baseDamage * attackGroundDamageModifier;
            leftHandDamageCollider.physicalDamage = baseDamage * attackGroundDamageModifier;
        }

        public void OpenRightHandCollider()
        {
            rightHandDamageCollider.EnableDamageCollider();
            bigfootManager.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(bigfootManager.bigfootSoundFXManager.armSwingWhooshes));
        }

        public void CloseRightHandCollider()
        {
            rightHandDamageCollider.DisableDamageCollider();
        }

        public void OpenLeftHandCollider()
        {
            leftHandDamageCollider.EnableDamageCollider();
            bigfootManager.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(bigfootManager.bigfootSoundFXManager.armSwingWhooshes));
        }

        public void CloseLeftHandCollider()
        {
            leftHandDamageCollider.DisableDamageCollider();
        }

        public void ActivateGroundAttack()
        {
            groundAttackCollider.GroundAttack();    
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
