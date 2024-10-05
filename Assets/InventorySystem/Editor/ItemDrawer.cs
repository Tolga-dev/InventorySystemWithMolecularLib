using InventorySystem.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace InventorySystem.Editor
{
    [CustomPropertyDrawer(typeof(Item))]
    public class ItemDrawer : PropertyDrawer
    {
        private float baseAmount = 3;
        private float amountOfFields = 8f;
        private float total;

        private bool _useShorter;
        private bool _itemFoldout;
        private bool _storageFoldOut;
        private bool _consumeFoldOut;
        public bool unfold;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 18f * amountOfFields;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            amountOfFields = baseAmount + total;
            total = 0;

            SerializedObject serializedObject = null;
            if (property.objectReferenceValue != null)
                serializedObject = new SerializedObject(property.objectReferenceValue);

            if (serializedObject != null)
            {
                serializedObject.Update();

                var id = serializedObject.FindProperty("id");
                var itemName = serializedObject.FindProperty("itemName");
                var description = serializedObject.FindProperty("description");
                var icon = serializedObject.FindProperty("icon");

                // item stack properties
                var isStackable = serializedObject.FindProperty("isStackable");
                var maxStackSize = serializedObject.FindProperty("maxStackSize");

                // item consumable properties
                var isConsumable = serializedObject.FindProperty("isConsumable");
                var maxUsesInOnceTime = serializedObject.FindProperty("maxUsesInOnceTime");

                EditorGUIUtility.wideMode = true;
                EditorGUIUtility.labelWidth = 240;

                EditorGUI.BeginProperty(position, null, property);

                position.height /= amountOfFields;
                position.y += position.height;
                position.x += 20;

                unfold = EditorGUI.Foldout(position, unfold, (property.objectReferenceValue as Item).name);
                position.y += position.height;
                position.x += 20;

                if (unfold)
                {
                    var width = position.width;
                    position.width = 140;

                    if (GUI.Button(position, _useShorter ? "Edit Values" : "Use Current values"))
                        _useShorter = !_useShorter;

                    position.width = width;
                    position.y += position.height;

                    if (_useShorter == false)
                    {
                        baseAmount = 4;
                        _itemFoldout = EditorGUI.Foldout(position, _itemFoldout, new GUIContent("Item Configuration"),true);
                        position.y += position.height;

                        if (_itemFoldout)
                        {
                            position.x += 20;
                            position.width -= 40;
                            EditorGUIUtility.labelWidth -= 20;
                            total += 4;
                            EditorGUI.indentLevel++;

                            itemName.stringValue =
                                EditorGUI.TextField(position, new GUIContent("Item name"), itemName.stringValue);
                            position.y += position.height;

                            id.intValue = EditorGUI.IntField(position, new GUIContent("Id"), id.intValue);
                            position.y += position.height;

                            description.stringValue = EditorGUI.TextField(position, new GUIContent("Item Description"),
                                description.stringValue);
                            position.y += position.height;

                            EditorGUI.ObjectField(position, icon, new GUIContent("Item sprite"));
                            position.y += position.height;

                            EditorGUI.indentLevel--;
                            position.width += 40;
                            EditorGUIUtility.labelWidth += 20;
                            position.x -= 20;
                        }

                        _storageFoldOut = EditorGUILayout.Foldout(_storageFoldOut, new GUIContent("Storage Configuration"), true, EditorStyles.foldoutHeader);
                        position.y += position.height;

                        if (_storageFoldOut)
                        {
                            position.x += 20;
                            position.width -= 40;
                            EditorGUIUtility.labelWidth -= 20;
                            total += 2;
                            EditorGUI.indentLevel++;


                            isStackable.boolValue =
                                EditorGUILayout.Toggle(new GUIContent("Is Stackable"), isStackable.boolValue);
                            position.y += position.height;

                            if (isStackable.boolValue)
                            {
                                maxStackSize.intValue = EditorGUILayout.IntField(new GUIContent("Max Stack Size"),
                                    maxStackSize.intValue);
                                position.y += position.height;
                            }

                            EditorGUI.indentLevel--;
                            position.width += 40;
                            EditorGUIUtility.labelWidth += 20;
                            position.x -= 20;
                        }

                        _consumeFoldOut = EditorGUILayout.Foldout(_consumeFoldOut, new GUIContent("Consume Configuration"), true, EditorStyles.foldoutHeader);
                        position.y += position.height;

                        if (_consumeFoldOut)
                        {
                            position.x += 20;
                            position.width -= 40;
                            EditorGUIUtility.labelWidth -= 20;
                            total += 2;
                            EditorGUI.indentLevel++;


                            isConsumable.boolValue =
                                EditorGUILayout.Toggle(new GUIContent("Is Consumable"), isConsumable.boolValue);
                            position.y += position.height;

                            if (isConsumable.boolValue)
                            {
                                maxUsesInOnceTime.intValue =
                                    EditorGUILayout.IntField(new GUIContent("Max Use In Once Time"),
                                        maxUsesInOnceTime.intValue);
                                position.y += position.height;
                            }

                            EditorGUI.indentLevel--;
                            position.width += 40;
                            EditorGUIUtility.labelWidth += 20;
                            position.x -= 20;
                        }

                    }
                    else
                    {
                        baseAmount = 3f;
                        position.width -= 20;
                        EditorGUI.ObjectField(position, property);
                    }
                }
                else
                {
                    amountOfFields = 1;
                    baseAmount = 1;
                }

                position.x -= 20;
                serializedObject.ApplyModifiedProperties();
                EditorGUI.EndProperty();
            }
            else
            {
                amountOfFields = 1;
                baseAmount = 1;
                EditorGUI.ObjectField(position, property);
                if (property.objectReferenceValue != null)
                {
                    serializedObject = new SerializedObject(property.objectReferenceValue);
                    serializedObject.ApplyModifiedProperties();
                    _useShorter = true;
                }
            }
        }
    }
}