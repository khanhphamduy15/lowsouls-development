using TMPro;
using UnityEngine;

namespace LS
{
    public class UI_Character_Save_Slot : MonoBehaviour
    {
        SaveFileDataWriter saveFileWriter;

        [Header("Game Slot")]
        public CharacterSlot characterSlot;

        [Header("Character Info")]
        public TextMeshProUGUI characterName;
        public TextMeshProUGUI timePlayed;

        private void OnEnable()
        {
            LoadSaveSlots();
        }

        private void LoadSaveSlots()
        {
            saveFileWriter = new SaveFileDataWriter();
            saveFileWriter.saveDataDirectoryPath = Application.persistentDataPath;

            //Save slot 01
            if (characterSlot == CharacterSlot.CharacterSlot_01)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);
                //if file exists, get inf
                if (saveFileWriter.CheckIfFileExist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot01.characterName;
                }
                //if not, disable this gameObject
                else
                {
                    gameObject.SetActive(false);
                }
            }
            //Save slot 02
            else if (characterSlot == CharacterSlot.CharacterSlot_02)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);
                //if file exists, get inf
                if (saveFileWriter.CheckIfFileExist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot02.characterName;
                }
                //if not, disable this gameObject
                else
                {
                    gameObject.SetActive(false);
                }
            }
            //Save slot 03
            else if (characterSlot == CharacterSlot.CharacterSlot_03)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);
                //if file exists, get inf
                if (saveFileWriter.CheckIfFileExist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot03.characterName;
                }
                //if not, disable this gameObject
                else
                {
                    gameObject.SetActive(false);
                }
            }
            //Save slot 04
            else if (characterSlot == CharacterSlot.CharacterSlot_04)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);
                //if file exists, get inf
                if (saveFileWriter.CheckIfFileExist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot04.characterName;
                }
                //if not, disable this gameObject
                else
                {
                    gameObject.SetActive(false);
                }
            }
            //Save slot 05
            else if (characterSlot == CharacterSlot.CharacterSlot_05)
            {
                saveFileWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);
                //if file exists, get inf
                if (saveFileWriter.CheckIfFileExist())
                {
                    characterName.text = WorldSaveGameManager.instance.characterSlot05.characterName;
                }
                //if not, disable this gameObject
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }

        public void LoadGameFromCharacterSaveSlot()
        {
            WorldSaveGameManager.instance.currentCharSlotBeingUsed = characterSlot;
            WorldSaveGameManager.instance.LoadGame();
        }

        public void SelectCurrentSlot()
        {
            TitleScreenManager.instance.SelectCharacterSlot(characterSlot);
        }
    }
}
