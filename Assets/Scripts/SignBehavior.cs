using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InverseWorld
{
    public class SignBehavior : MonoBehaviour
    {
        [SerializeField] private GameObject textPrefab;
        [SerializeField] private GameObject canvas;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                Instantiate(textPrefab,
                    new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z),
                    Quaternion.identity, canvas.transform);
            }
        }
    }
}
