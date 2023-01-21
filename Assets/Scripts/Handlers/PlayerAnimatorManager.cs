using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class PlayerAnimatorManager : AnimatorManager
    {
        PlayerManager playerManager;
        PlayerStats playerStats;
        InputHandler inputHandler;
        PlayerLocomotion playerLocomotion;
        private int vertical;
        private int horizontal;

        public void Initialize()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            playerStats = GetComponentInParent<PlayerStats>();
            anim = GetComponent<Animator>();
            inputHandler = GetComponent<InputHandler>();
            playerLocomotion = GetComponentInParent<PlayerLocomotion>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
        {
            #region Vertical
            float v = 0;

            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }
            else if (verticalMovement > 0.55f)
            {
                v = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if (verticalMovement < -0.55f)
            {
                v = -1f;
            }
            else
            {
                v = 0;
            }

            #endregion

            #region Horizontal
            float h = 0;

            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                h = -1f;
            }
            else
            {
                h = 0;
            }
            #endregion

            if (isSprinting && v != 0)
            {
                v = 2;
                h = horizontalMovement;
            }

            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        public void CanRotate()
        {
            anim.SetBool("canRotate", true);
            playerManager.rotateTowardsCamera = true;
        }

        public void StopRotate()
        {
            anim.SetBool("canRotate", false);
            playerManager.rotateTowardsCamera = false;
        }

        public void EnableCombo()
        {
            anim.SetBool("canDoCombo", true);
        }

        public void DisableCombo()
        {
            anim.SetBool("canDoCombo", false);
        }

        public void EnableIsInvulnerable()
        {
            anim.SetBool("isInvulnerable", true);
        }

        public void DisableIsInvulnerable()
        {
            anim.SetBool("isInvulnerable", false);
        }

        public void EnableIsParrying()
        {
            playerManager.isParrying = true;
        }

        public void DisableIsParrying()
        {
            playerManager.isParrying = false;
        }

        public void EnableCanBeRiposted()
        {
            playerManager.canBeRiposted = true;
        }

        public void DisableCanBeRiposted()
        {
            playerManager.canBeRiposted = false;
        }

        public override void TakeCriticalDamageAnimationEvent()
        {
            playerStats.TakeDamageNoAnimation(playerManager.pendingCriticalDamage);
            playerManager.pendingCriticalDamage = 0;
        }

        public void SetSheath(bool isTrue)
        {
            anim.SetBool("isSheathed", isTrue);
        }

        public void SetLocomotionType(int weaponType)
        {
            anim.SetInteger("weaponType", weaponType);
        }

        public void FreezePlayer()
        {
            anim.SetBool("isInteracting", true);
            anim.SetBool("canRotate", false);
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Vertical", 0);
            playerManager.isCameraLocked = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        public void UnfreezePlayer()
        {
            anim.SetBool("isInteracting", false);
            anim.SetBool("canRotate", true);
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Vertical", 0);
            playerManager.isCameraLocked = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnAnimatorMove()
        {
            if (playerManager.isInteracting == false)
                return;

            float delta = Time.deltaTime;
            playerLocomotion.rigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            playerLocomotion.rigidbody.velocity = velocity;
        }
    }
}
