using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        PlayerStats playerStats;
        public int attackCount;

        private void Awake()
        {
            animatorHandler = GetComponent<AnimatorHandler>();
            inputHandler = GetComponentInParent<InputHandler>();
            playerManager = GetComponentInParent<PlayerManager>();
            playerStats = GetComponentInParent<PlayerStats>();
            playerInventory = GetComponentInParent<PlayerInventory>();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);
                animatorHandler.PlayTargetAnimation(weapon.lightAttack[attackCount + 1], true);
                attackCount += 1;
            }
        }
        public void HandleLightAttack(WeaponItem weapon)
        {
            animatorHandler.PlayTargetAnimation(weapon.lightAttack[0], true);
            attackCount = 0;
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            animatorHandler.PlayTargetAnimation(weapon.heavyAttack[0], true);
            attackCount = 0;
        }

        public void HandleSheath(WeaponItem weapon, bool isTrue)
        {
            if (isTrue)
                animatorHandler.PlayTargetAnimation(weapon.sheathAnimation, true);
            else
                animatorHandler.PlayTargetAnimation(weapon.unsheathAnimation, true);

            animatorHandler.SetSheath(isTrue);
        }

        public void HandleLocomotionType(WeaponItem weapon)
        {
            animatorHandler.SetLocomotionType(weapon.weaponAnimationType);
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

                animatorHandler.anim.SetBool("isUsingPrimary", true);
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
                        playerInventory.currentSpell.AttemptToCastSpell(animatorHandler, playerStats);
                    else
                        animatorHandler.PlayTargetAnimation("Relax-No", true);
                }
            }
        }

        private void SuccessfullyCastSpell()
        {
            playerInventory.currentSpell.SuccessfullyCastSpell(animatorHandler, playerStats);
        }
        #endregion
    }
}
