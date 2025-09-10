using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class DamageCollider : MonoBehaviour
    {
        [Header("Collider")]
        [SerializeField] protected Collider damageCollider;

        [Header("Damage")]
        public float physicalDamage = 0;
        public float magicDamage = 0;
        public float fireDamage = 0;
        public float holyDamage = 0;
        public float lightningDamage = 0;

        [Header("Poise")]
        public float poiseDamage = 0;

        [Header("Contact Point")]
        protected Vector3 contactPoint;

        [Header("Character Damaged")]
        protected List<CharacterManager> characterDamaged = new List<CharacterManager>();

        [Header("Block")]
        protected Vector3 directionFromAttackToDamageTarget;
        protected float dotValueFromAttackToDamageTarget;

        protected virtual void Awake()
        {
            
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            CharacterManager dmgTarget = other.GetComponentInParent<CharacterManager>();
            if (dmgTarget != null)
            {
                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                //friendly fire check

                //blocking check
                CheckForBlock(dmgTarget);

                DamageTarget(dmgTarget);
            }
        }

        protected virtual void CheckForBlock(CharacterManager dmgTarget)
        {
            //if character has already been damaged
            if (characterDamaged.Contains(dmgTarget))
                return;

            GetBlockingDotValues(dmgTarget);

            //check if is blocking
            if (dmgTarget.characterNetworkManager.isBlocking.Value && dotValueFromAttackToDamageTarget > 0.3f)
            {
                characterDamaged.Add(dmgTarget);
                TakeBlockedDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeBlockedDamageEffect);
                damageEffect.physicalDamage = physicalDamage;
                damageEffect.fireDamage = fireDamage;
                damageEffect.magicDamage = magicDamage;
                damageEffect.holyDamage = holyDamage;
                damageEffect.lightningDamage = lightningDamage;
                damageEffect.poiseDamage = poiseDamage;
                damageEffect.staminaDamage = poiseDamage;
                damageEffect.contactPoint = contactPoint;

                //apply blocked character damage to target
                dmgTarget.characterEffectsManager.ProcessInstantEffects(damageEffect);
            }

            //check blocking direction
        }

        protected virtual void GetBlockingDotValues(CharacterManager dmgTarget)
        {
            directionFromAttackToDamageTarget = transform.position - dmgTarget.transform.position;
            dotValueFromAttackToDamageTarget = Vector3.Dot(directionFromAttackToDamageTarget, dmgTarget.transform.forward);
        }

        protected virtual void DamageTarget(CharacterManager dmgTarget)
        {
            //no more than 1 dmg per single atk
            if (characterDamaged.Contains(dmgTarget)) return;
            characterDamaged.Add(dmgTarget);

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
            damageEffect.physicalDamage = physicalDamage;
            damageEffect.fireDamage = fireDamage;
            damageEffect.magicDamage = magicDamage;
            damageEffect.holyDamage = holyDamage;
            damageEffect.lightningDamage = lightningDamage;
            damageEffect.poiseDamage = poiseDamage;
            damageEffect.contactPoint = contactPoint;

            dmgTarget.characterEffectsManager.ProcessInstantEffects(damageEffect);
        }

        public virtual void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public virtual void DisableDamageCollider()
        {
            damageCollider.enabled = false;
            characterDamaged.Clear();       //reset the characters that have been hit when reset collider, so they can be hit again 
        }

    }
}
