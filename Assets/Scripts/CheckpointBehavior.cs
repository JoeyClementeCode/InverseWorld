using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InverseWorld
{
    public class CheckpointBehavior : MonoBehaviour
    {
        private CheckpointRotation cr;
        private bool hitCheckpoint = false;
        
        private void Start()
        {
            cr = GetComponent<CheckpointRotation>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (hitCheckpoint == false)
            {
                if (col.CompareTag("Player"))
                {
                    DeathTrigger.currentInversionNumber = Switch.InversionLimit;
                    DeathTrigger.currentSpawnPos = this.gameObject;
                    StartCoroutine(cr.fastRotate());
                    hitCheckpoint = true;
                }
            }

        }
    }
}
