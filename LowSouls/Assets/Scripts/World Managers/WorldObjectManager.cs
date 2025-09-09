using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace LS {
    public class WorldObjectManager : MonoBehaviour
    {
        public static WorldObjectManager instance;

        [Header("Network Objects")]
        [SerializeField] List<NetworkObjectSpawner> networkObjectSpawners;
        [SerializeField] List<GameObject> spawnedInObjects;

        [Header("Fog Walls")]
        public List<FogWallInteractable> fogWalls;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SpawnNetworkObject(NetworkObjectSpawner networkObjectSpawner)
        {
            if (NetworkManager.Singleton.IsServer)
            {
                networkObjectSpawners.Add(networkObjectSpawner);
                networkObjectSpawner.AttemptToSpawnCharacter();
            }
        }
        public void AddFogWallToList(FogWallInteractable fogWall)
        {
            if (!fogWalls.Contains(fogWall))
            {
                fogWalls.Add(fogWall);
            }
        }

        public void RemoveFogWallFromList(FogWallInteractable fogWall)
        {
            if (fogWalls.Contains(fogWall))
            {
                fogWalls.Remove(fogWall);
            }
        }
    }
}
