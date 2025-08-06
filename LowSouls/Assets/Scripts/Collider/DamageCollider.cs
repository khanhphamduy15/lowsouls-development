using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class DamageCollider : MonoBehaviour
    {
        [Header("Collider")]
        protected Collider damageCollider;

        [Header("Damage")]
        public float physicalDamage = 0;
        public float fireDamage = 0;
        public float magicDamage = 0;
        public float lightningDamage = 0;
        public float holyDamage = 0;

        [Header("Contact Point")]
        protected Vector3 contactPoint;

        [Header("Character Damaged")]
        protected List<CharacterManager> characterDamaged = new List<CharacterManager>();

        private void OnTriggerEnter(Collider other)
        {
            CharacterManager dmgTarget = other.GetComponent<CharacterManager>();
            if (dmgTarget != null)
            {
                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                DamageTarget(dmgTarget);
            }
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
