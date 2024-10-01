using System;
using MolecularLib.PolymorphismSupport;
using MolecularLibTests.Scripts.Polymorphic.Poly;
using UnityEngine;

namespace MolecularLibTests.Scripts.Polymorphic
{
    public class Polymorphic : MonoBehaviour
    {
        public PolymorphicVariable<Animal> animalVariable;

        public bool debugger = false;
        private void Update()
        {
            if (debugger) return;
            
            animalVariable.Value.MakeSound();
            Debug.Log(animalVariable.ValueType);
            Debug.Log(animalVariable.SelectedPolymorphicType);
            
            if(animalVariable.As<Dog>(out var createdDog))
                createdDog.MakeSound();

        }
    }
    
    
}