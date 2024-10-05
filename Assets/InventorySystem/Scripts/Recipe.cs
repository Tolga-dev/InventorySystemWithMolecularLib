using System;
using UnityEngine;

namespace InventorySystem.Scripts
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "Recipe", order = 0)]
    public class Recipe : ScriptableObject
    {
        public int recipeId;
        public string recipeName;
        
        public int numberOfEnteredItems;
        public Item[] enteredItems;
        public int[] amountOfEnteredItems;

        public int numberOfProducedItems;
        public Item[] producedItems;
        public int[] amountOfProducedItems;
        
    }
}