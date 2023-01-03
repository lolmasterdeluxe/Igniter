using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IG
{
    public class StartDoorCollider : MonoBehaviour
    {
        InputHandler inputHandler;
        UIManager uiManager;
        GameManager gameManager;
        [SerializeField]
        private bool isWithinRange = false;

        private void Awake()
        {
            inputHandler = FindObjectOfType<InputHandler>();
            uiManager = FindObjectOfType<UIManager>();
            gameManager = FindObjectOfType<GameManager>();
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
            {
                isWithinRange = true;
                uiManager.ActivateInteractAlertPopup("Press E to begin a run");
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.tag == "Player")
            {
                isWithinRange = false;
                uiManager.DeactivateInteractAlertPopup();
            }
        }

        void Update()
        {
            if (isWithinRange && inputHandler.interact_input)
            {
                gameManager.StartGame();
                uiManager.DeactivateInteractAlertPopup();
            }
        }
    }
}
