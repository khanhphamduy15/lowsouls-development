using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

namespace LS
{
    [CreateAssetMenu(menuName = "A.I/State/Pursue Target")]
    public class PursueTargetState : AIState
    {
        public override AIState Tick(AICharacterManager aiCharacter)
        {
            //check if is performing action
            if (aiCharacter.isPerformingAction) return this;

            //check if target null -> return to idle
            if (aiCharacter.aiCharacterCombatManager.currentTarget == null) return SwitchState(aiCharacter, aiCharacter.idle);

            //enable navmesh agent active
            if (!aiCharacter.navMeshAgent.enabled) aiCharacter.navMeshAgent.enabled = true;

            //if target is outside fov, pivot to face them
            if (aiCharacter.aiCharacterCombatManager.enablePivot)
            {
                if (aiCharacter.aiCharacterCombatManager.viewableAngle < aiCharacter.aiCharacterCombatManager.minimumFOV
                    || aiCharacter.aiCharacterCombatManager.viewableAngle > aiCharacter.aiCharacterCombatManager.maximumFOV)
                    aiCharacter.aiCharacterCombatManager.PivotTowardsTarget(aiCharacter);
            }

            aiCharacter.aICharacterLocomotionManager.RotateTowardsAgent(aiCharacter);

            //within combat range -> switch state to combat
                if (aiCharacter.aiCharacterCombatManager.distanceFromTarget <= aiCharacter.navMeshAgent.stoppingDistance)
                    return SwitchState(aiCharacter, aiCharacter.combatStance);

            //unreachable target -> return original place

            //pursue target 
            NavMeshPath path = new NavMeshPath();
            aiCharacter.navMeshAgent.CalculatePath(aiCharacter.aiCharacterCombatManager.currentTarget.transform.position, path);
            aiCharacter.navMeshAgent.SetPath(path);

            return this;
        }
    }
}
