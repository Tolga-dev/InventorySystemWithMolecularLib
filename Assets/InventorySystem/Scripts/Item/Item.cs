using UnityEngine;

namespace InventorySystem.Scripts
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item/Item", order = 0)]
    public class Item : ScriptableObject
    {
        [Header("Default Item Values")]
        public int id;
        public string itemName;
        public string description;
        public Sprite icon;
        
        [Header("Stack")]
        public bool isStackable;
        public int maxStackSize;
        
        [Header("Consumable")]
        public bool isConsumable;
        public int maxUsesInOnceTime;
        
        
        
        
        
    }
}