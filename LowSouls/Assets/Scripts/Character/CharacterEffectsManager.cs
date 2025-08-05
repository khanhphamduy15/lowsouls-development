using UnityEngine;

namespace LS
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        //instant effects

        //gradual effects

        //static effects
        CharacterManager character;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public void ProcessInstantEffects(InstantCharacterEffects effects)
        {
            effects.ProcessEffect(character);
        }

    }
}