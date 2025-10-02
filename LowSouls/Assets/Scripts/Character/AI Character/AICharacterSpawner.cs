using Unity.Netcode;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace LS
{
    public class AICharacterSpawner : MonoBehaviour
    {
        [Header("Character")]
        [SerializeField] GameObject characterGameObject;
        [SerializeField] GameObject instantiatedGameObject;
        private AICharacterManager aiCharacter;

        private void Awake()
        {
        }

        private void Start()
        {
            WorldAIManager.instance.SpawnCharacter(this);
            gameObject.SetActive(false);
        }

        public void AttemptToSpawnCharacter()
        {
            if (characterGameObject != null)
            {
                instantiatedGameObject = Instantiate(characterGameObject);
                instantiatedGameObject.transform.position = transform.position;
                instantiatedGameObject.transform.rotation = transform.rotation;
                instantiatedGameObject.GetComponent<NetworkObject>().Spawn();
                aiCharacter =  instantiatedGameObject.GetComponent<AICharacterManager>();
                if (aiCharacter != null)
                    WorldAIManager.instance.AddCharacterToSpawnedCharactersList(aiCharacter);
            }
        }

        public void ResetCharacter()
        {
            if (instantiatedGameObject == null)
                return;

            if (aiCharacter == null)
                return;
          
            instantiatedGameObject.transform.position = transform.position;
            instantiatedGameObject.transform.rotation = transform.rotation;
            aiCharacter.aiCharacterNetworkManager.currentHealth.Value = aiCharacter.aiCharacterNetworkManager.maxHealth.Value;

            if (aiCharacter.isDead.Value)
            {
                aiCharacter.isDead.Value = false;
                aiCharacter.characterAnimatorManager.PlayTargetActionAnimation("Empty", false, false, true, true, true);
                aiCharacter.currentState.SwitchState(aiCharacter, aiCharacter.idle);
            }

            aiCharacter.characterUIManager.ResetCharacterHPBar();

            if (aiCharacter is AIBossCharacterManager)
            {
                AIBossCharacterManager boss = aiCharacter as AIBossCharacterManager;
                boss.aiCharacterNetworkManager.isAwake.Value = false;
                boss.sleepState.hasBeenAwakened = boss.hasBeenAwakened.Value;
                boss.currentState = boss.currentState.SwitchState(boss, boss.sleepState);
            }
        }
    }
}
