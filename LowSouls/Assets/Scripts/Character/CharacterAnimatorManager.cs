using UnityEngine;
using Unity.Netcode;

namespace LS
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager character;

        float vertical;
        float horizontal;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }


        public void UpdateAnimatorMovementParameters(float horizontalMovement, float verticalMovement)
        {
            //float snappedHorizontal = 0;
            //float snappedVertical = 0;

            //#region Horizontal
            ////Clamp
            //if (horizontalMovement > 0 && horizontalMovement <= 0.5f)
            //{
            //    snappedHorizontal = 0.5f;
            //}
            //else if (horizontalMovement > 0.5f &&  horizontalMovement <= 1)
            //{
            //    snappedHorizontal = 1;
            //}
            //else if (horizontalMovement < 0 && horizontalMovement >= -0.5f)
            //{
            //    snappedHorizontal = -0.5f;
            //}
            //else if (horizontalMovement < -0.5f &&  horizontalMovement >= -1)
            //{
            //    snappedHorizontal = -1;
            //} else
            //{
            //    snappedHorizontal = 0;
            //}
            //#endregion

            //#region Vertical
            //if (verticalMovement > 0 && verticalMovement <= 0.5f)
            //{
            //    snappedVertical = 0.5f;
            //}
            //else if (verticalMovement > 0.5f && verticalMovement <= 1)
            //{
            //    snappedVertical = 1;
            //}
            //else if (verticalMovement < 0 && verticalMovement >= -0.5f)
            //{
            //    snappedVertical = -0.5f;
            //}
            //else if (verticalMovement < -0.5f && verticalMovement >= -1)
            //{
            //    snappedVertical = -1;
            //}
            //else
            //{
            //    snappedVertical = 0;
            //}

            character.animator.SetFloat("Horizontal", horizontalMovement, 0.1f, Time.deltaTime);
            character.animator.SetFloat("Vertical", verticalMovement, 0.1f, Time.deltaTime);
//            #endregion
        }

        public virtual void PlayTargetActionAnimation
            (string targetAnimation, 
            bool isPerformingAction, 
            bool applyRootMotion = true,
            bool canRotate = false, 
            bool canMove = false)
        {
            character.animator.applyRootMotion = applyRootMotion;
            character.animator.CrossFade(targetAnimation, 0.2f);
            //Can be used to stop character from attempting new actions (true if stunned etc,...)
            character.isPerformingAction = isPerformingAction;
            character.canRotate = canRotate;
            character.canMove = canMove;

            character.characterNetworkManager.NotifyTheServerOfActionAnimationServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);
        }
    }
}

