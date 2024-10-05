using System;
using UnityEngine;

namespace InventorySystem.Scripts
{
    [Serializable]
    [CreateAssetMenu(fileName = "PatternRecipe", menuName = "PatternRecipe", order = 1)]
    public class PatternRecipe : ScriptableObject
    {
        public int numberOfFactors;
        public Item[] factors;

        public int numberOfProducts;
        public Item[] products;
        public int[] amountProducts;

        public Vector2Int gridSize;

        public Item[] pattern;
        public int[] amountPattern;

        public int id;
        public string key;
    }
}