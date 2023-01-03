using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class PlayerAttacker : MonoBehaviour
    {
        PlayerAnimatorManager playerAnimatorManager;
        InputHandler inputHandler;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        PlayerStats playerStats;
        WeaponSlotManager weaponSlotManager;
        public int attackCount;

        LayerMask backStabLayer = 1 << 14;

        private void Awake()
        {
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            inputHandler = GetComponentInParent<InputHandler>();
            playerManager = GetComponentInParent<PlayerManager>();
            playerStats = GetComponentInParent<PlayerStats>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            weaponSlotManager = GetComponent<WeaponSlotManager>();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (playerStats.currentStamina <= 0)
                return;

            if (inputHandler.comboFlag)
            {
                playerAnimatorManager.anim.SetBool("canDoCombo", false);
                playerAnimatorManager.PlayTargetAnimation(weapon.lightAttack[attackCount + 1], true);
                attackCount += 1;
            }
        }
        public void HandleLightAttack(WeaponItem weapon)
        {
            if (playerStats.currentStamina <= 0)
                return;

            playerAnimatorManager.PlayTargetAnimation(weapon.lightAttack[0], true);
            attackCount = 0;
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            if (playerStats.currentStamina <= 0)
                return;

            playerAnimatorManager.PlayTargetAnimation(weapon.heavyAttack[0], true);
            attackCount = 0;
        }

        public void HandleSheath(WeaponItem weapon, bool isTrue)
        {
            if (isTrue)
                playerAnimatorManager.PlayTargetAnimation(weapon.sheathAnimation, true);
            else
                playerAnimatorManager.PlayTargetAnimation(weapon.unsheathAnimation, true);

            playerAnimatorManager.SetSheath(isTrue);
        }

        public void HandleLocomotionType(WeaponItem weapon)
        {
            playerAnimatorManager.SetLocomotionType(weapon.weaponAnimationType);
        }

        #region Input Actions
        public void HandleRBAction()
        {
            if (playerInventory.primaryWeapon.weaponType == WeaponType.MeleeWeapon)
            {
                PerformRBMeleeAction();
            }
            else if (playerInventory.primaryWeapon.weaponType == WeaponType.SpellCaster || playerInventory.primaryWeapon.weaponType == WeaponType.FaithCaster || playerInventory.primaryWeapon.weaponType == WeaponType.PyroCaster)
            {
                // Handle Magic Action
                PerformRBMagicAction(playerInventory.primaryWeapon);
            }
        }
        #endregion

        #region Attack Actions
        private void PerformRBMeleeAction()
        {
            if (playerManager.canDoCombo)
            {
                inputHandler.comboFlag = true;
                HandleWeaponCombo(playerInventory.primaryWeapon);
                inputHandler.comboFlag = false;
            }
            else
            {
                if (playerManager.canDoCombo || playerManager.isInteracting)
                    return;

                playerAnimatorManager.anim.SetBool("isUsingPrimary", true);
                HandleLightAttack(playerInventory.primaryWeapon);
            }
        }

        private void PerformRBMagicAction(WeaponItem weapon)
        {
            if (playerManager.isInteracting)
                return;

            if (weapon.weaponType == WeaponType.FaithCaster)
            {
                if (playerInventory.currentSpell != null && playerInventory.currentSpell.isFaithSpell)
                {
                    if (playerStats.currentFocusPoints >= playerInventory.currentSpell.focusPointCost)
                        playerInventory.currentSpell.AttemptToCastSpell(playerAnimatorManager, playerStats);
                    else
                        playerAnimatorManager.PlayTargetAnimation("Relax-No", true);
                }
            }
        }

        private void SuccessfullyCastSpell()
        {
            playerInventory.currentSpell.SuccessfullyCastSpell(playerAnimatorManager, playerStats);
        }
        #endregion

        public void AttemptBackStabOrRiposte()
        {
            if (playerStats.currentStamina <= 0)
                return;

            RaycastHit hit;

            if (Physics.Raycast(inputHandler.criticalAttackRayCastStartPoint.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f, backStabLayer))
            {
                CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
                DamageCollider primaryWeapon = weaponSlotManager.primaryDamageCollider;

                if (enemyCharacterManager != null)
                {
                    // CHECK FOR TEAM I.D. (So you cant back stab friends or yourself?)
                    playerManager.transform.position = enemyCharacterManager.backStabCollider.backStabberStandPoint.position;
                    Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                    rotationDirection = hit.transform.position - playerManager.transform.position;
                    rotationDirection.y = 0;
                    rotationDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDirection);
                    Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManager.transform.rotation = targetRotation;

                    int criticalDamage = playerInventory.primaryWeapon.criticalDamageMultiplier * primaryWeapon.currentWeaponDamage;
                    enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                    playerAnimatorManager.PlayTargetAnimation("Back Stab", true);
                    enemyCharacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Back Stabbed", true);
                    // do damage
                }
            }

        }
    }
}
