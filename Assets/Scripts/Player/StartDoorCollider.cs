using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IG
{
    public class StartDoorCollider : ProximityInteractable
    {
        GameManager gameManager;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        public override void Interact()
        {
            base.Interact();
            gameManager.StartGame();
            uiManager.DeactivateInteractAlertPopup();
        }
    }
}
