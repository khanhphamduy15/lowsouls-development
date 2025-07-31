using UnityEngine;

namespace LS
{
    [System.Serializable]
    //reference of data, not monobehaviour
    public class CharacterSaveData
    {
        [Header("Scene Index")]
        public int sceneIndex;

        [Header("Character Name")]
        public string characterName = "Character";

        [Header("Time Played")]
        public float secondsPlayed;

        [Header("World Coordinates")]
        public float xPos;
        public float yPos;
        public float zPos;

    }
}
