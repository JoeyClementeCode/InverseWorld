using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InverseWorld
{
    public class DeathTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject startPos;
        
        private Switch sw;

        private void Start()
        {
            sw = GameObject.Find("SwitchManager").GetComponent<Switch>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            if (other.CompareTag("Player"))
            {
                other.gameObject.transform.position = startPos.transform.position;
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.zero;
                sw.Revert();
            }
        }
    }
}
