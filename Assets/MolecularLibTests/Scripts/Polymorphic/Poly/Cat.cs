using System;
using UnityEngine;

namespace MolecularLibTests.Scripts.Polymorphic.Poly
{
    [Serializable]
    public class Cat : Animal
    {
        public override void MakeSound()
        {
            Debug.Log("Meow!");
        }
    }
     
}