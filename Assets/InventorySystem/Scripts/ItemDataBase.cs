using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Scripts
{
    [Serializable]
    [CreateAssetMenu(fileName = "ItemDataBase", menuName = "Item/ItemDataBase", order = 0)]
    public class ItemDataBase : ScriptableObject
    {
        public string dataBaseName;
        public string dataBaseId;
        
        public List<Item> itemList = new List<Item>();

        public Item GetItemWithName(string itemName)
        {
            foreach (var items in itemList)
            {
                if (items.name == itemName)
                    return items;
            }
            
            return null;
        }

        public void SortItems()
        {
            itemList.Sort();
        }
        
    }
}