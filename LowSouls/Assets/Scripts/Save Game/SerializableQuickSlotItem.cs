using UnityEngine;

namespace LS
{
    [System.Serializable]
    public class SerializableQuickSlotItem : ISerializationCallbackReceiver
    {
        [SerializeField] public int itemID;
        [SerializeField] public int itemAmount;

        public QuickSlotItem GetQuickSlotItem()
        {
            QuickSlotItem quickSlotItem = WorldItemDatabase.instance.GetQuickSlotItemFromSerializableData(this);
            return quickSlotItem;
        }
        public void OnAfterDeserialize()
        {
        }

        public void OnBeforeSerialize()
        {
        }
    }
}
