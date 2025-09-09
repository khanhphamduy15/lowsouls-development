using UnityEngine;

namespace LS
{
    public class AIBigfootSoundFXManager : CharacterSoundFXManager
    {
        [Header("Attack Whooshes")]
        public AudioClip[] armSwingWhooshes;

        [Header("Ground Impacts")]
        public AudioClip[] groundImpacts;

        public virtual void PlayGroundImpactSFX()
        {
            if (groundImpacts.Length > 0)
                PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(groundImpacts));
        }
    }
}