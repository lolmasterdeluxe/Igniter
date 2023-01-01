using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class NPCAnimatorManager : AnimatorManager
    {
        public bool randomizeAnimation;
        public List<string> talkAnimations;
        NPCManager npcManager;
        // Start is called before the first frame update
        void Awake()
        {
            anim = GetComponent<Animator>();
            npcManager = GetComponentInParent<NPCManager>();
        }

        private void OnAnimatorMove()
        {
            float delta = Time.deltaTime;
            npcManager.npcRigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            npcManager.npcRigidbody.velocity = velocity;
        }
    }
}
