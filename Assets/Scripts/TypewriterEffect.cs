using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace InverseWorld
{
    public class TypewriterEffect : MonoBehaviour {

        public float delay = 0.1f;
        private string fullText;
        private string currentText = "";

        // Use this for initialization
        void Start ()
        {
            fullText = GetComponent<TextMeshProUGUI>().text;
            StartCoroutine(ShowText());
        }
	
        IEnumerator ShowText()
        {
            for(int i = 0; i <= fullText.Length; i++){
                currentText = fullText.Substring(0,i);
                this.GetComponent<TextMeshProUGUI>().text = currentText;
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
