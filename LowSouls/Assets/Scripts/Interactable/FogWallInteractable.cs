using UnityEngine;
using Unity.Netcode;
using System.Collections;

namespace LS
{
    public class FogWallInteractable : Interactable
    {
        [Header("Fog")]
        [SerializeField] GameObject[] fogWallObjects;

        [Header("Collision")]
        [SerializeField] Collider fogWallCollider;

        [Header("ID")]
        public int fogWallID;

        [Header("Active")]
        public NetworkVariable<bool> isActive = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        public override void Interact(PlayerManager player)
        {
            base.Interact(player);
            Vector3 directionToFog = (transform.position - player.transform.position).normalized;
            directionToFog.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(directionToFog);
            player.transform.rotation = targetRotation;

            AllowPlayerThroughFogWallCollidersServerRpc(player.NetworkObjectId);

            player.playerAnimatorManager.PlayTargetActionAnimation("Pass_Through_Fog_01", true);
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            OnIsActiveChanged(false, isActive.Value);
            isActive.OnValueChanged += OnIsActiveChanged;
            WorldObjectManager.instance.AddFogWallToList(this);

        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            isActive.OnValueChanged -= OnIsActiveChanged;
            WorldObjectManager.instance.RemoveFogWallFromList(this);
        }

        private void OnIsActiveChanged(bool oldStatus, bool newStatus)
        {
            if (isActive.Value)
            {
                foreach (var fogObj in fogWallObjects)
                {
                    fogObj.SetActive(true);
                }
            }

            else
            {
                foreach (var fogObj in fogWallObjects)
                {
                    fogObj.SetActive(false);
                }
            }
        }

        [ServerRpc(RequireOwnership = false)]
        private void AllowPlayerThroughFogWallCollidersServerRpc(ulong playerObjectID)
        {
            if (IsServer)
            {
                AllowPlayerThroughFogWallCollidersClientRpc(playerObjectID);
            }
        }

        [ClientRpc]
        private void AllowPlayerThroughFogWallCollidersClientRpc(ulong playerObjectID)
        {
            PlayerManager player = NetworkManager.Singleton.SpawnManager.SpawnedObjects[playerObjectID].GetComponent<PlayerManager>();
            if (player != null)
                StartCoroutine(DisableCollisionForTime(player));
        }

        private IEnumerator DisableCollisionForTime(PlayerManager player)
        {
            Physics.IgnoreCollision(player.characterController, fogWallCollider, true);
            yield return new WaitForSeconds(3);
            Physics.IgnoreCollision(player.characterController, fogWallCollider, false);
        }
    }
}
