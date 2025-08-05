using UnityEngine;
using UnityEngine.TextCore.Text;

namespace LS
{
    public class InstantCharacterEffects : ScriptableObject
    {
        [Header("Effect ID")]
        public int instantEffectID;

        public virtual void ProcessEffect(CharacterManager character)
        {

        }

    }
}
