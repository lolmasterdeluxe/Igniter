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

        public bool y_input;
        public bool x_input;
        public bool a_input;
        public bool b_input;

        public bool lb_input;
        public bool lt_input;
        public bool ls_input;

        public bool rb_input;
        public bool rt_input;
        public bool rs_input;

        public bool rsl_input;
        public bool rsr_input;

        public bool menu_input;

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

        public Transform criticalAttackRayCastStartPoint;

        PlayerControls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        PlayerAnimatorManager playerAnimatorManager;
        PlayerEffectsManager playerEffectsManager;
        PlayerStats playerStats;
        BlockingCollider blockingCollider;
        WeaponSlotManager weaponSlotManager;
        CameraHandler cameraHandler;
        PlayerAnimatorManager animatorHandler;
        UIManager uiManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            playerAttacker = GetComponentInChildren<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
            playerStats = GetComponent<PlayerStats>();
            playerEffectsManager = GetComponentInChildren<PlayerEffectsManager>();
            playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            blockingCollider = GetComponentInChildren<BlockingCollider>();
            uiManager = FindObjectOfType<UIManager>();
            cameraHandler = FindObjectOfType<CameraHandler>();
            animatorHandler = GetComponentInChildren<PlayerAnimatorManager>();
        }


        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

                // Input actions

                // Sheath/Unsheath
                inputActions.PlayerActions.Y.performed += i => y_input = true;
                // Use Item
                inputActions.PlayerActions.X.performed += i => x_input = true;
                // Interact (Examine/Open/Pick-Up/Etc..)
                inputActions.PlayerActions.A.performed += i => a_input = true;
                // Dash/Roll/Backstep
                // inputActions.PlayerActions.B.performed += i => b_input = true;

                // Right Hand Weapon Light Attack / Heavy Attack
                inputActions.PlayerActions.RB.performed += inputActions => rb_input = true;

                inputActions.PlayerActions.RT.performed += inputActions => rt_input = true;
                inputActions.PlayerActions.RT.canceled += inputActions => rt_input = false;

                // Lock on (target switch left/target switch right)
                /*inputActions.PlayerActions.RS.performed += inputActions => rs_input = true;
                inputActions.PlayerActions.RSL.performed += inputActions => rsl_input = true;
                inputActions.PlayerActions.RSR.performed += inputActions => rsr_input = true;*/

                // Blocking
                inputActions.PlayerActions.LB.performed += inputActions => lb_input = true;
                inputActions.PlayerActions.LB.canceled += inputActions => lb_input = false;
                inputActions.PlayerActions.LT.performed += inputActions => lt_input = true; // UNUSED

                inputActions.PlayerQuickSlots.DPadRight.performed += i => d_Pad_Right = true;
                inputActions.PlayerQuickSlots.DPadLeft.performed += i => d_Pad_Left = true;

                inputActions.PlayerActions.MenuButton.performed += i => menu_input = true;
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
                HandleCombatInput(delta);
                HandleLockOnInput();
            }

            HandleQuickSlotsInput();
            HandleSheathInput(delta);
            HandleInventoryInput();
            HandleUseConsumableInput();
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
            b_input = inputActions.PlayerActions.B.phase == UnityEngine.InputSystem.InputActionPhase.Started;

            // Sprint disabled
            // sprintFlag = b_input;

            if (b_input)
            {
                rollInputTimer += delta;

                if (playerStats.currentStamina <= 0)
                {
                    b_input = false;
                    sprintFlag = false;
                }
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

        private void HandleCombatInput(float delta)
        {
            if (rb_input)
            {
                if (rt_input)
                {
                    playerAttacker.HandleRTAction();
                }
                else
                {
                    playerAttacker.HandleRBAction();
                    playerAttacker.AttemptBackStabOrRiposte();
                }
            }

            if (lb_input)
            {
                playerAttacker.HandleLBAction();
            }
            else
            {
                playerManager.isBlocking = false;

                if (blockingCollider.blockingCollider.enabled)
                {
                    blockingCollider.DisableBlockingCollider();
                }
            }

            if (lt_input)
            {
                //Handle weapon art
                playerAttacker.HandleLTAction();
            }
        }

        private void HandleSheathInput(float delta)
        {
            if (y_input)
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
                    DisableLockOn();
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
            if (menu_input)
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
            rs_input = inputActions.PlayerActions.RS.WasPressedThisFrame();
            rsr_input = inputActions.PlayerActions.RSR.WasPressedThisFrame();
            rsl_input = inputActions.PlayerActions.RSL.WasPressedThisFrame();

            if (rs_input && !lockOnFlag)
            {
                rs_input = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.nearestLockOnTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                    lockOnFlag = true;
                }
            }
            else if (rs_input && lockOnFlag)
            {
                DisableLockOn();
            }

            if (lockOnFlag && rsr_input)
            {
                rsr_input = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.leftLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget;
                }
            }
            else if (lockOnFlag && rsl_input)
            {
                rsl_input = false;
                cameraHandler.HandleLockOn();
                if (cameraHandler.rightLockTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.rightLockTarget;
                }
            }

            cameraHandler.SetCameraHeight();
        }

        private void HandleUseConsumableInput()
        {
            if (x_input)
            {
                x_input = false;
                // Use current consumbale
                playerInventory.currentConsumable.AttemptToConsumeItem(playerAnimatorManager, weaponSlotManager, playerEffectsManager);
            }
        }

        public void DisableLockOn()
        {
            rs_input = false;
            lockOnFlag = false;
            cameraHandler.ClearLockOnTargets();
        }
    }
}
