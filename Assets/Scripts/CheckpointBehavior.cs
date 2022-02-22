using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InverseWorld
{
    public class CheckpointBehavior : MonoBehaviour
    {
        public static int currentInversionNumber;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                currentInversionNumber = Switch.InversionLimit;
                DeathTrigger.currentSpawnPos = this.gameObject;
            }
        }
    }
}
