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
        private void Awake()
        {
            inputHandler = FindObjectOfType<InputHandler>();
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
            {
                isWithinRange = true;
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.tag == "Player")
            {
                isWithinRange = false;
                npcManager.textBox.SetActive(false);
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
