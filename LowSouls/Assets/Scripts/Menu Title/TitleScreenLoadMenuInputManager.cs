using UnityEngine;

namespace LS
{
    public class TitleScreenLoadMenuInputManager : MonoBehaviour
    {
        PlayerControls playerControls;

        [Header("Title Screen Inputs")]
        [SerializeField] bool deleteCharSlot = false;

        private void Update()
        {
            if (deleteCharSlot)
            {
                deleteCharSlot = false;
                TitleScreenManager.instance.AttemptToDeleteCharSlot();
            }
        }

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();
                playerControls.UI.X.performed += i => deleteCharSlot = true;
            }
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }
    }
}
