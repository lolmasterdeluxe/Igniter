using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IG
{
    public class ButtonLineColor : MonoBehaviour
    {
        [SerializeField]
        private Button menuButton;
        [SerializeField]
        private Image menuButtonLines;

        // Update is called once per frame
        void Update()
        {
            menuButtonLines.color = menuButton.targetGraphic.canvasRenderer.GetColor();
        }
    }
}
