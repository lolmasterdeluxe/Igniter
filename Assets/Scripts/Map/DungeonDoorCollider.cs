using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    [RequireComponent(typeof(DungeonDoorManager))]
    public class DungeonDoorCollider : MonoBehaviour
    {
        private DungeonDoorManager dungeonDoorManager;

        private void Awake()
        {
            dungeonDoorManager = GetComponent<DungeonDoorManager>();
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
            {
                dungeonDoorManager.doorTrigger = true;
            }
        }
    }
}
