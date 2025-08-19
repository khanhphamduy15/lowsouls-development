using UnityEngine;

namespace LS {
    [CreateAssetMenu(menuName = "A.I/State/Idle")]
    public class IdleState : AIState
    {
        public override AIState Tick(AICharacterManager aiCharacter)
        {
            if (aiCharacter.characterCombatManager.currentTarget != null)
            {
                //pursue target
                return SwitchState(aiCharacter, aiCharacter.pursueTarget);
            }
            else
            {
                aiCharacter.aiCharacterCombatManager.FindATargetViaLineOfSight(aiCharacter);
                return this;
            }
        }
    }
}
