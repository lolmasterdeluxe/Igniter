using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class NPCInteractCollider : ProximityInteractable
    { 
        public NPCManager npcManager;

        public override void OnExit()
        {
            base.OnExit();
            npcManager.textBox.SetActive(false);
        }

        public override void Interact()
        {
            base.Interact();
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
