using UnityEngine;
using Unity.Netcode;

namespace LS
{
    public class Interactable : NetworkBehaviour
    {
        public string interactableText; //interact text prompt
        [SerializeField] protected Collider interactableCollider; //check for player interaction
        [SerializeField] protected bool hostOnlyInteractable = true;

        protected virtual void Awake()
        {
            if (interactableCollider == null)
                interactableCollider = GetComponent<Collider>();
        }

        protected virtual void Start()
        {
            if (interactableCollider == null)
                interactableCollider = GetComponent<Collider>();
        }

        public virtual void Interact(PlayerManager player)
        {
            Debug.Log("Interacted");
            if (!player.IsOwner)
                return;

            interactableCollider.enabled = false;
            player.playerInteractionManager.RemoveInteractionFromList(this);
            PlayerUIManager.instance.playerUIPopUpManager.CloseAllPopUpWindows();
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            PlayerManager player = other.GetComponent<PlayerManager>();
            if (player != null) 
            {
                if (!player.playerNetworkManager.IsHost && hostOnlyInteractable)
                    return;

                if (!player.IsOwner)
                    return;

                player.playerInteractionManager.AddInteractionToList(this);
            }
        }

        public virtual void OnTriggerExit(Collider other)
        {
            PlayerManager player = other.GetComponent<PlayerManager>();
            if (player != null)
            {
                if (!player.playerNetworkManager.IsHost && hostOnlyInteractable)
                    return;

                if (!player.IsOwner)
                    return;

                player.playerInteractionManager.RemoveInteractionFromList(this);

                PlayerUIManager.instance.playerUIPopUpManager.CloseAllPopUpWindows();
            }
        }
    }
}
