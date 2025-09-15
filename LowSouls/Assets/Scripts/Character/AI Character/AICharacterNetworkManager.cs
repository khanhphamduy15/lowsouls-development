using Unity.Netcode;
using UnityEngine;

namespace LS
{
    public class AICharacterNetworkManager : CharacterNetworkManager
    {
        AICharacterManager aiCharacter;

        protected override void Awake()
        {
            base.Awake();

            aiCharacter = GetComponent<AICharacterManager>();
        }

        public override void OnIsDeadChanged(bool oldStatus, bool newStatus)
        {
            base.OnIsDeadChanged(oldStatus, newStatus);

            if (newStatus)
            {
                if (aiCharacter != null && aiCharacter.aiCharacterInventoryManager != null)
                {
                    aiCharacter.aiCharacterInventoryManager.DropItem();
                }
            }
        }
    }
}
