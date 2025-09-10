using UnityEngine;
using Unity.Netcode;

namespace LS
{
    public class CharacterCombatManager : NetworkBehaviour
    {
        protected CharacterManager character;

        [Header("Last Attack Animation Performed")]
        public string lastAttackAnimation;

        [Header("Attack Type")]
        public AttackType currentAttackType;

        [Header("Attack Target")]
        public CharacterManager currentTarget;

        [Header("Lock On Transform")]
        public Transform lockOnTransform;

        [Header("Attack Flags")]
        public bool canPerformRollAttack = false;
        public bool canBlock = true;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public virtual void SetTarget(CharacterManager newTarget)
        {
            if (character.IsOwner)
            {
                if (newTarget != null)
                {
                    currentTarget = newTarget;
                    //Tell network we have a new target
                    character.characterNetworkManager.currentTargetNetworkObjectID.Value = newTarget.GetComponent<NetworkObject>().NetworkObjectId;
                }
                else
                {
                    currentTarget = null;
                }
            }
        }

        public void EnableIsInvulnerable()
        {
            if (character.IsOwner)
            character.characterNetworkManager.isInvulnerable.Value = true;
        }

        public void DisableIsInvulnerable()
        {
            if (character.IsOwner)
                character.characterNetworkManager.isInvulnerable.Value = false;

        }
        public void EnableCanDoRollingAttack()
        {
            canPerformRollAttack = true;
        }

        public void DisableCanDoRollingAttack()
        {
            canPerformRollAttack = false;
        }

        public virtual void EnableCanDoCombo()
        {

        }

        public virtual void DisableCanDoCombo()
        {

        }
    }
}
