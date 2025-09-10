using UnityEngine;

namespace LS
{
    public class CharacterSoundFXManager : MonoBehaviour
    {
        private AudioSource audioSource;

        [Header("Damage Grunts")]
        [SerializeField] protected AudioClip[] damageGrunts;

        [Header("Attack Grunts")]
        [SerializeField] protected AudioClip[] attackGrunts;

        [Header("Footstep")]
        [SerializeField] protected AudioClip[] footsteps;

        protected virtual void Awake()
        {
            audioSource = GetComponent<AudioSource>();  
        }

        public void PlayRollSoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.rollSFX);
        }

        public void PlaySoundFX(AudioClip soundFX, float volume = 1, bool randomizePitch = true, float pitchRandom = 0.1f) 
        {
            audioSource.PlayOneShot(soundFX, volume);
            audioSource.pitch = 1;

            if (randomizePitch)
            {
                audioSource.pitch += Random.Range(-pitchRandom, pitchRandom);
            }
        }

        public virtual void PlayDamageGruntsSFX()
        {
            if (damageGrunts.Length > 0) 
            PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(damageGrunts));
        }

        public virtual void PlayAttackGruntsSFX()
        {
            if (attackGrunts.Length > 0)
                PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(attackGrunts));
        }

        public virtual void PlayFootstepsSFX()
        {
            if (footsteps.Length > 0)
                PlaySoundFX(WorldSoundFXManager.instance.ChooseRandomSFXFromArray(footsteps));
        }

        public virtual void PlayBlockSFX()
        {

        }
    }
}
