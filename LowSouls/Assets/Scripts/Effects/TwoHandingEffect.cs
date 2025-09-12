using UnityEngine;

namespace LS
{
    [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Two Handing Effect")]
    public class TwoHandingEffect : StaticCharacterEffect
    {
        [SerializeField] int strengthGainedFromTwoHanding;

        public override void ProcessStaticEffect(CharacterManager character)
        {
            base.ProcessStaticEffect(character);
            if (character.IsOwner)
            {
                strengthGainedFromTwoHanding = Mathf.RoundToInt(character.characterNetworkManager.strength.Value / 2);
                Debug.Log("Strength gained: " + strengthGainedFromTwoHanding);
                character.characterNetworkManager.strengthModifier.Value += strengthGainedFromTwoHanding;
            }
        }

        public override void RemoveStaticEffect(CharacterManager character)
        {
            base.RemoveStaticEffect(character);
            if (character.IsOwner)
            {
                character.characterNetworkManager.strengthModifier.Value -= strengthGainedFromTwoHanding;
            }
        }
    }
}
