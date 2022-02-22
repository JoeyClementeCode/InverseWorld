using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InverseWorld
{
    public class DeathTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject startPos;
        public static GameObject currentSpawnPos;
        
        private Switch sw;

        private void Start()
        {
            sw = GameObject.Find("SwitchManager").GetComponent<Switch>();
            currentSpawnPos = startPos;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            if (other.CompareTag("Player"))
            {
                other.gameObject.transform.position = currentSpawnPos.transform.position;
                var rb = other.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.zero;
                sw.Revert();
                Switch.InversionLimit = CheckpointBehavior.currentInversionNumber;
            }
        }
    }
}
