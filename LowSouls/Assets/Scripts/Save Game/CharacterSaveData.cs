using UnityEngine;

namespace LS
{
    [System.Serializable]
    //reference of data, not monobehaviour
    public class CharacterSaveData
    {
        [Header("Scene Index")]
        public int sceneIndex = 1;

        [Header("Character Name")]
        public string characterName = "Character";

        [Header("Time Played")]
        public float secondsPlayed;

        [Header("World Coordinates")]
        public float xPos;
        public float yPos;
        public float zPos;

        [Header("Stats")]
        public int vitality;
        public int endurance;

        [Header("Resources")]
        public int currentHealth;
        public float currentStamina;

    }
}
