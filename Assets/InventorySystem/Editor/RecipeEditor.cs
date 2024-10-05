using System;
using InventorySystem.Scripts;
using UnityEditor;
using UnityEngine;

namespace InventorySystem.Editor
{
    [CustomEditor(typeof(Recipe), true)]
    public class RecipeEditor : UnityEditor.Editor
    {
        private SerializedProperty recipeName;
        private SerializedProperty recipeId;

        private SerializedProperty numberOfEnteredItems;
        private SerializedProperty enteredItems;
        private SerializedProperty amountOfEnteredItems;

        private SerializedProperty numberOfProducedItems;
        private SerializedProperty producedItems;
        private SerializedProperty amountOfProducedItems;


        private void OnEnable()
        {
            recipeId = serializedObject.FindProperty("recipeId");
            recipeName = serializedObject.FindProperty("recipeName");
            
            numberOfEnteredItems = serializedObject.FindProperty("numberOfEnteredItems");
            enteredItems = serializedObject.FindProperty("enteredItems");
            amountOfEnteredItems = serializedObject.FindProperty("amountOfEnteredItems");
            
            amountOfProducedItems = serializedObject.FindProperty("amountOfProducedItems");
            producedItems = serializedObject.FindProperty("producedItems");
            numberOfProducedItems = serializedObject.FindProperty("numberOfProducedItems");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Draw Recipe Database Fields
            DrawRecipeDatabaseFields();

            // Draw Entered Items Section
            DrawItemsSection("Input Items", numberOfEnteredItems, enteredItems, amountOfEnteredItems);

            // Draw Produced Items Section
            DrawItemsSection("Produced Items", numberOfProducedItems, producedItems, amountOfProducedItems);

            // Draw Recipe Visualization
            DrawRecipeVisualization();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawRecipeDatabaseFields()
        {
            EditorGUILayout.PropertyField(recipeId, new GUIContent("Data Base Id"));
            EditorGUILayout.PropertyField(recipeName, new GUIContent("Data Base Name"));
        }

        private void DrawItemsSection(string label, SerializedProperty itemCount, SerializedProperty items, SerializedProperty amounts)
        {
            EditorGUILayout.PropertyField(itemCount, new GUIContent($"Number of {label}"));
            
            items.arraySize = itemCount.intValue;
            amounts.arraySize = itemCount.intValue;

            items.isExpanded = EditorGUILayout.Foldout(items.isExpanded, label);

            if (items.isExpanded)
            {
                IncreaseIndent();

                for (int i = 0; i < items.arraySize; i++)
                {
                    DrawItemField(i, items, amounts, label);
                    EditorGUILayout.Separator();
                }

                if (items.arraySize == 0)
                {
                    EditorGUILayout.LabelField("Array is empty!");
                }

                DecreaseIndent();
            }
        }

        private void DrawItemField(int index, SerializedProperty items, SerializedProperty amounts, string label)
        {
            EditorGUILayout.ObjectField(items.GetArrayElementAtIndex(index), new GUIContent($"{label} {index}"));
            amounts.GetArrayElementAtIndex(index).intValue = EditorGUILayout.IntField(
                new GUIContent($"Amount {index}"), amounts.GetArrayElementAtIndex(index).intValue);
        }

        private void DrawRecipeVisualization()
        {
            Separator();
            EditorGUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(true));

            // Display entered items
            DisplayItems(enteredItems, amountOfEnteredItems, "Null", "+");

            // Display equals sign
            EditorGUILayout.LabelField("=", GUILayout.Height(36), GUILayout.Width(36));

            // Display produced items
            DisplayItems(producedItems, amountOfProducedItems, "Null", string.Empty);

            EditorGUILayout.EndHorizontal();
        }

        private void DisplayItems(SerializedProperty items, SerializedProperty amounts, string nullText, string separator)
        {
            var recipeContent = new GUIContent();

            for (int i = 0; i < items.arraySize; i++)
            {
                var item = items.GetArrayElementAtIndex(i).objectReferenceValue as Item;
                if (item == null)
                {
                    recipeContent.text = nullText;
                    recipeContent.image = null;
                }
                else
                {
                    recipeContent.text = amounts.GetArrayElementAtIndex(i).intValue.ToString();
                    recipeContent.image = item.icon.texture;
                }

                EditorGUILayout.LabelField(recipeContent, GUILayout.Height(36), GUILayout.Width(36));

                if (!string.IsNullOrEmpty(separator) && i < items.arraySize - 1)
                {
                    EditorGUILayout.LabelField(separator, GUILayout.Height(36), GUILayout.Width(36));
                }
            }
        }

        private void IncreaseIndent() => EditorGUI.indentLevel++;
        private void DecreaseIndent() => EditorGUI.indentLevel--;

        private void Separator() => EditorGUILayout.Separator();
    }
}
