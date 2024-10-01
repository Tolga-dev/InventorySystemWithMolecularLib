using System;
using System.Collections.Generic;
using MolecularLib;
using MolecularLib.Helpers;
using Unity.VisualScripting;
using UnityEngine;

namespace MolecularLibTests.Scripts.VolatileScriptableObjectTest
{
    [CreateAssetMenu(fileName = "VolatileScriptableObjectTest", menuName = "VolatileScriptableObjectTest", order = 0)]
    public class VolatileScriptableObjectTest : VolatileScriptableObject<VolatileScriptableObjectTest.Data>
    {
        public Data VolatileData
        {
            get => Value;
            set => Value = value;
        }
    
        [Serializable]
        public class Data
        { 
            [TextArea] public string myString;
            public MonoBehaviour myBehaviour;
            public int myInt;
            public float myFloat;
            public List<string> myList;
            public Optional<SerializableDictionary<int, string>> myOptionalDictionary;
            public ScriptableObject myScriptableObject;
        } 
    }
}