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
                    foreach (var model in player.playerEquipmentManager.fullHelmets)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true); 
                        }
                    }
                    break;
                case EquipmentModelType.HalfHelmet:
                    foreach (var model in player.playerEquipmentManager.halfHelmets)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.HelmetAccessories:
                    foreach (var model in player.playerEquipmentManager.helmetAccessories)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.FaceCover:
                    foreach (var model in player.playerEquipmentManager.faceCovers)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Hood:
                    foreach (var model in player.playerEquipmentManager.hoods)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Torso:
                    foreach (var model in player.playerEquipmentManager.fullBodies)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Back:
                    foreach (var model in player.playerEquipmentManager.backAccessories)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightShoulder:
                    foreach (var model in player.playerEquipmentManager.rightShoulders)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightUpperArm:
                    foreach (var model in player.playerEquipmentManager.rightUpperArms)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightElbow:
                    foreach (var model in player.playerEquipmentManager.rightElbows)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightLowerArm:
                    foreach (var model in player.playerEquipmentManager.rightLowerArms)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightHand:
                    foreach (var model in player.playerEquipmentManager.rightHands)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftShoulder:
                    foreach (var model in player.playerEquipmentManager.leftShoulders)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftUpperArm:
                    foreach (var model in player.playerEquipmentManager.leftUpperArms)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftElbow:
                    foreach (var model in player.playerEquipmentManager.leftElbows)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftLowerArm:
                    foreach (var model in player.playerEquipmentManager.leftLowerArms)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftHand:
                    foreach (var model in player.playerEquipmentManager.leftHands)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Hips:
                    foreach (var model in player.playerEquipmentManager.hips)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.HipsAttachment:
                    foreach (var model in player.playerEquipmentManager.hipAccessories)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightLeg:
                    foreach (var model in player.playerEquipmentManager.rightLegs)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightKnee:
                    foreach (var model in player.playerEquipmentManager.rightKnees)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftLeg:
                    foreach (var model in player.playerEquipmentManager.leftLegs)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftKnee:
                    foreach (var model in player.playerEquipmentManager.leftKnees)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
