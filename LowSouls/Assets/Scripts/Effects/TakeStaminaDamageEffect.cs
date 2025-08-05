using UnityEngine;

namespace LS
{
    [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Stamina Damage")]

    public class TakeStaminaDamageEffect : InstantCharacterEffects
    {
        public float staminaDamage;
        public override void ProcessEffect(CharacterManager character)
        {
            CalculateStaminaDamage(character);
        }

        private void CalculateStaminaDamage(CharacterManager character)
        {
            if (character.IsOwner)
            {
                character.characterNetworkManager.currentStamina.Value -= staminaDamage;
            }
        }

    }
}
