using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.Scripts.Crafting
{
    public class CrafterItemSpawner : MonoBehaviour
    {
        public PatternRecipe patternRecipe;
        public Recipe recipe;

        public GameObject item;

        public GameObject enterItemSpawnParent;
        public GameObject productItemSpawnParent;

        public GameObject recipeEnterItemSpawnParent;
        public GameObject recipeProductItemSpawnParent;

        public void Start()
        {
            SpawnItems(patternRecipe.pattern, enterItemSpawnParent.transform);
            SpawnItems(patternRecipe.products, productItemSpawnParent.transform);
            SpawnItems(recipe.enteredItems, recipeEnterItemSpawnParent.transform);
            SpawnItems(recipe.producedItems, recipeProductItemSpawnParent.transform);
        }

        private void SpawnItems<T>(IEnumerable<T> items, Transform parent) where T : Item
        {
            foreach (var itemData in items)
            {
                var instantiatedItem = Instantiate(item, parent);
                var itemImage = instantiatedItem.GetComponent<Image>();
                itemImage.sprite = (itemData).icon; // Set the sprite to the item's icon
            }
        }
    }
}