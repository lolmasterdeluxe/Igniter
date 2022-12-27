using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IG
{
    public class FocusPointBar : MonoBehaviour
    {
        public Slider slider;

        private void Start()
        {
            slider = GetComponent<Slider>();
        }
        public void SetMaxFocusPoints(float maxfocusPoints)
        {
            slider.maxValue = maxfocusPoints;
            slider.value = maxfocusPoints;
        }

        public void SetCurrentFocusPoints(float currentFocusPoints)
        {
            slider.value = currentFocusPoints;
        }
    }
}
