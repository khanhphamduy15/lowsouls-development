using UnityEngine;

namespace LS {
    public class BigfootHandDamageCollider : DamageCollider
    {
        [SerializeField] AIBossCharacterManager bossCharacter;

        protected override void Awake()
        {
            base.Awake();

            damageCollider = GetComponent<Collider>();
            bossCharacter = GetComponentInParent<AIBossCharacterManager>();
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
            damageEffect.angleHitFrom = Vector3.SignedAngle(bossCharacter.transform.forward, dmgTarget.transform.forward, Vector3.up);

            if (dmgTarget.IsOwner)
            {
                dmgTarget.characterNetworkManager.NotifyTheServerOfCharacterDamageServerRpc(
                    dmgTarget.NetworkObjectId,
                    bossCharacter.NetworkObjectId,
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
    }
}
