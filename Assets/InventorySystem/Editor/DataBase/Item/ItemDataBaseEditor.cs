using System;
using InventorySystem.Core;
using InventorySystem.Scripts;
using UnityEditor;
using UnityEngine;
using UniversalInventorySystem.Editors;

namespace InventorySystem.Editor
{
    [CustomEditor(typeof(ItemDataBase), true)]
    public class ItemDataBaseEditor : EditorCore
    {
        public SerializedProperty itemList;
        public SerializedProperty dataBaseName;
        public SerializedProperty dataBaseId;

        private void OnEnable()
        {
            itemList = serializedObject.FindProperty("itemList");
            dataBaseName = serializedObject.FindProperty("dataBaseId");
            dataBaseId = serializedObject.FindProperty("dataBaseName");
            
        } 

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(dataBaseId);
            EditorGUILayout.PropertyField(dataBaseName);

            Separator();
            
            itemList.isExpanded = EditorGUILayout.Foldout(itemList.isExpanded, new GUIContent("Item List"), true);
            
            if (itemList.isExpanded)
            {
                IncreaseIndent();

                itemList.arraySize = EditorGUILayout.IntField(new GUIContent("Size"), itemList.arraySize);

                for (int i = 0; i < itemList.arraySize; i++)
                {
                    EditorGUILayout.BeginHorizontal();
            
                    // Object field for each item
                    EditorGUILayout.ObjectField(itemList.GetArrayElementAtIndex(i), new GUIContent($"Item {i}"));

                    // Button to remove the item
                    if (GUILayout.Button("Remove", GUILayout.Width(60)))
                    {
                        var item = itemList.GetArrayElementAtIndex(i).objectReferenceValue;
                        if (item != null)
                        {
                            var assetPath = AssetDatabase.GetAssetPath(item);
                            AssetDatabase.DeleteAsset(assetPath);
                        }
                        
                        itemList.DeleteArrayElementAtIndex(i); // Remove the item at index i
                    }

                    EditorGUILayout.EndHorizontal();
                }

                if (GUILayout.Button("Add New Item"))
                {
                    itemList.arraySize++; // Increase the size of the array
                }
                DecreaseIndent();
            }
            
            if (GUILayout.Button("Open Editor"))
            {
                ItemDateEditorWindow.Open((ItemDataBase)target);
            }
            serializedObject.ApplyModifiedProperties();
        }

        
    }
}