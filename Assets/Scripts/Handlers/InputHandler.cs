using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool a_input;
        public bool b_input;
        public bool rb_input;
        public bool rt_input;
        public bool s_input;
        public bool jump_input;
        public bool inventory_input;
        public bool lockOnInput;
        public bool rightStickRight_input;
        public bool rightStickLeft_input;

        public bool d_Pad_Up;
        public bool d_Pad_Down;
        public bool d_Pad_Left;
        public bool d_Pad_Right;

        public bool rollFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public bool lockOnFlag;
        public bool inventoryFlag;
        public float rollInputTimer;

        PlayerControls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        PlayerStats playerStats;
        CameraHandler cameraHandler;
        AnimatorHandler animatorHandler;
        UIManager uiManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
            playerStats = GetComponent<PlayerStats>();
            uiManager = FindObjectOfType<UIManager>();
            cameraHandler = FindObjectOfType<CameraHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }


        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

                // Input actions
                inputActions.PlayerActions.RB.performed += inputActions => rb_input = true;
                inputActions.PlayerActions.RT.performed += inputActions => rt_input = true;
                inputActions.PlayerQuickSlots.DPadRight.performed += i => d_Pad_Right = true;
                inputActions.PlayerQuickSlots.DPadLeft.performed += i => d_Pad_Left = true;
                inputActions.PlayerActions.A.performed += i => a_input = true;
                inputActions.PlayerActions.Jump.performed += i => jump_input = true;
                inputActions.PlayerActions.Inventory.performed += i => inventory_input = true;

                // Temporary disabled to accomodate sheath bool
                /*inputActions.PlayerActions.LockOn.performed += i => lockOnInput = true;
                inputActions.PlayerMovement.LockOnTargetRight.performed += i => rightStickRight_input = true;
                inputActions.PlayerMovement.LockOnTargetLeft.performed += i => rightStickLeft_input = true;*/
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            HandleMoveInput(delta);

            if (!playerManager.isSheathed)
            {
                HandleRollInput(delta);
                HandleAttackInput(delta);
                HandleLockOnInput();
            }

            HandleQuickSlotsInput();
            HandleSheathInput(delta);
            HandleInventoryInput();
        }

        private void HandleMoveInput(float delta)
        {
            if (playerStats.isDead)
            {
                horizontal = 0;
                vertical = 0;
                return;
            }
            
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleRollInput(float delta)
        {
            b_input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;

            // Sprint disabled
            //sprintFlag = b_input;

            if (b_input)
            {
                rollInputTimer += delta;
            }
            else
            {
                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {       
                    sprintFlag = false;
                    rollFlag = true;
                }

                rollInputTimer = 0;
            }
        }

        private void HandleAttackInput(float delta)
        {
            if (rb_input)
            {
                if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttacker.HandleWeaponCombo(playerInventory.primaryWeapon);
                    comboFlag = false;
                }
                else
                {
                    if (playerManager.canDoCombo || playerManager.isInteracting)
                        return;

                    animatorHandler.anim.SetBool("isUsingPrimary", true);
                    playerAttacker.HandleLightAttack(playerInventory.primaryWeapon);
                }
            }

            if (rt_input)
            {
                playerAttacker.HandleHeavyAttack(playerInventory.primaryWeapon);
            }
        }

        private void HandleSheathInput(float delta)
        {
            s_input = inputActions.PlayerActions.SheathUnsheath.IsPressed();

            if (s_input)
            {
                if (playerManager.isInteracting || playerInventory.primaryWeapon.isUnarmed)
                    return;

                if (playerManager.isSheathed)
                {
                    playerAttacker.HandleSheath(playerInventory.primaryWeapon, false);
                    playerAttacker.HandleLocomotionType(playerInventory.primaryWeapon);
                }
                else
                {
                    playerAttacker.HandleSheath(playerInventory.primaryWeapon, true);

                    // Remove lock-on upon sheating
                    lockOnInput = false;
                    lockOnFlag = false;
                    cameraHandler.ClearLockOnTargets();
                }
            }
        }

        private void HandleQuickSlotsInput()
        {
            if (d_Pad_Right)
            {
                playerInventory.ChangePrimaryWeapon(true);
            }
            else if (d_Pad_Left)
            {
                playerInventory.ChangePrimaryWeapon(false);
            }

            if ((d_Pad_Left || d_Pad_Right) && !playerManager.isSheathed)
            {
                playerAttacker.HandleSheath(playerInventory.primaryWeapon, false);
                playerAttacker.HandleLocomotionType(playerInventory.primaryWeapon);
            }
        }

        private void HandleInventoryInput()
        {
            if (inventory_input)
            {
                inventoryFlag = !inventoryFlag;

                if (inventoryFlag)
                {
                    uiManager.OpenSelectWindow();
                    uiManager.UpdateUI();
                    uiManager.hudWindow.SetActive(false);
                }
                else
                {
                    uiManager.CloseSelectWindow();
                    uiManager.CloseAllInventoryWindow();
                    uiManager.hudWindow.SetActive(true);
                }
            }
        }

        private void HandleLockOnInput()
        {
            lockOnInput = inputActions.PlayerActions.LockOn.WasReleasedThisFrame();
            rightStickRight_input = inputActions.PlayerMovement.LockOnTargetRight.WasReleasedThisFrame();
            rightStickLeft_input = inputActions.PlayerMovement.LockOnTargetLeft.WasReleasedThisFrame();

            if (lockOnInput && !lockOnFlag)
            {
                lockOnInput = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.nearestLockOnTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                    lockOnFlag = true;
                }
            }
            else if (lockOnInput && lockOnFlag)
            {
                lockOnInput = false;
                lockOnFlag = false;
                cameraHandler.ClearLockOnTargets();
            }

            if (lockOnFlag && rightStickLeft_input)
            {
                rightStickLeft_input = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.leftLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget;
                }
            }
            else if (lockOnFlag && rightStickRight_input)
            {
                rightStickRight_input = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.rightLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.rightLockTarget;
                }
            }

            cameraHandler.SetCameraHeight();
        }
    }
}
