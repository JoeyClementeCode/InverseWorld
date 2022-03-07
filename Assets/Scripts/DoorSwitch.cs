using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InverseWorld
{
    public class DoorSwitch : MonoBehaviour
    {
        public static bool isOpened = false;
        
        [SerializeField] private GameObject platform;
        private GameObject platform2; // for later

        private void Update()
        {
            if (isOpened == false)
            {
                platform.SetActive(true);
            }
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                if (isOpened == false)
                {
                    platform.SetActive(false);
                    isOpened = true;
                }
            }
        }
    }
}
