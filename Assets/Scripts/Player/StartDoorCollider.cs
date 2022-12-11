using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IG
{
    public class StartDoorCollider : MonoBehaviour
    {
        public InputHandler inputHandler;
        public UIManager uiManager;
        public GameManager gameManager;
        [SerializeField]
        private bool isWithinRange = false;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
            {
                uiManager.interactWindow.SetActive(true);
                uiManager.interactWindowText.text = "Press E to begin a run";
                isWithinRange = true;
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.tag == "Player")
            {
                uiManager.interactWindow.SetActive(false);
                isWithinRange = false;
            }
        }

        void Update()
        {
            if (isWithinRange && inputHandler.interact_input)
            {
                gameManager.StartGame();
                uiManager.interactWindow.SetActive(false);
            }
        }
    }
}
