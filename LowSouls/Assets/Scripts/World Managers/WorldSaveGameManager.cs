using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace LS
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager instance;

        public PlayerManager player;

        [Header("SAVE/LOAD")]
        [SerializeField] bool saveGame;
        [SerializeField] bool loadGame;

        [Header("World Scene Index")]
        [SerializeField] private int worldSceneIndex = 1;

        [Header("Save Data Writer")]
        private SaveFileDataWriter saveFileDataWriter;

        [Header("Current Character Data")]
        public CharacterSlot currentCharSlotBeingUsed;
        public CharacterSaveData currentCharacterData;
        private string saveFileName;

        [Header("Character Slots")]
        public CharacterSaveData characterSlot01;
        public CharacterSaveData characterSlot02;
        public CharacterSaveData characterSlot03;
        public CharacterSaveData characterSlot04;
        public CharacterSaveData characterSlot05;

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

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            LoadAllCharacterProfiles();
        }

        private void Update()
        {
            if (saveGame)
            {
                saveGame = false;
                SaveGame();
            }
            if (loadGame)
            {
                loadGame = false;
                LoadGame();
            }  
        }

        public string DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot characterSlot)
        {
            string fileName = "";
            switch (characterSlot)
            {
                case CharacterSlot.CharacterSlot_01:
                    fileName = "charSlot_01";
                    break;
                case CharacterSlot.CharacterSlot_02:
                    fileName = "charSlot_02";
                    break;
                case CharacterSlot.CharacterSlot_03:
                    fileName = "charSlot_03";
                    break;
                case CharacterSlot.CharacterSlot_04:
                    fileName = "charSlot_04";
                    break;
                case CharacterSlot.CharacterSlot_05:
                    fileName = "charSlot_05";
                    break;
                default:
                    break;  
            }
            return fileName;
        }

        public void AttemptToCreateNewGame()
        {
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            //check to see if can create a new save file
            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_01);
            //if slot is not taken
            if (!saveFileDataWriter.CheckIfFileExist())
            {
                //create new
                currentCharSlotBeingUsed = CharacterSlot.CharacterSlot_01;
                currentCharacterData = new CharacterSaveData();
                StartCoroutine(LoadWorldScene());
                return;
            }

            //Do the same for slot 2
            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_02);
            if (!saveFileDataWriter.CheckIfFileExist())
            {
                currentCharSlotBeingUsed = CharacterSlot.CharacterSlot_02;
                currentCharacterData = new CharacterSaveData();
                StartCoroutine(LoadWorldScene());
                return;
            }

            //slot 3
            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_03);
            if (!saveFileDataWriter.CheckIfFileExist())
            {
                currentCharSlotBeingUsed = CharacterSlot.CharacterSlot_03;
                currentCharacterData = new CharacterSaveData();
                StartCoroutine(LoadWorldScene());
                return;
            }

            //slot 4
            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_04);
            if (!saveFileDataWriter.CheckIfFileExist())
            {
                currentCharSlotBeingUsed = CharacterSlot.CharacterSlot_04;
                currentCharacterData = new CharacterSaveData();
                StartCoroutine(LoadWorldScene());
                return;
            }

            //slot 5
            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_05);
            if (!saveFileDataWriter.CheckIfFileExist())
            {
                currentCharSlotBeingUsed = CharacterSlot.CharacterSlot_05;
                currentCharacterData = new CharacterSaveData();
                StartCoroutine(LoadWorldScene());
                return;
            }

            //notify if no free slot
            TitleScreenManager.instance.DisplayNoFreeCharSlotsPopUp();
        }

        public void LoadGame()
        {
            //load prev file
            saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharSlotBeingUsed);

            saveFileDataWriter = new SaveFileDataWriter();
            //general path
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = saveFileName;
            currentCharacterData = saveFileDataWriter.LoadSaveFile();

            StartCoroutine(LoadWorldScene());
        }

        //preload all saves
        private void LoadAllCharacterProfiles()
        {
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_01);
            characterSlot01 = saveFileDataWriter.LoadSaveFile();

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_02);
            characterSlot02 = saveFileDataWriter.LoadSaveFile();

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_03);
            characterSlot03 = saveFileDataWriter.LoadSaveFile();

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_04);
            characterSlot04 = saveFileDataWriter.LoadSaveFile();

            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_05);
            characterSlot05 = saveFileDataWriter.LoadSaveFile();

        }
        public void SaveGame()
        {
            //save current file
            saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharSlotBeingUsed);

            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = saveFileName;
            //pass player inf from game -> save file
            player.SaveGameDataToCurrentCharData(ref currentCharacterData);
            //write inf onto json file, saved to this machine
            saveFileDataWriter.CreateNewCharSaveFile(currentCharacterData);
        }

        public void DeleteGame(CharacterSlot characterSlot)
        {
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);
            saveFileDataWriter.DeleteSaveFile();
        }
        public IEnumerator LoadWorldScene()
        {
            //use for just 1 world scene
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);

            //use for different scenes for levels
            //AsyncOperation loadOperation = SceneManager.LoadSceneAsync(currentCharacterData.sceneIndex);
            player.LoadGameDataFromCurrentCharData(ref currentCharacterData);

            yield return null;
        }

        public int GetWorldSceneIndex()
        {
            return worldSceneIndex;
        }
    }
}
