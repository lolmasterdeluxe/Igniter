using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IG
{
    public class StaminaBar : MonoBehaviour
    {
        public Slider slider;
        RectTransform staminaBar;

        private void Start()
        {
            slider = GetComponent<Slider>();
            staminaBar = GetComponent<RectTransform>();
        }
        public void SetMaxStamina(float maxStamina)
        {
            staminaBar.sizeDelta = new Vector2(140 + maxStamina, 13);
            slider.maxValue = maxStamina;
            slider.value = maxStamina;
        }

        public void SetCurrentStamina(float currentStamina)
        {
            slider.value = currentStamina;
        }
    }
}
