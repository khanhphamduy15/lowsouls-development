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

        //item list
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
            //create unique id for every item
            for (int i = 0; i < items.Count; i++)
            {
                items[i].itemID = i;
            }
        }
        
        public WeaponItem getWeaponByID(int ID)
        {
            return weapons.FirstOrDefault(weapon => weapon.itemID == ID);
        }
    }
}
