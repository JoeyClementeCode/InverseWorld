using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InverseWorld
{
    public class CounterCollectible : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                Switch.InversionLimit++;
                gameObject.SetActive(false);
            }
        }
        
        
        
    }
}
