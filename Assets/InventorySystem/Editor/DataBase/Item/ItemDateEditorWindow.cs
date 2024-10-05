using System;
using InventorySystem.Scripts;
using UnityEditor;
using UnityEngine;

namespace InventorySystem.Editor
{
    public class ItemDateEditorWindow : UserEditorWindow
    {
        public Vector2 sideBar;
        public Vector2 contentBar;

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
                    serializedObject.FindProperty("dataBaseId").intValue = EditorGUILayout.IntField(
                        new GUIContent("Item Asset id"), serializedObject.FindProperty("dataBaseId").intValue);
                    serializedObject.FindProperty("dataBaseName").stringValue = EditorGUILayout.TextField(
                        new GUIContent("Item Asset key"), serializedObject.FindProperty("dataBaseName").stringValue);

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

        private void DrawSidebar(SerializedProperty prop)
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

            if (GUILayout.Button("Add new item"))
            {
                CreateScriptableObjectAsset window = GetWindow<CreateScriptableObjectAsset>("Create Item");
                window.property = prop;
            }

            if (!string.IsNullOrEmpty(selectedPropertyPath))
            {
                selectedProperty = serializedObject.FindProperty(selectedPropertyPath);
            }
        }
    }

    public class CreateScriptableObjectAsset : EditorWindow
    {
        public string itemName;
        public SerializedProperty property;
        public string path = "";
        public string defaultPath = "Assets/Items";

        [MenuItem("Assets/Create ScriptableObject Asset")]
        public static void ShowWindow()
        {
            GetWindow<CreateScriptableObjectAsset>("Create Item Asset");
        }
        private void OnGUI()
        {
            EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));

            path = EditorPrefs.GetString("SavedPath");
            if(path == "")
                path = defaultPath;

            path = EditorGUILayout.TextField("Default Path to save", path);
        
            if (GUILayout.Button("Select Path")) 
            {
                var selectedPath = EditorUtility.SaveFolderPanel("Select Folder to Save Item", path, "");
                if (!string.IsNullOrEmpty(selectedPath))
                {
                    path = FileUtil.GetProjectRelativePath(selectedPath); // Ensure the path is relative to the project
                    EditorPrefs.SetString("SavedPath", path);
                }
            }

            if (string.IsNullOrEmpty(itemName)) itemName = "Untitled";
            itemName = EditorGUILayout.TextField("Name of the item", itemName);

            if (GUILayout.Button("Create Item Asset"))
            {
                if (string.IsNullOrEmpty(path))
                {
                    EditorUtility.DisplayDialog("Error", "Please specify a valid path.", "OK");
                }
                else
                {
                    CreateItemAsset(path, itemName, property);
                    AssetDatabase.Refresh();
                }
            }

            EditorGUILayout.EndVertical();
        }
        public static Item CreateItemAsset(string path, string soName, SerializedProperty prop)
        {
            if (!AssetDatabase.IsValidFolder(path))
            {
                string parentFolder = System.IO.Path.GetDirectoryName(path);
                string newFolder = System.IO.Path.GetFileName(path);

                AssetDatabase.CreateFolder(parentFolder, newFolder);
            }
            
            Item asset = CreateInstance<Item>();
            
            path += $"/{soName}.asset";

            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();

            prop.arraySize++;
            prop.GetArrayElementAtIndex(prop.arraySize - 1).objectReferenceValue = asset;

            return asset;
        }

    }
}