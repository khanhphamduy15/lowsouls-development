using UnityEngine;

namespace LS
{
    public class WeaponItemAction : ScriptableObject
    {
        public int actionID;

        public virtual void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            
        }
    }
}
