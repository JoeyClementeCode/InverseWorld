using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InverseWorld
{
    public class Switch : MonoBehaviour
    {
        [Header("Object References")]
        public GameObject environment1;
        public GameObject environment2;
        public GameObject player;

        [SerializeField] private Sprite regularSprite;
        [SerializeField] private Sprite invertedSprite;
        [SerializeField] private InversionVignette iv;
        
        
        // Extra Variables
        public static bool IsInverted = false;
        public static int InversionLimit = 3;

        public float inversionTime = 10f;
        private bool timerOn = false;
        

        void Update()
        {
            Switcher();

            if (timerOn)
            {
                Timer();
            }
        }

        public void Switcher()
        {
            if (InversionLimit > 0)
            {
                if (Input.GetKeyDown(KeyCode.Q) && IsInverted == false)
                {
                    Invert();
                }
                else if (Input.GetKeyDown(KeyCode.Q) && IsInverted == true)
                {
                    Revert();
                    InversionLimit--;
                }
            }
        }

        private void Timer()
        {
            inversionTime -= Time.deltaTime;
                
            if (inversionTime < 0)
            {
                Destroy(player);
                timerOn = false;
            }

            if (inversionTime < 7.5f)
            {
                iv.StartVignette();
            }
        }

        public void Invert()
        {
            if (Camera.main != null) 
                Camera.main.backgroundColor = Color.white;
            player.GetComponent<SpriteRenderer>().sprite = invertedSprite;
            player.GetComponent<Rigidbody2D>().gravityScale = -8f;
            player.transform.localScale = new Vector3(1, -1, 1);
            environment1.SetActive(false);
            environment2.SetActive(true);
            IsInverted = true;
            timerOn = true;
        }

        public void Revert()
        {
            if (Camera.main != null) 
                Camera.main.backgroundColor = Color.black;
            player.GetComponent<SpriteRenderer>().sprite = regularSprite;
            player.GetComponent<Rigidbody2D>().gravityScale = 8f;
            player.transform.localScale = new Vector3(1, 1, 1);
            environment1.SetActive(true);
            environment2.SetActive(false);
            IsInverted = false;
            timerOn = false;
            inversionTime = 10f;
            iv.EndVignette();
        }
        
    }   
}

