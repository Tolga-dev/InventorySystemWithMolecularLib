using System;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.Scripts.Inventories
{
    public class InventoryCanvas : MonoBehaviour
    {
        public InventorySave inventorySave;

        public GameObject itemPrefab; // Prefab for the item to be instantiated
        public Transform itemParent; // Parent object to hold the instantiated items

        private void Start()
        {
            foreach (var slot in inventorySave.slots)
            {
                var instantiatedItem = Instantiate(itemPrefab, itemParent);
                var itemImage = instantiatedItem.GetComponent<Image>();
                itemImage.sprite = slot.itemInSlot.icon; // Set the sprite to the item's icon
            }
        }
    }
}
