using UnityEngine;

namespace LS
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager player;
        [HideInInspector] public float verticalMovement;
        [HideInInspector] public float horizontalMovement;
        [HideInInspector] public float moveAmount;

        [Header("Movement Settings")]
        [SerializeField] float walkingSpeed = 2f;
        [SerializeField] float runningSpeed = 5f;
        [SerializeField] float rotationSpeed = 15;
        [SerializeField] float sprintingSpeed = 8f;
        [SerializeField] int sprintingStaminaCost = 2;
        private Vector3 moveDirection;
        private Vector3 targetRotationDirection;

        [Header("Jump")]
        [SerializeField] float jumpHeight = 3;
        [SerializeField] int jumpStaminaCost = 8;
        private Vector3 jumpDirection;

        [Header("Dodge")]
        private Vector3 rollDirection;
        [SerializeField] float rollStaminaCost = 12;
        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        protected override void Update()
        {
            base.Update();
            if (player.IsOwner)
            {
                player.characterNetworkManager.verticalMovement.Value = verticalMovement;
                player.characterNetworkManager.horizontalMovement.Value = horizontalMovement;
                player.characterNetworkManager.moveAmount.Value = moveAmount;
            }
            else
            {
                verticalMovement = player.characterNetworkManager.verticalMovement.Value;
                horizontalMovement = player.characterNetworkManager.horizontalMovement.Value;
                moveAmount = player.characterNetworkManager.moveAmount.Value;

                player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.playerNetworkManager.isSprinting.Value);
            }
        }
        public void HandleAllMovement()
        {
            //Ground movements
            HandleGroundedMovement();

            //Rotation
            HandleRotation();

            //Jumping 
            HandleJumpingMovement();
            
        }

        private void GetMovementValues()
        {
            verticalMovement = PlayerInputManager.instance.verticalInput;
            horizontalMovement = PlayerInputManager.instance.horizontalInput;
            moveAmount = PlayerInputManager.instance.moveAmount;

            //Clamp movement
        } 

        private void HandleGroundedMovement()
        {
            if (!player.canMove) return;
            GetMovementValues();
            //Move direction based on camera facing perspective (goc cam) & movement input
            moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
            moveDirection = moveDirection + PlayerCamera.instance.transform.right * horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if (player.playerNetworkManager.isSprinting.Value)
            {
                player.characterController.Move(moveDirection * sprintingSpeed * Time.deltaTime);
            }
            else
            {
                if (PlayerInputManager.instance.moveAmount > 0.5f)
                {
                    //move at run speed
                    player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
                }
                else if (PlayerInputManager.instance.moveAmount <= 0.5f)
                {
                    //move at walk speed
                    player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
                }
            }
        }

        private void HandleJumpingMovement()
        {
            if (player.isJumping)
            {
                player.characterController.Move(jumpDirection * runningSpeed * Time.deltaTime);
            }
        }
        private void HandleRotation()
        {
            if (!player.canRotate) return; 
            targetRotationDirection = Vector3.zero;
            targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
            targetRotationDirection = targetRotationDirection + PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
            targetRotationDirection.Normalize();
            targetRotationDirection.y = 0;

            if (targetRotationDirection == Vector3.zero)
            {
                targetRotationDirection = transform.forward;
            }
            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation,rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }

        public void HandleSprinting()
        {
            if (player.isPerformingAction)
            {
                player.playerNetworkManager.isSprinting.Value = false;
            }

            if (player.playerNetworkManager.currentStamina.Value <= 0)
            {
                player.playerNetworkManager.isSprinting.Value = false;
                return;
            }
            // no stamina => sprinting = false
            // moving => sprinting = true
            if (moveAmount >= 0.5)
            {
                player.playerNetworkManager.isSprinting.Value = true;
            }
            // not moving => sprinting = true
            else
            {
                player.playerNetworkManager.isSprinting.Value = false;
            }
            // stationary => sprinting = false
            if (player.playerNetworkManager.isSprinting.Value)
            {
                player.playerNetworkManager.currentStamina.Value -= sprintingStaminaCost * Time.deltaTime;
            }
        }

        public void AttemptToPerformDodge()
        {
            //moving when dodge = roll, staying still when dodge = backstep
            if (player.isPerformingAction)
            {
               return;
            }
            if (player.playerNetworkManager.currentStamina.Value <= 0)
            {
                return;
            }

            if (moveAmount > 0)
            {
                Vector3 inputDirection = PlayerCamera.instance.cameraObject.transform.forward * PlayerInputManager.instance.verticalInput
                                       + PlayerCamera.instance.cameraObject.transform.right * PlayerInputManager.instance.horizontalInput;

                inputDirection.y = 0;
                rollDirection = inputDirection.normalized;
                Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
                player.transform.rotation = playerRotation;

                player.playerAnimatorManager.PlayTargetActionAnimation("Roll_Forward_01", true);
            } else
            {
                //backstep
                //Not implemented, missing animation
            }
            player.playerNetworkManager.currentStamina.Value -= rollStaminaCost;
        }

        public void AttemptToPerformJump()
        {
            if (player.isPerformingAction)
                return;
            //no stamina = no jump
            if (player.playerNetworkManager.currentStamina.Value <= 0)
                return;
            //is jumping = no jump
            if (player.isJumping)
                return;   
            //mid air = no jump
            if (!player.isGrounded)
                return;
            player.playerAnimatorManager.PlayTargetActionAnimation("Main_Jump_01", false);
            player.isJumping = true;

            //to handing
            player.playerNetworkManager.currentStamina.Value -= jumpStaminaCost;

            jumpDirection = PlayerCamera.instance.cameraObject.transform.forward * PlayerInputManager.instance.verticalInput;
            jumpDirection += PlayerCamera.instance.cameraObject.transform.right * PlayerInputManager.instance.horizontalInput;
            jumpDirection.y = 0;

            if (jumpDirection != Vector3.zero)
            {
                //calc jump length base on movement speed
                if (player.playerNetworkManager.isSprinting.Value)
                {
                    jumpDirection *= 2;
                }
                else if (PlayerInputManager.instance.moveAmount > 0.5f)
                {
                    jumpDirection *= 1;
                }
                else if (PlayerInputManager.instance.moveAmount < 0.5f)
                {
                    jumpDirection *= 0.5f;
                }
            }
        }

        public void ApplyJumpingVelocity()
        {
            //upward velocity depend on force
            yVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityForce);

        }
    }
}
