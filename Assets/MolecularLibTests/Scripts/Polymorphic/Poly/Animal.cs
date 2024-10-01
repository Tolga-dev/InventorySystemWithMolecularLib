using System;
using UnityEngine;

namespace MolecularLibTests.Scripts.Polymorphic.Poly
{
    [Serializable]
    public class Animal
    {
        public virtual void MakeSound()
        {
            Debug.Log("Default sound");
        }
    }
}