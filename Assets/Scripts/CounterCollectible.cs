using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InverseWorld
{
    public class CounterCollectible : MonoBehaviour
    {
        public static bool isCollected = false;
        [SerializeField] private GameObject sprite;

        private void Update()
        {
            if (isCollected == false)
            {
                sprite.SetActive(true);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                if (isCollected == false)
                {
                    Switch.InversionLimit++;
                    sprite.SetActive(false);
                    isCollected = true;
                }

            }
        }
        
        
        
    }
}
