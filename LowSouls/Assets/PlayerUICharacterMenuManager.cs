using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LS
{
    public class PlayerUICharacterMenuManager : MonoBehaviour
    {
        [Header("Menu")]
        [SerializeField] GameObject menu;

        public void OpenCharacterMenu()
        {
            PlayerUIManager.instance.menuWindowIsOpen = true;
            menu.SetActive(true);
        }

        public void CloseCharacterMenu()
        {
            PlayerUIManager.instance.menuWindowIsOpen = false;
            menu.SetActive(false);
        }

        public void CloseCharacterMenuAfterTime()
        {
            StartCoroutine(WaitThenCloseMenu());
        }

        private IEnumerator WaitThenCloseMenu()
        {
            yield return new WaitForFixedUpdate();
            PlayerUIManager.instance.menuWindowIsOpen = false;
            menu.SetActive(false);
        }

        public void ReturnToTitleMenu()
        {
            if (PlayerUIManager.instance != null)
            {
                CloseCharacterMenuAfterTime();
            }

            PlayerUIManager.instance.playerUILoadingScreenManager.ActivateLoadingScreen();

            string menuScene = SceneUtility.GetScenePathByBuildIndex(0);
            NetworkManager.Singleton.SceneManager.LoadScene(menuScene, LoadSceneMode.Single);
        }
    }
}
