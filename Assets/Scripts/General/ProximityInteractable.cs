using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class ProximityInteractable : MonoBehaviour
    {
        public InputHandler inputHandler;
        public UIManager uiManager;
        [SerializeField]
        private bool isWithinRange = false;
        public string interactableText;

        protected void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
            {
                isWithinRange = true;
                OnEnter();
                uiManager.ActivateInteractAlertPopup(interactableText);
            }
        }

        protected void OnTriggerExit(Collider collision)
        {
            if (collision.tag == "Player")
            {
                isWithinRange = false;
                OnExit();
                uiManager.DeactivateInteractAlertPopup();
            }
        }

        public virtual void OnEnter()
        {
            Debug.Log("Entered collider");
        }

        public virtual void OnExit()
        {
            Debug.Log("Exitted collider");
        }

        public virtual void Interact()
        {
            Debug.Log("Interacted");
        }

        void Update()
        {
            if (isWithinRange && inputHandler.interact_input)
            {
                Interact();
            }
        }
    }
}
