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

        private void Start()
        {
            volume.profile.TryGetSettings(out vignette);
            vignette.intensity.value = 0f;
        }

        public void StartVignette()
        {
            vignette.intensity.value = 1f;
        }
        
        public void EndVignette()
        {
            vignette.intensity.value = 0f;
        }
    }
}
