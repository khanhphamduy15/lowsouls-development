using UnityEngine;
using Unity.Netcode;
using System.Collections;

namespace LS
{
    public class PickUpItemInteractable : Interactable
    {
        public ItemPickUpType pickUpType;
        [Header("Item")]
        [SerializeField] Item item;

        [Header("Enemy Loot Pick Up")]
        public NetworkVariable<int> itemID = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<ulong> enemyDropID = new NetworkVariable<ulong>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        public bool trackEnemyDropPos = true;

        [Header("World Spawn Pick Up")]
        [SerializeField] int worldSpawnInteractableID;
        [SerializeField] bool hasBeenLooted = false;

        protected override void Start()
        {
            base.Start();

            if (pickUpType == ItemPickUpType.WorldSpawn)
                CheckIfWorldItemHasBeenLooted();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            itemID.OnValueChanged += OnItemIDChanged;
            networkPosition.OnValueChanged += OnNetworkPositionChanged;
            enemyDropID.OnValueChanged += OnEnemyDropIDChanged;

            if (!IsOwner)
            {
                OnItemIDChanged(0, itemID.Value);
                OnNetworkPositionChanged(Vector3.zero, networkPosition.Value);
                OnEnemyDropIDChanged(0, enemyDropID.Value);
            }
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            itemID.OnValueChanged -= OnItemIDChanged;
            networkPosition.OnValueChanged -= OnNetworkPositionChanged;
            enemyDropID.OnValueChanged -= OnEnemyDropIDChanged;
        }

        private void CheckIfWorldItemHasBeenLooted()
        {
            if (!NetworkManager.Singleton.IsHost)
            {
                gameObject.SetActive(false);
                return;
            }

            if (!WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.ContainsKey(worldSpawnInteractableID))
            {
                WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.Add(worldSpawnInteractableID, false);
            }
            hasBeenLooted = WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted[worldSpawnInteractableID];

            if (hasBeenLooted)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }

        public override void Interact(PlayerManager player)
        {
            if (player.isPerformingAction)
                return;

            base.Interact(player);

            //sfx play
            player.characterSoundFXManager.PlaySoundFX(WorldSoundFXManager.instance.pickUpItemSFX);

            //add to inventory
            player.playerInventoryManager.AddItemToInventory(item);

            //display item info ui
            PlayerUIManager.instance.playerUIPopUpManager.SendItemPopUp(item, 1);

            if (pickUpType == ItemPickUpType.WorldSpawn)
            {
                if (WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.ContainsKey((int)worldSpawnInteractableID))
                {
                    WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.Remove(worldSpawnInteractableID);
                }
                WorldSaveGameManager.instance.currentCharacterData.worldItemsLooted.Add(worldSpawnInteractableID, true);
            }

            DestroyThisNetworkObjectServerRpc();
        }

        protected void OnItemIDChanged(int oldValue, int newValue)
        {
            if (pickUpType != ItemPickUpType.CharacterDrop)
                return;

            item = WorldItemDatabase.instance.GetItemByID(itemID.Value);
        }

        protected void OnNetworkPositionChanged(Vector3 oldPosition, Vector3 newPosition)
        {
            if (pickUpType != ItemPickUpType.CharacterDrop)
                return;

            transform.position = networkPosition.Value;
        }

        protected void OnEnemyDropIDChanged(ulong oldValue, ulong newValue)
        {
            if (pickUpType != ItemPickUpType.CharacterDrop)
                return;

            if (trackEnemyDropPos)
                StartCoroutine(EnemyDropPosTrack());
        }

        protected IEnumerator EnemyDropPosTrack()
        {
            AICharacterManager enemyDrop = NetworkManager.Singleton.SpawnManager.SpawnedObjects[enemyDropID.Value].gameObject.GetComponent<AICharacterManager>();
            bool trackEnemy = false;

            if (enemyDrop != null)
                trackEnemy = true;
            if (trackEnemy)
            {
                while (gameObject.activeInHierarchy)
                {
                    transform.position = enemyDrop.characterCombatManager.lockOnTransform.position;
                    yield return null;
                }
            }
            yield return null;
        }

        [ServerRpc(RequireOwnership = false)]
        protected void DestroyThisNetworkObjectServerRpc()
        {
            if (IsServer)
            {
                GetComponent<NetworkObject>().Despawn();
            }
        }
    }
}
