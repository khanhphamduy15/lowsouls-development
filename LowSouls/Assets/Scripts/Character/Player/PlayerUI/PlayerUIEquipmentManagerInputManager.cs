using UnityEngine;

namespace LS
{
    public class PlayerUIEquipmentManagerInputManager : MonoBehaviour
    {
        PlayerControls playerControls;

        PlayerUIEquipmentMenuManager playerUIEquipmentMenuManager;
        [Header("Inputs")]
        [SerializeField] bool unequipItemInput;

        private void Awake()
        {
            playerUIEquipmentMenuManager = GetComponentInParent<PlayerUIEquipmentMenuManager>();
        }

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.PlayerActions.Unequipitem.performed += i => unequipItemInput = true;
            }
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        private void Update()
        {
            HandlePlayerUIEquipmentManagerInput();
        }

        private void HandlePlayerUIEquipmentManagerInput()
        {
            if (unequipItemInput)
            {
                unequipItemInput = false;
                playerUIEquipmentMenuManager.UnequipSelectedItem();
            }
        }
    }
}
