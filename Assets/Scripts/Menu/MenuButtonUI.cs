using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IG
{
    public class MenuButtonUI : MonoBehaviour
    {
        [SerializeField]
        private List<Button> menuButton;
        [SerializeField]
        private List<Image> menuButtonLines;

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < menuButton.Count; ++i)
            {
                menuButtonLines[i].color = menuButton[i].targetGraphic.canvasRenderer.GetColor();
            }
        }
    }
}
