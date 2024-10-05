using InventorySystem.Scripts;
using UnityEditor;
using UnityEngine;

namespace InventorySystem.Editor
{
    [CustomPropertyDrawer(typeof(Recipe))]
    public class RecipeDrawer : PropertyDrawer
    {
        public bool showRecipe = true;
        public int baseAmount = 1;
        public int amount = 1;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 18f * amount;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUIUtility.wideMode = true;
            position.height /= amount;
            EditorGUI.PropertyField(position, property);

            DrawRecipeBox(position, property, label);
        }

        private void DrawRecipeBox(Rect position, SerializedProperty property, GUIContent label)
        {
            var buttonRect = new Rect(position.x, position.y, 165, position.height);

            if (property.objectReferenceValue != null)
            {
                DrawShowRecipeButton(ref buttonRect);

                HandleSerializedObject(position, property);
            }
        }

        private void DrawShowRecipeButton(ref Rect buttonRect)
        {
            buttonRect.x += 165;
            buttonRect.width = 85;

            if (GUI.Button(buttonRect, new GUIContent("Show Recipe")))
            {
                showRecipe = !showRecipe;
                Debug.Log($"Show Recipe: {showRecipe}");
            }
        }

        private void HandleSerializedObject(Rect position, SerializedProperty property)
        {
            if (property.objectReferenceValue == null) return;

            SerializedObject serializedObject = new SerializedObject(property.objectReferenceValue);

            position.y += position.height;

            if (showRecipe)
            {
                DrawRecipeContent(position, serializedObject);
            }
            else
            {
                amount = baseAmount;
            }
        }

        private void DrawRecipeContent(Rect position, SerializedObject serializedObject)
        {
            amount = 3; // Expanded height for showing recipes

            SerializedProperty pattern = serializedObject.FindProperty("enteredItems");
            SerializedProperty product = serializedObject.FindProperty("producedItems");

            SerializedProperty factorsAmount = serializedObject.FindProperty("amountOfEnteredItems");
            SerializedProperty productsAmount = serializedObject.FindProperty("amountOfProducedItems");

            var patternPos = new Rect(position.x, position.y, position.width, position.height + 10);

            DrawItemPattern(patternPos, pattern, factorsAmount, product, productsAmount);
        }

        private void DrawItemPattern(Rect patternPos, SerializedProperty pattern, SerializedProperty factorsAmount, 
                                     SerializedProperty product, SerializedProperty productsAmount)
        {
            for (int i = 0; i < pattern.arraySize; i++)
            {
                DrawFactorItem(ref patternPos, pattern, factorsAmount, i);

                DrawPlusOrEquals(ref patternPos, i, pattern.arraySize);

                if (i == pattern.arraySize - 1)
                {
                    DrawProductItems(ref patternPos, product, productsAmount);
                }
            }
        }

        private void DrawFactorItem(ref Rect patternPos, SerializedProperty pattern, SerializedProperty factorsAmount, int i)
        {
            Item item = pattern.GetArrayElementAtIndex(i).objectReferenceValue as Item;
            int fAmount = factorsAmount.GetArrayElementAtIndex(i).intValue;

            GUIContent content = CreateItemContent(item, fAmount);

            EditorGUI.LabelField(patternPos, content);

            patternPos.x += 36;
        }

        private GUIContent CreateItemContent(Item item, int amounts)
        {
            var content = new GUIContent
            {
                text = item == null ? "null" : "",
                image = item?.icon.texture
            };

            GUIContent amountContent = new GUIContent
            {
                text = amounts == 0 ? "" : amounts.ToString()
            };

            return new GUIContent(content.text + amountContent.text, content.image);
        }

        private void DrawPlusOrEquals(ref Rect patternPos, int i, int arraySize)
        {
            string symbol = i < arraySize - 1 ? "+" : "=";
            EditorGUI.LabelField(patternPos, symbol);

            patternPos.x += 15;
        }

        private void DrawProductItems(ref Rect patternPos, SerializedProperty product, SerializedProperty productsAmount)
        {
            for (int j = 0; j < product.arraySize; j++)
            {
                Item productItem = product.GetArrayElementAtIndex(j).objectReferenceValue as Item;
                int pAmount = productsAmount.GetArrayElementAtIndex(j).intValue;

                GUIContent productContent = CreateItemContent(productItem, pAmount);

                EditorGUI.LabelField(patternPos, productContent);

                patternPos.x += 36;
            }
        }
    }
}
