using UnityEngine;

namespace LS {
    public class CharacterFootstepSFXMaker : MonoBehaviour
    {
        CharacterManager character;

        AudioSource audioSource;
        GameObject steppedOnObject;

        private bool hasTouchedGround = false;
        private bool hasPlayedFootstepSFX = false;
        [SerializeField] float distanceToGround = 0.05f;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            character = GetComponentInParent<CharacterManager>(); 
        }

        private void FixedUpdate()
        {
            CheckForFootstep();
        }

        private void CheckForFootstep()
        {
            if (character == null) return;

            if (character.characterNetworkManager.isMoving.Value)
                return;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, character.transform.TransformDirection(Vector3.down), out hit, distanceToGround, WorldUtilityManager.instance.GetEnvironmentLayers()))
            {
                hasTouchedGround = true;
                if (!hasPlayedFootstepSFX)
                    steppedOnObject = hit.transform.gameObject;
            }
            else
            {
                hasTouchedGround = false;
                hasPlayedFootstepSFX = false;
                steppedOnObject = null;
            }

            if (hasTouchedGround && !hasPlayedFootstepSFX)
            {
                hasPlayedFootstepSFX = true;
                PlayFootstepSFX();
            }
        }

        private void PlayFootstepSFX()
        {
            //audioSource.PlayOneShot(WorldSoundFXManager.instance.ChooseRandomFootstepSoundBasedOnGround(steppedOnObject, character));
            character.characterSoundFXManager.PlayFootstepsSFX();
        }
    }
}
