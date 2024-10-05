using InventorySystem.Scripts;
using UnityEditor;
using UnityEngine;

namespace InventorySystem.Editor
{
    [CustomEditor(typeof(PatternRecipe))]
    public class PatternRecipeEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open Editor"))
            {
                PatternRecipeEditorWindow.Open((PatternRecipe)target);
            }

            var currentPattern = serializedObject.FindProperty("pattern");
            var gridSize = serializedObject.FindProperty("gridSize").vector2IntValue;

            var content = new GUIContent();
            EditorGUILayout.BeginVertical("box");

            for (int i = 0; i < gridSize.y; i++)
            {
                EditorGUILayout.BeginHorizontal();

                for (int j = 0; j < gridSize.x; j++)
                {
                    var item = currentPattern.GetArrayElementAtIndex(i * gridSize.x + j).objectReferenceValue as Item;
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
                EditorGUILayout.EndHorizontal();
            }
            
            var products = serializedObject.FindProperty("products");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("=", GUILayout.Width(10), GUILayout.Height(36));

            for (int i = 0; i < products.arraySize; i++)
            {
                var productItem = products.GetArrayElementAtIndex(i).objectReferenceValue as Item;
                
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
            
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.SelectableLabel($"id: {serializedObject.FindProperty("id").intValue}, key: {serializedObject.FindProperty("key").stringValue}");
            EditorGUILayout.EndVertical();
        }

    }
}