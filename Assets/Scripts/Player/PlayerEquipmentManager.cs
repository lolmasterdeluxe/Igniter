using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class PlayerEquipmentManager : MonoBehaviour
    {
        InputHandler inputHandler;
        PlayerInventory playerInventory;

        [Header("Equipment Model Changers")]
        // Head Equipment
        HelmetModelChanger helmetModelChanger;
        // Torso Equipment
        TorsoModelChanger torsoModelChanger;
        LeftUpperArmModelChanger leftUpperArmModelChanger;
        RightUpperArmModelChanger rightUpperArmModelChanger;
        // Left Equipment
        HipModelChanger hipModelChanger;
        LeftLegModelChanger leftLegModelChanger;
        RightLegModelChanger rightLegModelChanger;
        // Hand Equipment
        LeftHandModelChanger leftHandModelChanger;
        RightHandModelChanger rightHandModelChanger;

        [Header("Default Models")]
        public string defaultHeadModel;
        public string defaultTorsoModel;
        public string defaultHipModel;
        public string defaultLeftLegModel;
        public string defaultRightLegModel;
        public string defaultLeftUpperArmModel;
        public string defaultRightUpperArmModel;
        public string defaultLeftHandModel;
        public string defaultRightHandModel;
        //defaultLegModel;
        //defaultHandModel;

        [SerializeField]
        BlockingCollider blockingCollider;

        private void Awake()
        {
            inputHandler = GetComponentInParent<InputHandler>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            helmetModelChanger = GetComponentInChildren<HelmetModelChanger>();
            torsoModelChanger = GetComponentInChildren<TorsoModelChanger>();
            hipModelChanger = GetComponentInChildren<HipModelChanger>();
            leftLegModelChanger = GetComponentInChildren<LeftLegModelChanger>();
            rightLegModelChanger = GetComponentInChildren<RightLegModelChanger>();
            leftUpperArmModelChanger = GetComponentInChildren<LeftUpperArmModelChanger>();
            rightUpperArmModelChanger = GetComponentInChildren<RightUpperArmModelChanger>();
            leftHandModelChanger = GetComponentInChildren<LeftHandModelChanger>();
            rightHandModelChanger = GetComponentInChildren<RightHandModelChanger>();
        }

        private void Start()
        {
            EquipAllEquipmentModelsOnStart();
        }

        private void EquipAllEquipmentModelsOnStart()
        {
            // HELMET EQUIPMENT
            helmetModelChanger.UnEquipHelmetModels();

            if (playerInventory.currentHelmetEquipment != null)
                helmetModelChanger.EquipHelmetModelByName(playerInventory.currentHelmetEquipment.helmetModelName);
            else
                helmetModelChanger.EquipHelmetModelByName(defaultHeadModel);

            // TORSO EQUIPMENT
            torsoModelChanger.UnEquipTorsoModels();
            leftUpperArmModelChanger.UnEquipLeftUpperArmModels();
            rightUpperArmModelChanger.UnEquipRightUpperArmModels();

            if (playerInventory.currentTorsoEquipment != null)
            {
                torsoModelChanger.EquipTorsoModelByName(playerInventory.currentTorsoEquipment.torsoModelName);
                leftUpperArmModelChanger.EquipLeftUpperArmModelByName(playerInventory.currentTorsoEquipment.upperLeftArmModelName);
                rightUpperArmModelChanger.EquipRightUpperArmModelByName(playerInventory.currentTorsoEquipment.upperRightArmModelName);
            }
            else
            {
                helmetModelChanger.EquipHelmetModelByName(defaultTorsoModel);
                leftUpperArmModelChanger.EquipLeftUpperArmModelByName(defaultLeftUpperArmModel);
                rightUpperArmModelChanger.EquipRightUpperArmModelByName(defaultRightUpperArmModel);
            }

            // LEG EQUIPMENT
            hipModelChanger.UnEquipHipModels();
            leftLegModelChanger.UnEquipLeftLegModels();
            rightLegModelChanger.UnEquipRightLegModels();

            if (playerInventory.currentLegEquipment != null)
            {
                hipModelChanger.EquipHipModelByName(playerInventory.currentLegEquipment.hipModelName);
                leftLegModelChanger.EquipLeftLegModelByName(playerInventory.currentLegEquipment.leftLegName);
                rightLegModelChanger.EquipRightLegModelByName(playerInventory.currentLegEquipment.rightLegName);
            }
            else
            {
                hipModelChanger.EquipHipModelByName(defaultHipModel);
                leftLegModelChanger.EquipLeftLegModelByName(defaultLeftLegModel);
                rightLegModelChanger.EquipRightLegModelByName(defaultRightLegModel);
            }

            // HAND EQUIPMENT
            leftHandModelChanger.UnEquipLeftHandModels();
            rightHandModelChanger.UnEquipRightHandModels();

            if (playerInventory.currentHandEquipment != null)
            {
                leftHandModelChanger.EquipLeftHandModelByName(playerInventory.currentHandEquipment.leftHandModelName);
                rightHandModelChanger.EquipRightHandModelByName(playerInventory.currentHandEquipment.rightHandModelName);
            }
            else
            {
                leftHandModelChanger.EquipLeftHandModelByName(defaultLeftHandModel);
                rightHandModelChanger.EquipRightHandModelByName(defaultRightHandModel);
            }
        }

        public void OpenBlockingCollider()
        {
            blockingCollider.SetColliderDamageAbsorption(playerInventory.primaryWeapon);
            blockingCollider.EnableBlockingCollider();
        }

        public void CloseBlockingCollider()
        {
            blockingCollider.DisableBlockingCollider();
        }
    }
}
