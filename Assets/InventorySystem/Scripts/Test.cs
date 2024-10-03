using System;
using UnityEngine;

namespace InventorySystem.Scripts
{
    public class Test : MonoBehaviour
    {
        public Item item;

        public void Start()
        {
            if (item.icon == null)
            {
                Debug.Log("Null");
                return;
            }
            Debug.Log("No Null");
        }
    }
}
