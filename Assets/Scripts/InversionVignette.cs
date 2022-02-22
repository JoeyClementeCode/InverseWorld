using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


namespace InverseWorld
{
    public class InversionVignette : MonoBehaviour
    {
        public PostProcessVolume volume; //Assigned with the editor
        Vignette vignette;

        private GameObject player;
        [SerializeField] private GameObject cam;

        private void Start()
        {
            player = GameObject.Find("Player");
            volume.profile.TryGetSettings(out vignette);
            vignette.intensity.value = 0f;
        }

        public void StartVignette()
        {
            vignette.intensity.value += .001f;
        }
        
        public void EndVignette()
        {
            vignette.intensity.value = 0f;
        }
    }
}
