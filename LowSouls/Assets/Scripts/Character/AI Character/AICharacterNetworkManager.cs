using System.Collections;
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
                    StartCoroutine(DestroyAfterTime(5));
                    aiCharacter.aiCharacterInventoryManager.DropItem();
                }
            }
        }
        private IEnumerator DestroyAfterTime(float delay)
        {
            yield return new WaitForSeconds(delay);

            if (IsServer && NetworkObject != null && NetworkObject.IsSpawned)
            {
                NetworkObject.Despawn();
            }
        }
    }
}
