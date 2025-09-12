using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LS
{
    public class WorldItemDatabase : MonoBehaviour
    {
        public static WorldItemDatabase instance;

        public WeaponItem unarmedWeapon;

        [Header("Weapons")]
        [SerializeField] List<WeaponItem> weapons = new List<WeaponItem>();

        [Header("Head Equipments")]
        [SerializeField] List<HeadEquipmentItem> headEquipments = new List<HeadEquipmentItem>();

        [Header("Body Equipments")]
        [SerializeField] List<BodyEquipmentItem> bodyEquipments = new List<BodyEquipmentItem>();

        [Header("Leg Equipments")]
        [SerializeField] List<LegEquipmentItem> legEquipments = new List<LegEquipmentItem>();

        [Header("Hand Equipments")]
        [SerializeField] List<HandEquipmentItem> handEquipments = new List<HandEquipmentItem>();


        //item list
        [Header("Items")]
        [SerializeField] List<Item> items = new List<Item>();
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
            //add all weapon to item list
            foreach (var weapon in weapons)
            {
                items.Add(weapon);
            }
            foreach (var item in headEquipments)
            {
                items.Add(item);
            }
            foreach (var item in bodyEquipments)
            {
                items.Add(item);
            }
            foreach (var item in legEquipments)
            {
                items.Add(item);
            }
            foreach (var item in handEquipments)
            {
                items.Add(item);
            }
            //create unique id for every item
            for (int i = 0; i < items.Count; i++)
            {
                items[i].itemID = i;    
            }
        }
        
        public WeaponItem GetWeaponByID(int ID)
        {
            return weapons.FirstOrDefault(weapon => weapon.itemID == ID);
        }

        public HeadEquipmentItem GetHeadEquipmentByID(int ID)
        {
            return headEquipments.FirstOrDefault(item => item.itemID == ID);
        }

        public BodyEquipmentItem GetBodyEquipmentByID(int ID)
        {
            return bodyEquipments.FirstOrDefault(item => item.itemID == ID);
        }

        public LegEquipmentItem GetLegEquipmentByID(int ID)
        {
            return legEquipments.FirstOrDefault(item => item.itemID == ID);
        }

        public HandEquipmentItem GetHandEquipmentByID(int ID)
        {
            return handEquipments.FirstOrDefault(item => item.itemID == ID);
        }
    }
}
