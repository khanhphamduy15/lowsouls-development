using UnityEngine;

namespace LS
{
    public class CharacterLocomotionManager : MonoBehaviour
    {
        CharacterManager character;

        [Header("Ground Check Jumping")]
        [SerializeField] protected float gravityForce = -5.55f;
        [SerializeField] float groundCheckSphereRadius = 0.3f;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] protected Vector3 yVelocity; //pull up/down force
        [SerializeField] protected float groundedVelocity = -20; //sticking ground when grounded
        [SerializeField] protected float fallStartYVelocity = -5; //start force, apply more force over time
        protected bool fallingVelocityHasBeenSet = false;
        protected float inAirTimer = 0;

        [Header("Flags")]
        public bool isRolling = false;
        public bool canRotate = true;
        public bool canMove = true;
        public bool isGrounded = true;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Update()
        {
            HandleGroundCheck();
            if (character.characterLocomotionManager.isGrounded)
            {
                if (yVelocity.y < 0)
                {
                    inAirTimer = 0;
                    fallingVelocityHasBeenSet = false;
                    yVelocity.y = groundedVelocity;
                }
            }
            else
            {
                if (!character.characterNetworkManager.isJumping.Value && !fallingVelocityHasBeenSet)
                {
                    fallingVelocityHasBeenSet = true;
                    yVelocity.y = fallStartYVelocity;
                }

                inAirTimer += Time.deltaTime;
                character.animator.SetFloat("inAirTimer", inAirTimer);
                yVelocity.y += gravityForce * Time.deltaTime;
            }
            character.characterController.Move(yVelocity * Time.deltaTime);
        }

        protected void HandleGroundCheck()
        {
            character.characterLocomotionManager.isGrounded = Physics.CheckSphere(character.transform.position, groundCheckSphereRadius, groundLayer);
        }

        //protected void OnDrawGizmosSelected()
        //{
        //    Gizmos.DrawSphere(character.transform.position, groundCheckSphereRadius);
        //}

        public void EnableCanRotate()
        {
            canRotate = true;
        }
        public void DisableCanRotate()
        {
            canRotate = false;
        }
    }
}
