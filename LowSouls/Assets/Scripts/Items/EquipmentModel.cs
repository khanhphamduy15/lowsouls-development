using UnityEngine;

namespace LS {
    [CreateAssetMenu(menuName = "Equipment Model")]
    public class EquipmentModel : ScriptableObject
    {
        //Type
        public EquipmentModelType equipmentModelType;
        public string equipmentName;

        //Name

        public void LoadModel(PlayerManager player)
        {
            switch (equipmentModelType)
            {
                case EquipmentModelType.FullHelmet:
                    foreach (var model in player.playerEquipmentManager.fullHelmetObjects)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true); 
                        }
                    }
                    break;
                case EquipmentModelType.OpenHelmet:
                    break;
                case EquipmentModelType.HelmetAccessories:
                    break;
                case EquipmentModelType.FaceCover:
                    break;
                case EquipmentModelType.Torso:
                    break;
                case EquipmentModelType.Back:
                    break;
                case EquipmentModelType.RightShoulder:
                    break;
                case EquipmentModelType.RightUpperArm:
                    break;
                case EquipmentModelType.RightElbow:
                    break;
                case EquipmentModelType.RightLowerArm:
                    break;
                case EquipmentModelType.RightHand:
                    break;
                case EquipmentModelType.LeftShoulder:
                    break;
                case EquipmentModelType.LeftUpperArm:
                    break;
                case EquipmentModelType.LeftElbow:
                    break;
                case EquipmentModelType.LeftLowerArm:
                    break;
                case EquipmentModelType.LeftHand:
                    break;
                case EquipmentModelType.Hips:
                    break;
                case EquipmentModelType.HipsAttachment:
                    break;
                case EquipmentModelType.RightLeg:
                    break;
                case EquipmentModelType.RightKnee:
                    break;
                case EquipmentModelType.LeftLeg:
                    break;
                case EquipmentModelType.LeftKnee:
                    break;
                default:
                    break;
            }
        }
    }
}
