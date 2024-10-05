using UnityEditor;
using UnityEngine;

namespace InventorySystem.Core
{
    public static class EditorCore
    {
        public static void Separator()
        {
            EditorGUILayout.Separator();
        }

        public static void AddTab()
        {
            EditorGUI.indentLevel++;
        }
        public static void BackTab()
        {
            EditorGUI.indentLevel--;
        }
    }
}