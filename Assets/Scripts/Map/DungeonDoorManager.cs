using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class DungeonDoorManager : MonoBehaviour
    {
        [Header("Door Transforms")]
        public Transform leftEntryDoor;
        public Transform rightEntryDoor;
        public Transform leftExitDoor;
        public Transform rightExitDoor;
        [SerializeField]
        private Transform enemyParent;

        [Header("Door Animation Properties")]
        public float leftOpenDegrees;
        public float leftCloseDegrees;
        public float rightOpenDegrees;
        public float rightCloseDegrees;

        public float rotationSpeed = 10;
        public bool doorEntryTrigger = false;
        public bool doorExitTrigger = false;
        public bool roomClearEventActive = false;
        public bool isGameEnd = false;
        GameManager gameManager;

        private float leftExitTargetDegrees;
        private float rightExitTargetDegrees;

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
            if (doorEntryTrigger)
            {
                Quaternion trl = Quaternion.Euler(leftEntryDoor.localRotation.x, leftCloseDegrees, leftEntryDoor.localRotation.z);
                Quaternion targetRotationLeft = Quaternion.Lerp(leftEntryDoor.localRotation, trl, rotationSpeed * Time.deltaTime);
                leftEntryDoor.localRotation = targetRotationLeft;

                Quaternion trR = Quaternion.Euler(rightEntryDoor.localRotation.x, rightCloseDegrees, rightEntryDoor.localRotation.z);
                Quaternion targetRotationRight = Quaternion.Lerp(leftEntryDoor.localRotation, trR, rotationSpeed * Time.deltaTime);
                rightEntryDoor.localRotation = targetRotationRight;

                if (isGameEnd)
                    gameManager.ActivateEndScreen();
            }

            if (doorExitTrigger)
            {
                Quaternion trl = Quaternion.Euler(leftExitDoor.localRotation.x, leftExitTargetDegrees, leftExitDoor.localRotation.z);
                Quaternion targetRotationLeft = Quaternion.Lerp(leftExitDoor.localRotation, trl, rotationSpeed * Time.deltaTime);
                leftExitDoor.localRotation = targetRotationLeft;

                Quaternion trR = Quaternion.Euler(rightExitDoor.localRotation.x, rightExitTargetDegrees, rightExitDoor.localRotation.z);
                Quaternion targetRotationRight = Quaternion.Lerp(rightExitDoor.localRotation, trR, rotationSpeed * Time.deltaTime);
                rightExitDoor.localRotation = targetRotationRight;

                if (leftExitDoor.localRotation == trl || rightExitDoor.localRotation == trR)
                    doorExitTrigger = false;
            }
        }

        public void RoomClearEvent()
        {
            if (enemyParent.childCount <= 0 && !roomClearEventActive)
            {
                doorExitTrigger = true;
                roomClearEventActive = true;
            }
        }

        public void SetExitDoors(Transform leftDoor, Transform rightDoor, float leftTargetDegrees, float rightTargetDegrees)
        {
            leftExitDoor = leftDoor;
            rightExitDoor = rightDoor;
            leftExitTargetDegrees = leftTargetDegrees;
            rightExitTargetDegrees = rightTargetDegrees;
        }
    }
}
