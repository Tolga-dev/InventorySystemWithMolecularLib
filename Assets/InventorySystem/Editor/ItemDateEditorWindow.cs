using System;
using InventorySystem.Scripts;
using UnityEditor;
using UnityEngine;

namespace InventorySystem.Editor
{
    public class ItemDateEditorWindow : UserEditorWindow 
    {
        public SerializedObject serializedObject;
        public SerializedProperty currentProperty;
        
        public Vector2 sideBar;
        public Vector2 contentBar;
        
        private string selectedPropertyPath;
        protected SerializedProperty selectedProperty;
        protected bool showItemAssets;

        public static void Open(ItemDataBase target)
        {
            var window = GetWindow<ItemDateEditorWindow>("Item Data Base Editor");
            window.serializedObject = new SerializedObject(target);
            
        }

        private void OnGUI()
        {
            if (serializedObject == null) return;

            currentProperty = serializedObject.FindProperty("itemList");

            DrawSideBar();
        }

        private void DrawSideBar()
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));

            sideBar = EditorGUILayout.BeginScrollView(sideBar);
            
            DrawSidebar(currentProperty);
            
            EditorGUILayout.EndScrollView();
            
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));

            if (selectedProperty != null)
            {
                contentBar = EditorGUILayout.BeginScrollView(contentBar);

                if (showItemAssets == false) 
                    EditorGUILayout.PropertyField(selectedProperty, true);
                else
                {
                    serializedObject.FindProperty("dataBaseId").intValue = EditorGUILayout.IntField(new GUIContent("Item Asset id"), serializedObject.FindProperty("dataBaseId").intValue);
                    serializedObject.FindProperty("dataBaseName").stringValue = EditorGUILayout.TextField(new GUIContent("Item Asset key"), serializedObject.FindProperty("dataBaseName").stringValue);

                }
                EditorGUILayout.EndScrollView();
            }
            else
            {
                EditorGUILayout.LabelField("Select an item from the list");
            }
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            
            serializedObject.ApplyModifiedProperties();
        }

        protected void DrawSidebar(SerializedProperty prop)
        {
            if (GUILayout.Button("Item Asset"))
            {
                selectedPropertyPath = prop.propertyPath;
                showItemAssets = true;
            }

            foreach (SerializedProperty p in prop)
            {
                if (GUILayout.Button((p.objectReferenceValue as Item).name))
                {
                    selectedPropertyPath = p.propertyPath;
                    showItemAssets = false;
                }
            }
            if (!string.IsNullOrEmpty(selectedPropertyPath))
            {
                selectedProperty = serializedObject.FindProperty(selectedPropertyPath);
            }
        }
    }
}