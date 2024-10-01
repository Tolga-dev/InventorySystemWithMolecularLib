using System;
using MolecularLib.Helpers;
using UnityEngine;

namespace MolecularLibTests.Scripts.HelpersTest
{
    public class VectorHelper : MonoBehaviour
    {
        public Vector2 myVector2;   
        public Vector3 myVector3;
        public Vector4 myVector4;
        public Vector3 minVec3;
        public Vector3 maxVec3;
        public Vector2 minVec2;
        public Vector2 maxVec2;
        public Vector3Int myVec3Int;
        
        private void Start()
        {
            myVector2.WithX(5);
            myVector3.WithX(x => x + 1);
            myVector4.WithoutW();
            myVector3.IsBetween(minVec3, maxVec3); // min and max not inclusive
            myVector2.IsWithin(minVec2, maxVec2); // min and max inclusive
            myVec3Int.ToVec2();
        }
        
        
    }
}