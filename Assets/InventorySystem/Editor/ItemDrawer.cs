using UnityEditor;
using UnityEngine.UIElements;

namespace InventorySystem.Editor
{
    [CustomEditor(typeof(TYPE))]
    public class ItemDrawer : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            
            return base.CreateInspectorGUI();
        }
    }
}