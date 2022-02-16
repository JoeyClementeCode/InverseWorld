using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InverseWorld
{
    public class SignBehavior : MonoBehaviour
    {
        private bool isShowingText = false;
        [SerializeField] private GameObject textPrefab;
        [SerializeField] private GameObject canvas;

        private GameObject newText;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player") && isShowingText == false)
            {
                if (Switch.IsInverted == false)
                {
                    newText = Instantiate(textPrefab,
                        new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z),
                        Quaternion.identity, canvas.transform);
                    
                }
                else if (Switch.IsInverted)
                {
                    newText = Instantiate(textPrefab,
                        new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z),
                        Quaternion.identity, canvas.transform);
                }

                isShowingText = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player") && isShowingText)
            {
                Destroy(newText);
                isShowingText = false;
            }
        }
    }
}
