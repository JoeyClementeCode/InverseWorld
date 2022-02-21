using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InverseWorld
{
    public class MainMenuInverse : MonoBehaviour
    {
        private bool IsInverted = false;

        [SerializeField] private GameObject canvas1;
        [SerializeField] private GameObject canvas2;
        
        void Update()
        {
            Invert();
        }
        
        public void Invert()
        {
            if (Input.GetKeyDown(KeyCode.Space) && IsInverted == false)
            {
                if (Camera.main != null) 
                    Camera.main.backgroundColor = Color.white;
                canvas1.SetActive(false);
                canvas2.SetActive(true);
                IsInverted = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && IsInverted == true)
            {
                if (Camera.main != null) 
                    Camera.main.backgroundColor = Color.black;
                canvas1.SetActive(true);
                canvas2.SetActive(false);
                IsInverted = false;
            }
        }
    }
}
