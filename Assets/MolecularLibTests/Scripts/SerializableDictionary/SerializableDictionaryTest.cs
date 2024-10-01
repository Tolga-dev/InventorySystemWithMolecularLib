using System;
using MolecularLib;
using UnityEngine;

namespace MolecularLibTests.Scripts.SerializableDictionary
{
    public class SerializableDictionaryTest : MonoBehaviour
    {
        public SerializableDictionary<string, GameObject> objectIds = new SerializableDictionary<string, GameObject>();

        public void Start()
        {
            foreach (var objectId in objectIds)
            {
                Debug.Log(objectId.Key + " : " + objectId.Value.name);  
            }
        }
    }
}