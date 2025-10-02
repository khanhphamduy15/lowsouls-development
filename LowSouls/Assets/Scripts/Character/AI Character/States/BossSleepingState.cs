using UnityEngine;
using UnityEngine.TextCore.Text;

namespace LS
{
    [CreateAssetMenu(menuName = "A.I/State/Boss Sleep")]
    public class BossSleepingState : AIState
    {
        [SerializeField] string sleepAnimation = "Sleep_01";
        [SerializeField] string wakeAnimation = "Awaken_01";
        private bool sleepAnimationSet = false;

        public bool hasBeenAwakened = false;

        public override AIState Tick(AICharacterManager aiCharacter)
        {
            aiCharacter.navMeshAgent.enabled = false;
            if (!hasBeenAwakened)
            {
                return HasNotBeenAwakened(aiCharacter);    
            }
            else
            {
                return HasBeenAwakened(aiCharacter);
            }
        }

        private AIState HasBeenAwakened(AICharacterManager aiCharacter)
        {
            if (aiCharacter.characterCombatManager.currentTarget != null && !aiCharacter.aiCharacterNetworkManager.isAwake.Value)
            {
                aiCharacter.aiCharacterNetworkManager.isAwake.Value = true;
                return SwitchState(aiCharacter, aiCharacter.pursueTarget);
            }

            return this;
        }

        private AIState HasNotBeenAwakened(AICharacterManager aiCharacter)
        {
            aiCharacter.navMeshAgent.enabled = false;

            if (!sleepAnimationSet && !aiCharacter.aiCharacterNetworkManager.isAwake.Value)
            {
                sleepAnimationSet = true;
                aiCharacter.aiCharacterNetworkManager.sleepAnimation.Value = sleepAnimation;
                aiCharacter.aiCharacterNetworkManager.wakeAnimation.Value = wakeAnimation;
                aiCharacter.characterAnimatorManager.PlayTargetActionAnimation(aiCharacter.aiCharacterNetworkManager.sleepAnimation.Value.ToString(), true);
            }

            if (aiCharacter.characterCombatManager.currentTarget != null && !aiCharacter.aiCharacterNetworkManager.isAwake.Value)
            {
                aiCharacter.aiCharacterNetworkManager.isAwake.Value = true;

                if (!aiCharacter.isPerformingAction && !aiCharacter.isDead.Value)
                    aiCharacter.characterAnimatorManager.PlayTargetActionAnimation(aiCharacter.aiCharacterNetworkManager.wakeAnimation.Value.ToString(), true);

                return SwitchState(aiCharacter, aiCharacter.pursueTarget);
            }
            return this;
        }

    }
}