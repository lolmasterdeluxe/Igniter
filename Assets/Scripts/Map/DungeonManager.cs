using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class DungeonManager : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> dungeonLayouts;
        [SerializeField]
        private GameObject dungeonRestRoom;
        [SerializeField]
        private GameObject initialDungeon;
        [SerializeField]
        private int numberOfRooms = 1;
        [SerializeField]
        private Transform dungeonLayoutParent;

        public void CreateDungeonLayout()
        {
            for (int i = 0; i <= numberOfRooms + 1; ++i)
            {
                GameObject currentDungeon;
                // Initialize rest room as final room
                if (i == numberOfRooms + 1)
                    currentDungeon = Instantiate(dungeonRestRoom, dungeonLayoutParent);
                else
                    currentDungeon = Instantiate(dungeonLayouts[Random.Range(0, dungeonLayouts.Count)], dungeonLayoutParent);

                Transform entry = currentDungeon.transform.GetChild(0);
                Transform exit = initialDungeon.transform.GetChild(0).GetChild(0);

                DungeonDoorManager initialDungeonDoorManager = initialDungeon.GetComponent<DungeonDoorManager>();
                DungeonDoorManager currentDungeonDoorManager = currentDungeon.GetComponent<DungeonDoorManager>();
                initialDungeonDoorManager.SetExitDoors(currentDungeonDoorManager.leftEntryDoor, currentDungeonDoorManager.rightEntryDoor, currentDungeonDoorManager.leftOpenDegrees, currentDungeonDoorManager.rightOpenDegrees);

                // Disable position placeholder cubes
                entry.GetComponent<MeshRenderer>().enabled = false;
                exit.GetComponent<MeshRenderer>().enabled = false;

                // Set entry position to exit position
                entry.position = exit.position;
                entry.rotation = exit.rotation;
                initialDungeon = currentDungeon;
            }
        }

        public void ClearDungeonLayout()
        {
            for (int i = 1; i < dungeonLayoutParent.childCount; ++i)
            {
                Destroy(dungeonLayoutParent.GetChild(i).gameObject);
            }
        }
    }
}
