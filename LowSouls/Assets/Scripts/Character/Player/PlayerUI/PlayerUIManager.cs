using UnityEngine;
using Unity.Netcode;

namespace LS {
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager instance;
        [HideInInspector] public PlayerManager localPlayer;

        [Header("NETWORK JOIN")]
        [SerializeField] bool startGameAsClient;
        [HideInInspector] public PlayerUIHudManager playerUIHudManager;
        [HideInInspector] public PlayerUIPopUpManager playerUIPopUpManager;
        [HideInInspector] public PlayerUICharacterMenuManager playerUICharacterMenuManager;
        [HideInInspector] public PlayerUIEquipmentMenuManager playerUIEquipmentMenuManager;
        [HideInInspector] public PlayerUISiteOfGraceManager playerUISiteOfGraceManager;
        [HideInInspector] public PlayerUITeleportLocationManager playerUITeleportLocationManager;
        [HideInInspector] public PlayerUILoadingScreenManager playerUILoadingScreenManager;


        [Header("UI Flags")]
        public bool menuWindowIsOpen = false;
        public bool subMenuWindowIsOpen = false;
        public bool popupWindowIsOpen = false;
        public bool AnyWindowIsOpen => menuWindowIsOpen || subMenuWindowIsOpen;

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
            playerUIHudManager = GetComponentInChildren<PlayerUIHudManager>();
            playerUIPopUpManager = GetComponentInChildren<PlayerUIPopUpManager>();
            playerUICharacterMenuManager = GetComponentInChildren<PlayerUICharacterMenuManager>();
            playerUIEquipmentMenuManager = GetComponentInChildren<PlayerUIEquipmentMenuManager>();
            playerUISiteOfGraceManager = GetComponentInChildren<PlayerUISiteOfGraceManager>();
            playerUITeleportLocationManager = GetComponentInChildren<PlayerUITeleportLocationManager>();
            playerUILoadingScreenManager = GetComponentInChildren<PlayerUILoadingScreenManager>();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (startGameAsClient) 
            {
                //Start client = turn off host, then restart as client
                startGameAsClient = false;
                NetworkManager.Singleton.Shutdown();
                NetworkManager.Singleton.StartClient();
            }
        }

        public void CloseAllMenuWindows()
        {
            playerUICharacterMenuManager.CloseCharacterMenu();
            playerUIEquipmentMenuManager.CloseEquipmentManagerMenu();
            playerUISiteOfGraceManager.CloseSiteOfGraceManagerMenu();
            playerUITeleportLocationManager.CloseTeleportLocationManagerMenu();
        }

        public void CloseAllSubMenuWindows()
        {
            playerUIEquipmentMenuManager.CloseEquipmentManagerMenu();
        }
    }
}

