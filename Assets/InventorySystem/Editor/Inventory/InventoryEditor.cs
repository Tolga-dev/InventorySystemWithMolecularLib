using InventorySystem.Core;
using InventorySystem.Scripts;
using InventorySystem.Scripts.Inventories;
using UnityEditor;
using UnityEngine;

namespace InventorySystem.Editor.Inventory
{
    [CustomEditor(typeof(InventorySave), true)]
    public class InventoryEditor : EditorCore
    {
        public SerializedProperty slots;

        private void OnEnable()
        {
            slots = serializedObject.FindProperty("slots");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Inventory Editor", EditorStyles.boldLabel);
            Separator();

            slots.isExpanded = EditorGUILayout.Foldout(slots.isExpanded, new GUIContent("Item List"), true);

            if (slots.isExpanded)
            {
                IncreaseIndent();

                int newSize = EditorGUILayout.IntField(new GUIContent("Size"), slots.arraySize);
                if (newSize != slots.arraySize)
                {
                    slots.arraySize = Mathf.Max(newSize, 0); // Ensure size is non-negative
                }

                for (int i = 0; i < slots.arraySize; i++)
                {
                    var slot = slots.GetArrayElementAtIndex(i);
                    var itemInSlot = slot.FindPropertyRelative("itemInSlot");
                    var amount = slot.FindPropertyRelative("amount");

                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.ObjectField(itemInSlot, new GUIContent($"Item {i}"));

                    amount.intValue = EditorGUILayout.IntField(amount.intValue);

                    if (GUILayout.Button("Remove", GUILayout.Width(60)))
                    {
                        itemInSlot.objectReferenceValue = null; // Set item to null
                        amount.intValue = 0; // Set amount to 0
                    }

                    EditorGUILayout.EndHorizontal();
                }

                DecreaseIndent();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}