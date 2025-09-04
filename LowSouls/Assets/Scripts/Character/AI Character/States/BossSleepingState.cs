using UnityEngine;

namespace LS
{
    [CreateAssetMenu(menuName = "A.I/State/Boss Sleep")]
    public class BossSleepingState : AIState
    {
        public override AIState Tick(AICharacterManager aiCharacter)
        {
            return base.Tick(aiCharacter);
        }
    }
}