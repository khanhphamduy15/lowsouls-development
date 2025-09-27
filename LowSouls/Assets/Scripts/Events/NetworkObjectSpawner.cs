using Unity.Netcode;
using UnityEngine;

namespace LS {
    public class NetworkObjectSpawner : MonoBehaviour
    {
        [Header("Object")]
        [SerializeField] GameObject networkGameObject;
        [SerializeField] GameObject instantiatedGameObject;

        private void Awake()
        {
        }

        private void Start()
        {
            WorldObjectManager.instance.SpawnNetworkObject(this);
            gameObject.SetActive(false);
        }

        public void AttemptToSpawnCharacter()
        {
            if (networkGameObject != null)
            {
                instantiatedGameObject = Instantiate(networkGameObject);
                instantiatedGameObject.transform.position = transform.position;
                instantiatedGameObject.transform.rotation = transform.rotation;
                instantiatedGameObject.GetComponent<NetworkObject>().Spawn();
            }
        }
    }
}
