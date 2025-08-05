using LS;
using UnityEngine;

namespace LS
{
    public class PlayerEffectsManager : CharacterEffectsManager
    {
        [Header("Debug Delete Later")]
        [SerializeField] InstantCharacterEffects effectsToTest;
        [SerializeField] bool processEffect = false;

        private void Update()
        {
            if (processEffect)
            {
                processEffect = false;
                InstantCharacterEffects effect = Instantiate(effectsToTest);
                ProcessInstantEffects(effect);
            }
        }
    }
}
