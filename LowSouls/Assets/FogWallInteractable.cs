using UnityEngine;
using Unity.Netcode;

namespace LS
{
    public class FogWallInteractable : NetworkBehaviour
    {
        [Header("Fog")]
        [SerializeField] GameObject[] fogWallObjects;

        [Header("ID")]
        public int fogWallID;

        [Header("Active")]
        public NetworkVariable<bool> isActive = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

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
            if (newStatus)
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
    }
}
