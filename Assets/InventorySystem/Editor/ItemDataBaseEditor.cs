using System;
using InventorySystem.Scripts;
using UnityEditor;
using UnityEngine;
using UniversalInventorySystem.Editors;

namespace InventorySystem.Editor
{
    [CustomEditor(typeof(ItemDataBase), true)]
    public class ItemDataBaseEditor : UnityEditor.Editor
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
                AddTab();

                itemList.arraySize = EditorGUILayout.IntField(new GUIContent("Size"), itemList.arraySize);

                for (int i = 0; i < itemList.arraySize; i++)
                {
                    EditorGUILayout.ObjectField(itemList.GetArrayElementAtIndex(i), new GUIContent($"Item {i}"));
                }
                BackTab();
            }
            
            if (GUILayout.Button("Open Editor"))
            {
                ItemDateEditorWindow.Open((ItemDataBase)target);
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void Separator()
        {
            EditorGUILayout.Separator();
        }

        private void AddTab()
        {
            EditorGUI.indentLevel++;
        }
        private void BackTab()
        {
            EditorGUI.indentLevel--;
        }
    }
}