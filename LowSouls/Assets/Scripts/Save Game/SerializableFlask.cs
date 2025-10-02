using UnityEngine;

namespace LS
{
    [System.Serializable]
    public class SerializableFlask : ISerializationCallbackReceiver
    {
        [SerializeField] public int itemID;

        public FlaskItem GetFlask()
        {
            FlaskItem flaskItem = WorldItemDatabase.instance.GetFlaskFromSerializableData(this);
            return flaskItem;
        }
        public void OnAfterDeserialize()
        {
        }

        public void OnBeforeSerialize()
        {
        }
    }
}
