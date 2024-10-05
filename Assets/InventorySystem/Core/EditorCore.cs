using UnityEditor;
using UnityEngine;

namespace InventorySystem.Core
{
    public class EditorCore : Editor
    {
        public void IncreaseIndent() => EditorGUI.indentLevel++;
        public void DecreaseIndent() => EditorGUI.indentLevel--;

        public void Separator() => EditorGUILayout.Separator();
    }
}