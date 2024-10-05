using System;
using InventorySystem.Scripts;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace InventorySystem.Editor
{
    [CustomEditor(typeof(Item), true)]
    public class ItemEditor : UnityEditor.Editor
    {
        public SerializedProperty id;
        public SerializedProperty itemName;
        public SerializedProperty description;
        public SerializedProperty icon;

        public SerializedProperty isStackable;
        public SerializedProperty maxStackSize;
        
        public SerializedProperty isConsumable;
        public SerializedProperty maxUsesInOnceTime;
        
        // temp
        private bool _itemFoldOut;
        private bool _storageFoldOut;
        private bool _consumeFoldOut;
        
        private void OnEnable()
        {
            // item properties
            id = serializedObject.FindProperty("id");
            itemName = serializedObject.FindProperty("itemName");
            description = serializedObject.FindProperty("description");
            icon = serializedObject.FindProperty("icon");
            
            // item stack properties
            isStackable = serializedObject.FindProperty("isStackable");
            maxStackSize = serializedObject.FindProperty("maxStackSize");
            
            // item consumable properties
            isConsumable = serializedObject.FindProperty("isConsumable");
            maxUsesInOnceTime = serializedObject.FindProperty("maxUsesInOnceTime");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            ItemFoldOut();
            
            AddSpace();

            StorageFoldOut();

            AddSpace();

            ConsumeFoldOut();
            

            serializedObject.ApplyModifiedProperties();
        }

        private void ConsumeFoldOut()
        {
            _consumeFoldOut = EditorGUILayout.Foldout(_consumeFoldOut, new GUIContent("Consume Configuration"), true, EditorStyles.foldoutHeader);

            if (_consumeFoldOut)
            {
                AddTab();
                
                isConsumable.boolValue = EditorGUILayout.Toggle(new GUIContent("Is Consumable"), isConsumable.boolValue);
                
                if (isConsumable.boolValue)
                {
                    maxUsesInOnceTime.intValue = EditorGUILayout.IntField(new GUIContent("Max Use In Once Time"), maxUsesInOnceTime.intValue);
                }
                
                BackTab();
            }

        }

        private void StorageFoldOut()
        {
            _storageFoldOut = EditorGUILayout.Foldout(_storageFoldOut, new GUIContent("Storage Configuration"), true, EditorStyles.foldoutHeader);
            
            if (_storageFoldOut)
            {
                AddTab();
                
                isStackable.boolValue = EditorGUILayout.Toggle(new GUIContent("Is Stackable"), isStackable.boolValue);
                if (isStackable.boolValue)
                {
                    maxStackSize.intValue = EditorGUILayout.IntField(new GUIContent("Max Stack Size"), maxStackSize.intValue);
                }
                
                BackTab();
            }
            
        }


        private void ItemFoldOut()
        {
            _itemFoldOut = EditorGUILayout.Foldout(_itemFoldOut, new GUIContent("Item Configuration"), true,
                EditorStyles.foldout);
            
            if (_itemFoldOut)
            {
                AddTab();
                
                itemName.stringValue = EditorGUILayout.TextField(new GUIContent("Item Name"), itemName.stringValue);
                id.intValue = EditorGUILayout.IntField(new GUIContent("Id"), id.intValue);
                description.stringValue = EditorGUILayout.TextField(new GUIContent("Item Description"), description.stringValue);
                
                EditorGUILayout.ObjectField(icon, new GUIContent("Item Icon"));
                var itemIcon = icon.objectReferenceValue as Sprite;
                if(itemIcon != null)
                    EditorGUILayout.LabelField(new GUIContent(itemIcon.texture), GUILayout.Height(54));
                
                BackTab();
                
            }
            
        }

        private void AddTab()
        {
            EditorGUI.indentLevel++;
        }
        private void BackTab()
        {
            EditorGUI.indentLevel--;
        }
        private void AddSpace()
        {
            EditorGUILayout.Separator();
        }

    }
}