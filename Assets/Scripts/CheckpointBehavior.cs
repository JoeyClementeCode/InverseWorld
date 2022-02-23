using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InverseWorld
{
    public class CheckpointBehavior : MonoBehaviour
    {
        public static int currentInversionNumber;
        private CheckpointRotation cr;
        private bool hitCheckpoint = false;
        
        

        private void Start()
        {
            cr = GetComponent<CheckpointRotation>();
            currentInversionNumber = 3;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (hitCheckpoint == false)
            {
                if (col.CompareTag("Player"))
                {
                    currentInversionNumber = Switch.InversionLimit;
                    DeathTrigger.currentSpawnPos = this.gameObject;
                    StartCoroutine(cr.fastRotate());
                    hitCheckpoint = true;
                }
            }

        }
    }
}
