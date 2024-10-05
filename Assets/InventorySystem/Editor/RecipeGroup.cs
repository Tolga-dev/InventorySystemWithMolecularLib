using System.Collections.Generic;
using InventorySystem.Scripts;
using UnityEngine;

namespace InventorySystem.Editor
{
    [CreateAssetMenu(fileName = "RecipeGroup", menuName = "RecipeGroup", order = 1), System.Serializable]
    public class RecipeGroup : ScriptableObject
    {
        public List<Recipe> recipesList = new List<Recipe>();
        public List<PatternRecipe> recipesPatternList = new List<PatternRecipe>();
        [Space]
        public string strId;
        public int id;
    }
}