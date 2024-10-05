using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Scripts.Inventories
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory", order = 0)]
    public class InventorySave : ScriptableObject
    {
        public List<Slot> slots = new List<Slot>();
        
    }

    [Serializable]
    public class Slot
    {
        public Item itemInSlot;
        public int amount;
    }
    
}