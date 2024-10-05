using InventorySystem.Scripts;
using UnityEditor;
using UnityEngine;

namespace InventorySystem.Editor
{
    [CustomPropertyDrawer(typeof(Recipe))]
    public class RecipeDrawer : PropertyDrawer
    {
        public bool showRecipe = true;
        public int baseAmount = 1;
        public int amount = 1;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 18f * amount;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUIUtility.wideMode = true;
            position.height /= amount;
            EditorGUI.PropertyField(position, property);

            var buttonRect = new Rect(position.x, position.y, 165, position.height);
            if (property.objectReferenceValue != null)
            {
                buttonRect.x += 165;
                buttonRect.width = 85;
                if (GUI.Button(buttonRect, new GUIContent("Show Recipe")))
                {
                    showRecipe = !showRecipe;
                    Debug.Log($"Show Recipe {showRecipe}");
                }

                SerializedObject serializedObject = null;

                if (property.objectReferenceValue != null)
                    serializedObject = new SerializedObject(property.objectReferenceValue);

                if (serializedObject != null)
                {
                    position.y += position.height;

                    if (showRecipe)
                    {
                        amount = 3;
                        var pattern = serializedObject.FindProperty("enteredItems");
                        var product = serializedObject.FindProperty("producedItems");

                        var factorsAmount = serializedObject.FindProperty("amountOfEnteredItems");
                        var productsAmount = serializedObject.FindProperty("amountOfProducedItems");

                        var patternPos = new Rect(position.x, position.y, position.width, position.height + 10);

                        var content = new GUIContent();

                        for (int i = 0; i < pattern.arraySize; i++)
                        {
                            var item = pattern.GetArrayElementAtIndex(i).objectReferenceValue as Item;
                            var fAmount = factorsAmount.GetArrayElementAtIndex(i).intValue;

                            if (item == null)
                            {
                                content.text = "null";
                                content.image = null;
                            }
                            else
                            {
                                content.text = "";
                                content.image = item.icon.texture;
                            }

                            EditorGUI.LabelField(patternPos, content);
                            var secondContent = new GUIContent
                            {
                                text = fAmount == 0 ? "" : fAmount.ToString()
                            };

                            EditorGUI.LabelField(patternPos, secondContent);
                            patternPos.x += 36;

                            if (i < pattern.arraySize - 1)
                            {
                                EditorGUI.LabelField(patternPos, "+");

                                patternPos.x += 16;
                            }
                            else
                            {
                                EditorGUI.LabelField(patternPos, "=");
                                patternPos.x += 15;
                                for (int j = 0; j < product.arraySize; j++)
                                {
                                    var productItem = product.GetArrayElementAtIndex(j).objectReferenceValue as Item;
                                    var pAmount = productsAmount.GetArrayElementAtIndex(j).intValue;
                                    if (productItem == null)
                                    {
                                        content.text = "null";
                                        content.image = null;
                                    }
                                    else
                                    {
                                        content.text = "";
                                        content.image = productItem.icon.texture;
                                    }

                                    EditorGUI.LabelField(patternPos, content);
                                    patternPos.x -= 5;
                                    GUIContent prodContent = new GUIContent
                                    {
                                        text = pAmount == 0 ? "" : pAmount.ToString()
                                    };
                                    EditorGUI.LabelField(patternPos, prodContent);
                                    patternPos.x += 36;
                                }

                                patternPos.x += 36;
                            }
                        }
                    }
                    else
                    {
                        amount = baseAmount;
                    }
                }
            }
        }
    }
}