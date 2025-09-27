using UnityEngine;

namespace LS {
    public class PlayerUIToggleHud : MonoBehaviour
    {
        private void OnEnable()
        {
            //hide hud
            PlayerUIManager.instance.playerUIHudManager.ToggleHUD(false);
        }

        private void OnDisable()
        {
            //open hud
            PlayerUIManager.instance.playerUIHudManager.ToggleHUD(true);
        }
    }
}
