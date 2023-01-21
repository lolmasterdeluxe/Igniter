using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class PlayerManager : CharacterManager
    {
        InputHandler inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
        PlayerStats playerStats;
        PlayerAnimatorManager playerAnimatorManager;
        PlayerLocomotion playerLocomotion;
        PlayerInventory playerInventory;
        UIManager uiManager;

        public bool isInteracting;

        [Header("Player Flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;
        public bool isSheathed;
        public bool isUsingPrimary;
        public bool isUsingSecondary;
        public bool isInvulnerable;
        public bool rotateTowardsCamera;
        public bool isCameraLocked;

        private void Start()
        {
            cameraHandler = CameraHandler.singleton;
            inputHandler = GetComponent<InputHandler>();
            playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();
            anim = GetComponentInChildren<Animator>();
            playerStats = GetComponent<PlayerStats>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            playerInventory = GetComponent<PlayerInventory>();
            uiManager = FindObjectOfType<UIManager>();
            backStabCollider = GetComponentInChildren<CriticalDamageCollider>();
        }

        // Update is called once per frame
        private void Update()
        {
            float delta = Time.deltaTime;

            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");
            isSheathed = anim.GetBool("isSheathed");
            isUsingPrimary = anim.GetBool("isUsingPrimary");
            isUsingSecondary = anim.GetBool("isUsingSecondary");
            isInvulnerable = anim.GetBool("isInvulnerable");
            isFiringSpell = anim.GetBool("isFiringSpell");
            anim.SetBool("isBlocking", isBlocking);
            anim.SetBool("isInAir", isInAir);
            anim.SetBool("isDead", playerStats.isDead);
            anim.SetBool("isStunned", isStunned);

            inputHandler.TickInput(delta);
            playerAnimatorManager.canRotate = anim.GetBool("canRotate");
            playerLocomotion.HandleRollingAndSprinting(delta, playerInventory.primaryWeapon);
            //playerLocomotion.HandleJumping();
            playerStats.RegenerateStamina();

            CheckForInteractableObject();
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
            playerLocomotion.HandleRotation(delta, rotateTowardsCamera);
        }

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;

            inputHandler.y_input = false;
            inputHandler.x_input = false;
            inputHandler.a_input = false;

            inputHandler.rb_input = false;
            inputHandler.lt_input = false;

            inputHandler.d_Pad_Up = false;
            inputHandler.d_Pad_Down = false;
            inputHandler.d_Pad_Left = false;
            inputHandler.d_Pad_Right = false;

            inputHandler.menu_input = false;

            float delta = Time.fixedDeltaTime;
            if (cameraHandler != null && !isCameraLocked)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }

            if (isInAir)
            {
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
            }
        }

        #region Player Interactions

        public void CheckForInteractableObject()
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 10f, cameraHandler.ignoreLayers))
            { 
                if (hit.collider.tag == "Interactable")
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if (interactableObject != null && interactableObject.radius >= Vector3.Distance(transform.position, hit.transform.position))
                    {
                        string interactableText = interactableObject.interactableText;
                        uiManager.interactableUI.interactableText.text = interactableText;
                        uiManager.interactAlertPopUpSphereCast.SetActive(true);

                        if (inputHandler.a_input)
                        {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }
                else
                {
                    if (uiManager.interactAlertPopUpSphereCast != null)
                    {
                        uiManager.interactAlertPopUpSphereCast.SetActive(false);
                    }

                    if (uiManager.itemPopUp != null && inputHandler.a_input)
                    {
                        uiManager.itemPopUp.SetActive(false);
                    }
                }
            }
            else
            {
                if (uiManager.interactAlertPopUpSphereCast != null)
                {
                    uiManager.interactAlertPopUpSphereCast.SetActive(false);
                }

                if (uiManager.itemPopUp != null && inputHandler.a_input)
                {
                    uiManager.itemPopUp.SetActive(false);
                }
            }

        }

        public void OpenChestInteraction(Transform playerStandsHereWhenOpeningChest)
        {
            playerLocomotion.rigidbody.velocity = Vector3.zero; // Stops the player from ice skating
            transform.position = playerStandsHereWhenOpeningChest.position;
            playerAnimatorManager.PlayTargetAnimation("Relax-Activate", true);
        }

        #endregion
    }
}
