using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace InverseWorld
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI invertCount;

        private void Update()
        {
            UpdateUI();
        }

        private void SetInversionCount()
        {
            invertCount.text = Switch.InversionLimit.ToString();
        }

        private void UpdateUI()
        {
            SetInversionCount();
        }
    }
}
