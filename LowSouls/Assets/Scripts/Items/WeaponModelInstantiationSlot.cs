using UnityEngine;

namespace LS
{
    public class WeaponModelInstantiationSlot : MonoBehaviour
    {
        public WeaponModelSlot weaponSlot;
        public GameObject currentWeaponModel;
        public void UnloadWeapon()
        {
            if (currentWeaponModel != null)
            {
                Destroy(currentWeaponModel);
            }
        }

        public void PlaceWeaponModelIntoSlot(GameObject weaponModel)
        {
            currentWeaponModel = weaponModel;
            weaponModel.transform.parent = transform;

            weaponModel.transform.localPosition = Vector3.zero;
            weaponModel.transform.localRotation = Quaternion.identity;
            weaponModel.transform.localScale = Vector3.one;
        }

        public void PlaceWeaponModelInUnequippedSlot(GameObject weaponModel, WeaponClass weaponClass, PlayerManager player)
        {
            currentWeaponModel = weaponModel;
            weaponModel.transform.parent = transform;

            switch (weaponClass)
            {
                case WeaponClass.StraightSword:
                    weaponModel.transform.localPosition = new Vector3(0.064f, 0f, -0.06f);
                    weaponModel.transform.localRotation = Quaternion.Euler(194, 90, -0.22f);
                    break;
                case WeaponClass.Claymore:
                    weaponModel.transform.localPosition = new Vector3(0.064f, 0f, -0.06f);
                    weaponModel.transform.localRotation = Quaternion.Euler(194, 90, -0.22f);
                    break;
                case WeaponClass.MediumShield:
                    weaponModel.transform.localPosition = new Vector3(0.199f, -0.099f, -0.161f);
                    weaponModel.transform.localRotation = Quaternion.Euler(-155.459f, -0.3560181f, 1.291f);
                    break;
                default:
                    break;
            }
        }
    }
}
