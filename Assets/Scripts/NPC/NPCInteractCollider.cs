using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class NPCInteractCollider : MonoBehaviour
    {
        [SerializeField]
        private bool isWithinRange = false;
        public NPCManager npcManager;
        InputHandler inputHandler;
        UIManager uiManager;
        private void Awake()
        {
            inputHandler = FindObjectOfType<InputHandler>();
            uiManager = FindObjectOfType<UIManager>();
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
            {
                isWithinRange = true;
                uiManager.ActivateInteractAlertPopup("Talk to " + npcManager.characterName);
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.tag == "Player")
            {
                isWithinRange = false;
                npcManager.textBox.SetActive(false);
                uiManager.DeactivateInteractAlertPopup();
            }
        }

        void Update()
        {
            if (inputHandler.interact_input && isWithinRange)
            {
                if (!npcManager.textBox.activeInHierarchy)
                {
                    npcManager.StartDialogue();
                }
                else
                {
                    npcManager.ContinueDialogue();
                }
            }
        }
    }
}
