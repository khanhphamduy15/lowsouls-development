using UnityEngine;
using Unity.Netcode;

namespace LS
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager character;

        int vertical;
        int horizontal;

        [Header("Damage Animations")]
        public string hit_Forward_Medium_01 = "Hit_Forward_Medium_01";
        public string hit_Backward_Medium_01 = "Hit_Backward_Medium_01";
        public string hit_Left_Medium_01 = "Hit_Left_Medium_01";
        public string hit_Right_Medium_01 = "Hit_Right_Medium_01";

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();

            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }


        public void UpdateAnimatorMovementParameters(float horizontalMovement, float verticalMovement, bool isSprinting)
        {
            float horizontalAmount = horizontalMovement;
            float verticalAmount = verticalMovement;
            if (isSprinting)
            {
                verticalAmount = 2;
            }
            character.animator.SetFloat(horizontal, horizontalAmount, 0.1f, Time.deltaTime);
            character.animator.SetFloat(vertical, verticalAmount, 0.1f, Time.deltaTime);
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

        public virtual void PlayTargetAttackActionAnimation
            (AttackType attackType,
            string targetAnimation,
            bool isPerformingAction,
            bool applyRootMotion = true,
            bool canRotate = false,
            bool canMove = false)
        {
            Debug.Log("Playing " + targetAnimation);
            //keep track of last attack performed (for combos)
            //keep track of current attack type (light heavy etc)
            character.characterCombatManager.currentAttackType = attackType;
            //update animation set to current weapon animation
            character.animator.applyRootMotion = applyRootMotion;
            character.animator.CrossFade(targetAnimation, 0.2f);
            character.isPerformingAction = isPerformingAction;
            character.canRotate = canRotate;
            character.canMove = canMove;

            character.characterNetworkManager.NotifyTheServerOfAttackActionAnimationServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);
        }


    }
}

