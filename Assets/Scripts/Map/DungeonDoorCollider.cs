using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class DungeonDoorCollider : MonoBehaviour
    {
        [SerializeField]
        private DungeonDoorManager dungeonDoorManager;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
            {
                dungeonDoorManager.doorEntryTrigger = true;
                Destroy(this);
            }
        }
    }
}
