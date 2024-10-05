using UnityEngine;

namespace InventorySystem.Scripts
{
    [CreateAssetMenu(fileName = "PatternRecipe", menuName = "PatternRecipe", order = 134), System.Serializable]
    public class PatternRecipe : ScriptableObject
    {
        public int recipeId;
        public string recipeName;
        
        public int numberOfEnteredItems;
        public Item[] enteredItems;
        public int[] amountOfEnteredItems;
        
        public int numberOfProducedItems;
        public Item[] producedItems;
        public int[] amountOfProducedItems;

        public Vector2Int gridSize;

    }
}