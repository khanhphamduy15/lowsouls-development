using System.Collections.Generic;
using UnityEngine;

namespace LS
{
    public class WorldGameSessionManager : MonoBehaviour
    {
        public static WorldGameSessionManager instance;
        [Header("Active Players In Session")]
        public List<PlayerManager> players = new List<PlayerManager>();

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
        public void AddPlayerToActivePlayersList(PlayerManager player)
        {
            //if list does not already contains player, add them
            if (!players.Contains(player))
            {
                players.Add(player);
            }
            //null check & remove null slots
            for (int i = players.Count - 1; i > -1; i--)
            {
                if (players[i] == null)
                {
                    players.RemoveAt(i);
                }
            }
        }

        public void RemovePlayerToActivePlayersList(PlayerManager player)
        {
            //if list already contains player, remove them
            if (players.Contains(player))
            {
                players.Remove(player);
            }
            //null check & remove null slots
            for (int i = players.Count - 1; i > -1; i--)
            {
                if (players[i] == null)
                {
                    players.RemoveAt(i);
                }
            }
        }
    }
}
