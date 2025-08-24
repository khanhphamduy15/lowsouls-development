using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class BigfootGroundAttackCollider : DamageCollider
    {
        [SerializeField] AIBigfootCharacterManager bigfootCharacterManager;

        protected override void Awake()
        {
            base.Awake();
            bigfootCharacterManager = GetComponentInParent<AIBigfootCharacterManager>();
        }
        public void GroundAttack()
        {
            GameObject groundAttackVFX = Instantiate(bigfootCharacterManager.bigfootCombatManager.bigfootImpactVFX, transform);

            Collider[] colliders = Physics.OverlapSphere(transform.position, bigfootCharacterManager.bigfootCombatManager.groundAttackRadius, WorldUtilityManager.instance.GetCharacterLayers());
            List<CharacterManager> characterDamaged = new List<CharacterManager>();
            foreach (var collider in colliders)
            {
                CharacterManager character = collider.GetComponentInParent<CharacterManager>();

                if (character != null)
                {
                    if (characterDamaged.Contains(character)) continue;

                    if (character == bigfootCharacterManager) continue;

                    characterDamaged.Add(character);

                    if (character.IsOwner)
                    {
                        TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
                        damageEffect.physicalDamage = bigfootCharacterManager.bigfootCombatManager.groundAttackDamage;
                        damageEffect.poiseDamage = bigfootCharacterManager.bigfootCombatManager.groundAttackDamage;

                        character.characterEffectsManager.ProcessInstantEffects(damageEffect);
                    }
                }
            }
        }
    }
}
