using UnityEngine;

namespace LS
{
    public class MeleeWeaponDamageCollider : DamageCollider
    {
        [Header("Attacking Character")]
        public CharacterManager characterCausingDamage;

        [Header("Weapon Attack Modifiers")]
        public float light_Attack_01_Modifier;

        protected override void Awake()
        {
            base.Awake();
            if (damageCollider == null)
            {
                damageCollider = GetComponent<Collider>();
            }
            damageCollider.enabled = false;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            CharacterManager dmgTarget = other.GetComponentInParent<CharacterManager>();
            if (dmgTarget == characterCausingDamage) return;
            {

            }
            if (dmgTarget != null)
            {
                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                DamageTarget(dmgTarget);
            }
        }

        protected override void DamageTarget(CharacterManager dmgTarget)
        {
            //no more than 1 dmg per single atk
            if (characterDamaged.Contains(dmgTarget)) return;
            characterDamaged.Add(dmgTarget);

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
            damageEffect.physicalDamage = physicalDamage;
            damageEffect.magicDamage = magicDamage;
            damageEffect.fireDamage = fireDamage;
            damageEffect.holyDamage = holyDamage;
            damageEffect.lightningDamage = lightningDamage;
            damageEffect.contactPoint = contactPoint;
            damageEffect.angleHitFrom = Vector3.SignedAngle(characterCausingDamage.transform.forward, dmgTarget.transform.forward, Vector3.up);

            switch (characterCausingDamage.characterCombatManager.currentAttackType)
            {
                case AttackType.LightAttack01:
                    ApplyAttackDamageModifier(light_Attack_01_Modifier, damageEffect);
                    break;
                default:
                    break;
            }  

            if (characterCausingDamage.IsOwner)
            {
                dmgTarget.characterNetworkManager.NotifyTheServerOfCharacterDamageServerRpc(
                    dmgTarget.NetworkObjectId,
                    characterCausingDamage.NetworkObjectId,
                    damageEffect.physicalDamage,
                    damageEffect.magicDamage,
                    damageEffect.fireDamage,
                    damageEffect.holyDamage,
                    damageEffect.lightningDamage,
                    damageEffect.poiseDamage,
                    damageEffect.angleHitFrom,
                    damageEffect.contactPoint.x,
                    damageEffect.contactPoint.y,
                    damageEffect.contactPoint.z);
            }
        }

        private void ApplyAttackDamageModifier(float modifier, TakeDamageEffect damage)
        {
            damage.physicalDamage *= modifier;
            damage.magicDamage *= modifier;
            damage.fireDamage *= modifier;
            damage.holyDamage *= modifier;
            damage.lightningDamage *= modifier;
            damage.poiseDamage *= modifier;

            //if attack is a fully charged heavy, multiplier by full charge modifier after normal modifier have been calc
        }
    }
}
