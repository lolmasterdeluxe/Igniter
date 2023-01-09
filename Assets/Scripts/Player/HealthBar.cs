using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IG
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        RectTransform healthBar;

        private void Start()
        {
            slider = GetComponent<Slider>();
            healthBar = GetComponent<RectTransform>();
        }
        public void SetMaxHealth(int maxHealth)
        {
            // Width 150 = Max Health Lvl 1 = 10
            // Width 160 = Max Health Lvl 2 = 20
            healthBar.sizeDelta = new Vector2(140 + maxHealth, 13);
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        public void SetCurrentHealth(int currentHealth)
        {
            slider.value = currentHealth;
        }


    }
}
