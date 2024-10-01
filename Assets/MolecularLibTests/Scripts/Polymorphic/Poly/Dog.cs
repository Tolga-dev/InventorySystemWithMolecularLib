using System;
using UnityEngine;

namespace MolecularLibTests.Scripts.Polymorphic.Poly
{
    [Serializable]
    public class Dog : Animal
    {
        public override void MakeSound()
        {
            Debug.Log("Woof!");
        }
    }
}