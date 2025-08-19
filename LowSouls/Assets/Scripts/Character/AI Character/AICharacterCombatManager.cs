using UnityEngine;

namespace LS
{
    public class AICharacterCombatManager : CharacterCombatManager
    {
        [Header("Action Recovery")]
        public float actionRecoveryTimer = 0;

        [Header("Detection")]
        [SerializeField] float detectionRadius = 15;
        public float minimumFOV = -35;
        public float maximumFOV = 35;

        [Header("Target Info")]
        public float distanceFromTarget;
        public float viewableAngle;
        public Vector3 targetDirection;

        [Header("Attack Rotation Speed")]
        public float attackRotationSpeed = 25;

        protected override void Awake()
        {
            base.Awake();

            lockOnTransform = GetComponentInChildren<LockOnTransform>().transform;
        }

        public void FindATargetViaLineOfSight(AICharacterManager aiCharacter)
        {
            if (currentTarget != null) return;

            Collider[] colliders = Physics.OverlapSphere(aiCharacter.transform.position, detectionRadius, WorldUtilityManager.instance.GetCharacterLayers());

            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterManager targetCharacter = colliders[i].transform.GetComponent<CharacterManager>();

                if (targetCharacter == null)
                    continue;

                if (targetCharacter == aiCharacter)
                    continue;

                if (targetCharacter.isDead.Value)
                    continue;

                if (WorldUtilityManager.instance.CanIDamageThisTarget(aiCharacter.characterGroup, targetCharacter.characterGroup))
                {
                    Vector3 targetDirection = targetCharacter.transform.position - aiCharacter.transform.position;
                    float angleOfPotentialTarget = Vector3.Angle(targetDirection, aiCharacter.transform.forward);

                    if (angleOfPotentialTarget > minimumFOV && angleOfPotentialTarget < maximumFOV)
                    {
                        //check environment block
                        if (Physics.Linecast(aiCharacter.characterCombatManager.lockOnTransform.position,
                            targetCharacter.characterCombatManager.lockOnTransform.position, 
                            WorldUtilityManager.instance.GetEnvironmentLayers()))
                        {
                            Debug.DrawLine(aiCharacter.characterCombatManager.lockOnTransform.position,
                                targetCharacter.characterCombatManager.lockOnTransform.position);
                        }
                        else
                        {
                            targetDirection = targetCharacter.transform.position - transform.position;
                            viewableAngle = WorldUtilityManager.instance.GetAngleOfTarget(transform, targetDirection);
                            aiCharacter.characterCombatManager.SetTarget(targetCharacter);
                            PivotTowardsTarget(aiCharacter);
                        }
                    }
                }

            }
        }

        public void PivotTowardsTarget(AICharacterManager aiCharacter)
        {
            if (aiCharacter.isPerformingAction) return;

            if (viewableAngle >= 61 && viewableAngle <= 110)
                aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Right_90", true);

            if (viewableAngle <= -61 && viewableAngle >= -110)
                aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Left_90", true);

            if (viewableAngle >= 146 && viewableAngle <= 180)
                aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Right_180", true);

            if (viewableAngle <= -146 && viewableAngle >= -180)
                aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Turn_Left_180", true);
        }

        public void RotateTowardsAgent(AICharacterManager aiCharacter)
        {
            if (aiCharacter.aiCharacterNetworkManager.isMoving.Value)
            {
                aiCharacter.transform.rotation = aiCharacter.navMeshAgent.transform.rotation;
            }
        }
        public void RotateTowardsTargetWhilstAttacking(AICharacterManager aiCharacter)
        {
            if (currentTarget == null)
                return;

            if (!aiCharacter.aICharacterLocomotionManager.canRotate)
                return;

            if (!aiCharacter.isPerformingAction)
                return;

            Vector3 targetDirection = currentTarget.transform.position - aiCharacter.transform.position;
            targetDirection.y = 0;
            targetDirection.Normalize();

            if (targetDirection == Vector3.zero)
                targetDirection = aiCharacter.transform.forward;

            Quaternion targetRotation =  Quaternion.LookRotation(targetDirection);

            aiCharacter.transform.rotation = Quaternion.Slerp(aiCharacter.transform.rotation, targetRotation, attackRotationSpeed * Time.deltaTime);
        }

        public void HandleActionRecovery(AICharacterManager aiCharacter)
        {
            if (actionRecoveryTimer > 0)
            {
                if (!aiCharacter.isPerformingAction)
                {
                    actionRecoveryTimer -= Time.deltaTime;
                }
            }
        }
    }
}
