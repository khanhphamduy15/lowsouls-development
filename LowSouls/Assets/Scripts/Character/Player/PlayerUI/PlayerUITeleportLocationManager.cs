using UnityEngine;
using UnityEngine.UI;

namespace LS
{
    public class PlayerUITeleportLocationManager : MonoBehaviour
    {
        [Header("Menu")]
        [SerializeField] GameObject menu;

        [SerializeField] GameObject[] teleportLocations;

        public void OpenTeleportLocationManagerMenu()
        {
            PlayerUIManager.instance.menuWindowIsOpen = true;
            menu.SetActive(true);

            CheckForUnlockedTeleport();
        }

        public void CloseTeleportLocationManagerMenu()
        {
            PlayerUIManager.instance.menuWindowIsOpen = false;
            menu.SetActive(false);
        }

        private void CheckForUnlockedTeleport()
        {
            bool hasFirstSelectedButton = false;
            for (int i = 0; i < teleportLocations.Length; i++)
            {
                for (int j = 0; j < WorldObjectManager.instance.sitesOfGrace.Count; j++)
                {
                    if (WorldObjectManager.instance.sitesOfGrace[j].siteOfGraceID == i)
                    {
                        if (WorldObjectManager.instance.sitesOfGrace[j].isActivated.Value)
                        {
                            teleportLocations[i].SetActive(true);

                            if (!hasFirstSelectedButton)
                            {
                                hasFirstSelectedButton = true;
                                teleportLocations[i].GetComponent<Button>().Select();
                                teleportLocations[i].GetComponent<Button>().OnSelect(null);
                            }
                        }
                        else
                        {
                            teleportLocations[i].SetActive(false);
                        }
                    }
                }
            }
        }

        public void TeleportToSiteOfGrace(int siteID)
        {
            for (int i = 0; i < WorldObjectManager.instance.sitesOfGrace.Count; i++)
            {
                if (WorldObjectManager.instance.sitesOfGrace[i].siteOfGraceID == siteID)
                {
                    //tp
                    WorldObjectManager.instance.sitesOfGrace[i].TeleportToSiteOfGrace();
                    return;
                }
            }
        }
    }
}
