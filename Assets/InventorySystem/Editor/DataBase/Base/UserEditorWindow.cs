using InventorySystem.Scripts;
using UnityEditor;
using UnityEngine;

namespace InventorySystem.Editor
{
    public class UserEditorWindow : EditorWindow
    {
        public SerializedObject serializedObject;
        public SerializedProperty currentProperty;
        
        public SerializedProperty selectedProperty;
        public bool showItemAssets;

        public string selectedPropertyPath;
    }
}