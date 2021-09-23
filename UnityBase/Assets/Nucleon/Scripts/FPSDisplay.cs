using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FPCounter))]
public class FPSDisplay : MonoBehaviour
{
        public Text fpsLabel;
        private FPCounter fpsCounter;
        private void Awake()
        {
                fpsCounter = GetComponent<FPCounter>();
        }

        private void Update()
        {
                fpsLabel.text = fpsCounter.FPS.ToString();
        }
}