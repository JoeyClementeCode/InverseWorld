using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InverseWorld
{
    public class CheckpointRotation : MonoBehaviour
    {
        public float rotateSpeed = -0.05f;
        
        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, 0, rotateSpeed);
        }

        public IEnumerator fastRotate()
        {
            rotateSpeed = 6;
            yield return new WaitForSeconds(.7f);
            rotateSpeed = -0.05f;
        }
        
    }
}
