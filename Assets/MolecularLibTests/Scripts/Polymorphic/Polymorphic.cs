using System;
using MolecularLib.PolymorphismSupport;
using MolecularLibTests.Scripts.Polymorphic.Poly;
using UnityEngine;

namespace MolecularLibTests.Scripts.Polymorphic
{
    public class Polymorphic : MonoBehaviour
    {
        public PolymorphicVariable<Animal> animalVariable;

        private void Update()
        {
            animalVariable.Value.MakeSound();
        }
    }
    
    
}