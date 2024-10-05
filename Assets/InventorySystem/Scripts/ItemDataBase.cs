using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem.Scripts
{
    [Serializable]
    [CreateAssetMenu(fileName = "ItemDataBase", menuName = "Item/ItemDataBase", order = 0)]
    public class ItemDataBase : ScriptableObject
    {
        public string dataBaseName;
        public int dataBaseId;
        
        public List<Item> itemList = new List<Item>();
        public Item GetItemAtIndex(int index) { return itemList[index]; }

        public Item GetItemWithName(string itemName)
        {
            return itemList.FirstOrDefault(items => items.name == itemName);
        }
        public Item GetItemWithID(int id)
        {
            return itemList.FirstOrDefault(i => i.id == id);
        }
        
        
    }
}