using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class DungeonDoorManager : MonoBehaviour
    {
        [SerializeField]
        private Transform door;
        [SerializeField]
        private Transform enemyParent;
        [Header("Door Animation Properties")]
        public float targetDegrees;
        public float rotationSpeed = 10;
        public bool doorTrigger = false;
        public bool isExit = false;
        public bool isGameEnd = false;
        GameManager gameManager;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            DoorInteraction();
            RoomClearEvent();
        }

        public void DoorInteraction()
        {
            if (doorTrigger)
            {
                Quaternion tr = Quaternion.Euler(door.localRotation.x, targetDegrees, door.localRotation.z);
                Quaternion targetRotation = Quaternion.Lerp(door.localRotation, tr, rotationSpeed * Time.deltaTime);
                door.localRotation = targetRotation;

                if (isGameEnd)
                    gameManager.ActivateEndScreen();
            }
        }

        public void RoomClearEvent()
        {
            if (isExit)
            {
                if (enemyParent.childCount <= 0)
                    doorTrigger = true;
            }
        }
    }
}
