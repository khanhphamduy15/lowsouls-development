using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class TitleScreenManager : MonoBehaviour
    {
        public static TitleScreenManager instance;

        [Header("Menus")]
        [SerializeField] GameObject titleScreenMainMenu;
        [SerializeField] GameObject titleScreenLoadMenu;

        [Header("Buttons")]
        [SerializeField] Button loadMenuReturnButton;
        [SerializeField] Button mainMenuNewGameButton;
        [SerializeField] Button mainMenuLoadGameButton;
        [SerializeField] Button deleteCharPopUpConfirmButton;
        [SerializeField] Button noCharSlotsOKButton;


        [Header("Pop Ups")]
        [SerializeField] GameObject noCharSlotsPopUp;
        [SerializeField] GameObject deleteCharSlotPopUp;

        [Header("Character Slots")]
        public CharacterSlot currentSelectedSlot = CharacterSlot.NO_SLOT;

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
        public void StartNetworkAsHost()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void StartNewGame()
        {
            WorldSaveGameManager.instance.AttemptToCreateNewGame();
        }

        public void OpenLoadGameMenu()
        {
            titleScreenMainMenu.SetActive(false);
            titleScreenLoadMenu.SetActive(true);

            //select return button 
            loadMenuReturnButton.Select();
        }

        public void CloseLoadGameMenu()
        {
            titleScreenMainMenu.SetActive(true);
            titleScreenLoadMenu.SetActive(false);

            //select load button 
            mainMenuLoadGameButton.Select();
        }

        public void DisplayNoFreeCharSlotsPopUp()
        {
            noCharSlotsPopUp.SetActive(true);
            noCharSlotsOKButton.Select();
        }

        public void CloseNoFreeCharSlotsPopUp()
        {
            noCharSlotsPopUp.SetActive(false);
            mainMenuNewGameButton.Select();
        }

        //Char slot func    
        public void SelectCharacterSlot(CharacterSlot charSlot)
        {
            currentSelectedSlot = charSlot;
        }

        public void SelectNoSlot()
        {
            currentSelectedSlot = CharacterSlot.NO_SLOT;
        }

        public void AttemptToDeleteCharSlot()
        {
            if (currentSelectedSlot != CharacterSlot.NO_SLOT)
            {
                deleteCharSlotPopUp.SetActive(true);
                deleteCharPopUpConfirmButton.Select();
            }
        }

        public void DeleteCharSlot()
        {
            deleteCharSlotPopUp.SetActive(false);
            WorldSaveGameManager.instance.DeleteGame(currentSelectedSlot);

            //manual refresh by disable/enable
            titleScreenLoadMenu.SetActive(false);
            titleScreenLoadMenu.SetActive(true);
            loadMenuReturnButton.Select();
        }

        public void CloseDeleteCharSlotPopUp()
        {
            deleteCharSlotPopUp.SetActive(false);
            loadMenuReturnButton.Select();
        }
    }
}