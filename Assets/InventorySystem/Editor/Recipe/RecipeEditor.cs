using System;
using InventorySystem.Core;
using InventorySystem.Scripts;
using UnityEditor;
using UnityEngine;

namespace InventorySystem.Editor
{
    [CustomEditor(typeof(Recipe), true)]
    public class RecipeEditor : EditorCore
    {
        private SerializedProperty _recipeName;
        private SerializedProperty _recipeId;

        private SerializedProperty _numberOfEnteredItems;
        private SerializedProperty _enteredItems;
        private SerializedProperty _amountOfEnteredItems;

        private SerializedProperty _numberOfProducedItems;
        private SerializedProperty _producedItems;
        private SerializedProperty _amountOfProducedItems;


        private void OnEnable()
        {
            _recipeId = serializedObject.FindProperty("recipeId");
            _recipeName = serializedObject.FindProperty("recipeName");
            
            _numberOfEnteredItems = serializedObject.FindProperty("numberOfEnteredItems");
            _enteredItems = serializedObject.FindProperty("enteredItems");
            _amountOfEnteredItems = serializedObject.FindProperty("amountOfEnteredItems");
            
            _amountOfProducedItems = serializedObject.FindProperty("amountOfProducedItems");
            _producedItems = serializedObject.FindProperty("producedItems");
            _numberOfProducedItems = serializedObject.FindProperty("numberOfProducedItems");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Draw Recipe Database Fields
            DrawRecipeDatabaseFields();

            // Draw Entered Items Section
            DrawItemsSection("Input Items", _numberOfEnteredItems, _enteredItems, _amountOfEnteredItems);

            // Draw Produced Items Section
            DrawItemsSection("Produced Items", _numberOfProducedItems, _producedItems, _amountOfProducedItems);

            // Draw Recipe Visualization
            DrawRecipeVisualization();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawRecipeDatabaseFields()
        {
            EditorGUILayout.PropertyField(_recipeId, new GUIContent("Data Base Id"));
            EditorGUILayout.PropertyField(_recipeName, new GUIContent("Data Base Name"));
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
            DisplayItems(_enteredItems, _amountOfEnteredItems, "Null", "+");

            // Display equals sign
            EditorGUILayout.LabelField("=", GUILayout.Height(36), GUILayout.Width(36));

            // Display produced items
            DisplayItems(_producedItems, _amountOfProducedItems, "Null", string.Empty);

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

        
    }
}
