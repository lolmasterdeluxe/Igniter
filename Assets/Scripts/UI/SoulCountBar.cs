using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IG
{
    public class SoulCountBar : MonoBehaviour
    {
        public TextMeshProUGUI soulCountText;

        public void SetSoulCountText(int soulCountNumber)
        {
            soulCountText.text = soulCountNumber.ToString();
        }
    }
}
