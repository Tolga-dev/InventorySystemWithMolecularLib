using InventorySystem.Scripts;
using UnityEditor;
using UnityEngine;

namespace InventorySystem.Editor
{
    [CustomEditor(typeof(PatternRecipe))]
    public class PatternRecipeEditor : UnityEditor.Editor
    {
        public GUIContent content = new GUIContent();
        
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open Editor"))
            {
                PatternRecipeEditorWindow.Open((PatternRecipe)target);
            }

            DrawPatternGrid();
            DrawProductItems();
            DrawRecipeDetails();
            
        }

        private void DrawPatternGrid()
        {
            var currentPattern = serializedObject.FindProperty("pattern");
            var gridSize = serializedObject.FindProperty("gridSize").vector2IntValue;

            EditorGUILayout.BeginVertical("box");

            for (int i = 0; i < gridSize.y; i++)
            {
                EditorGUILayout.BeginHorizontal();

                for (int j = 0; j < gridSize.x; j++)
                {
                    DrawGridItem(currentPattern, i * gridSize.x + j);
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }

        private void DrawGridItem(SerializedProperty currentPattern, int index)
        {
            var item = currentPattern.GetArrayElementAtIndex(index).objectReferenceValue as Item;
            if (item == null)
            {
                content.text = "null";
                content.image = null;
            }
            else
            {
                content.text = "";
                content.image = item.icon.texture;
            }

            EditorGUILayout.LabelField(content, GUILayout.Width(36), GUILayout.Height(36));
        }

        private void DrawProductItems()
        {
            var products = serializedObject.FindProperty("products");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("=", GUILayout.Width(10), GUILayout.Height(36));

            for (int i = 0; i < products.arraySize; i++)
            {
                DrawProductItem(products, i);
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawProductItem(SerializedProperty products, int index)
        {
            var productItem = products.GetArrayElementAtIndex(index).objectReferenceValue as Item;

            if (productItem == null)
            {
                content.text = "null";
                content.image = null;
            }
            else
            {
                content.text = "";
                content.image = productItem.icon.texture;
            }

            EditorGUILayout.LabelField(content, GUILayout.Width(36), GUILayout.Height(36));
        }

        private void DrawRecipeDetails()
        {
            EditorGUILayout.SelectableLabel($"id: {serializedObject.FindProperty("id").intValue}, key: {serializedObject.FindProperty("key").stringValue}");
        }
    }
}