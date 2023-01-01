using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class Interactable : MonoBehaviour
    {
        public float radius = 0.6f;
        public string interactableText;
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public virtual void Interact(PlayerManager playerManager)
        {
            // Called when player interacts
            Debug.Log("You interacted with an object");
        }
    }
}
