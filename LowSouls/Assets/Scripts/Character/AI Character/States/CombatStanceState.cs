using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LS
{
    [CreateAssetMenu(menuName = "A.I/State/Combat Stance State")]
    public class CombatStanceState : AIState
    {
        [Header("Attacks")]
        public List<AICharacterAttackAction> aiCharacterAttacks;            //List of attacks
        protected List<AICharacterAttackAction> potentialAttacks;             //List of possible attacks in this state (based on angle,distance,...)
        private AICharacterAttackAction chosenAttack;
        private AICharacterAttackAction previousAttack;
        protected bool hasAttack = false;


        [Header("Combo")]
        [SerializeField] protected bool canPerformCombo = false;
        [SerializeField] protected int chanceToPerformCombo = 25;
        protected bool hasRolledForComboChance = false;

        [Header("Engagement Distance")]
        [SerializeField] public float maximumEngagementDistance = 5;

        public override AIState Tick(AICharacterManager aiCharacter)
        {
            if (aiCharacter.isPerformingAction)
                return this;

            if (!aiCharacter.navMeshAgent.enabled)
                aiCharacter.navMeshAgent.enabled = true;

            if (!aiCharacter.aiCharacterNetworkManager.isMoving.Value)
            {
                if (aiCharacter.aiCharacterCombatManager.viewableAngle < -30 || aiCharacter.aiCharacterCombatManager.viewableAngle > 30)
                    aiCharacter.aiCharacterCombatManager.PivotTowardsTarget(aiCharacter);
            }

            aiCharacter.aiCharacterCombatManager.RotateTowardsAgent(aiCharacter);

            //character not present -> idle
            if (aiCharacter.aiCharacterCombatManager.currentTarget == null)
                return SwitchState(aiCharacter, aiCharacter.idle);

            if (!hasAttack)
            {
                GetNewAttack(aiCharacter);
            }
            else
            {
                //check recovery timer, pass attack to attack state, roll for combo chance, switch state
                aiCharacter.attack.currentAttack = chosenAttack;
                return SwitchState(aiCharacter, aiCharacter.attack);
            }

            //outside engagement distance  -> pursue state  
            if (aiCharacter.aiCharacterCombatManager.distanceFromTarget > maximumEngagementDistance)
                return SwitchState(aiCharacter, aiCharacter.pursueTarget);

            NavMeshPath path = new NavMeshPath();
            aiCharacter.navMeshAgent.CalculatePath(aiCharacter.aiCharacterCombatManager.currentTarget.transform.position, path);
            aiCharacter.navMeshAgent.SetPath(path);

            return this;
        }
        protected virtual void GetNewAttack(AICharacterManager aiCharacter)
        {
            //Sort and remove attacks that cant be performed (based on angle,distance,...)
            potentialAttacks = new List<AICharacterAttackAction>();
            foreach (var potentialAttack in aiCharacterAttacks)
            {
                float distance = aiCharacter.aiCharacterCombatManager.distanceFromTarget;
                float angle = aiCharacter.aiCharacterCombatManager.viewableAngle;

                if (potentialAttack.minimumAttackDistance > distance)
                {
                    continue;
                }

                if (potentialAttack.maximumAttackDistance < distance)
                {
                    continue;
                }

                if (potentialAttack.minimumAttackAngle > angle)
                {
                    continue;
                }

                if (potentialAttack.maximumAttackAngle < angle)
                {
                    continue;
                }

                potentialAttacks.Add(potentialAttack);
            }

            if (potentialAttacks.Count <= 0) return;

            var totalWeight = 0;
            foreach (var attack in potentialAttacks)
            {
                totalWeight += attack.attackWeight;
            }

            var randomWeightValue = Random.Range(1, totalWeight + 1);
            var processedWeight = 0;
            foreach (var attack in potentialAttacks)
            {
                processedWeight += attack.attackWeight;

                if (randomWeightValue <= processedWeight)
                {
                    chosenAttack = attack;
                    previousAttack = chosenAttack;
                    hasAttack = true;
                }
            }
        }

        protected virtual bool RollForOutcomeChance(int outcomeChance)
        {
            bool outcomeWillBePerform = false;
            int randomPercentage = Random.Range(0, 100);

            if (randomPercentage < outcomeChance)
                outcomeWillBePerform = true;

            return outcomeWillBePerform;
        }
        protected override void ResetStateFlags(AICharacterManager aICharacter)
        {
            base.ResetStateFlags(aICharacter);

            hasRolledForComboChance = false;
            hasAttack = false;
        }
    }
}
