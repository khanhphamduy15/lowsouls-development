using UnityEngine;
using Unity.Netcode;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace LS
{
    public class AIBossCharacterManager : AICharacterManager
    {
        public int bossID = 0;
        [SerializeField] bool hasBeenDefeated = false;
        [SerializeField] bool hasBeenAwakened = false;

        [SerializeField] List<FogWallInteractable> fogWalls;

        [Header("DEBUG")]
        [SerializeField] bool wakeBossUp = false;

        protected override void Update()
        {
            base.Update();

            if (wakeBossUp)
            {
                wakeBossUp = false;
                WakeBoss();
            }
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsServer)
            {
                if (!WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.ContainsKey(bossID))
                {
                    WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Add(bossID, false);
                    WorldSaveGameManager.instance.currentCharacterData.bossesDefeated.Add(bossID, false);

                }
                else
                {
                    hasBeenDefeated = WorldSaveGameManager.instance.currentCharacterData.bossesDefeated[bossID];
                    hasBeenAwakened = WorldSaveGameManager.instance.currentCharacterData.bossesAwakened[bossID];
                }

                StartCoroutine(GetFogWallsFromWorldObjectManager());

                // boss awakened -> enable fog wall
                if (hasBeenAwakened)
                {
                    for (int i = 0; i < fogWalls.Count; i++)
                    {
                        fogWalls[i].isActive.Value = true;
                    }
                }

                // boss defeated -> disable fog wall
                if (hasBeenDefeated)
                {
                    for (int i = 0; i < fogWalls.Count; i++)
                    {
                        fogWalls[i].isActive.Value = false;
                    }
                    aiCharacterNetworkManager.isActive.Value = false;
                }
            }
        }

        public override IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
        {
            if (IsOwner)
            {
                characterNetworkManager.currentHealth.Value = 0;
                isDead.Value = true;

                //not grounded = aerial death animation

                if (!manuallySelectDeathAnimation)
                {
                    characterAnimatorManager.PlayTargetActionAnimation("Dead_01", true);
                }

                hasBeenDefeated = true;

                if (!WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.ContainsKey(bossID))
                {
                    WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Add(bossID, true);
                    WorldSaveGameManager.instance.currentCharacterData.bossesDefeated.Add(bossID, true);

                }
                else
                {
                    WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Remove(bossID);
                    WorldSaveGameManager.instance.currentCharacterData.bossesDefeated.Remove(bossID);
                    WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Add(bossID, true);
                    WorldSaveGameManager.instance.currentCharacterData.bossesDefeated.Add(bossID, true);
                }

                WorldSaveGameManager.instance.SaveGame();
            }

            yield return new WaitForSeconds(5);

            //Disable character 
        }

        private IEnumerator GetFogWallsFromWorldObjectManager()
        {
            while (WorldObjectManager.instance.fogWalls.Count == 0)
                yield return new WaitForEndOfFrame();

            fogWalls = new List<FogWallInteractable>();

            foreach (var fogWall in WorldObjectManager.instance.fogWalls)
            {
                if (fogWall.fogWallID == bossID)
                    fogWalls.Add(fogWall);
            }
        }

        public void WakeBoss()
        {
            hasBeenAwakened = true;
            if (!WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.ContainsKey(bossID))
            {
                WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Add(bossID, true);

            }
            else
            {
                WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Remove(bossID);
                WorldSaveGameManager.instance.currentCharacterData.bossesAwakened.Add(bossID, true);
            }

            for (int i = 0; i < fogWalls.Count; i++)
            {
                fogWalls[i].isActive.Value = true;
            }   
        }
    }
}
