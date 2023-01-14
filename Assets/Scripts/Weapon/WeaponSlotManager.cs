using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class WeaponSlotManager : MonoBehaviour
    {
        PlayerManager playerManager;
        PlayerInventory playerInventory;

        [HideInInspector]
        public WeaponHolderSlot primarySlot;
        [HideInInspector]
        public WeaponHolderSlot secondarySlot;

        WeaponSheathSlot primarySheathSlot;
        WeaponSheathSlot secondarySheathSlot;

        public DamageCollider primaryDamageCollider;
        public DamageCollider secondaryDamageCollider;

        private bool isPrimary;
        public WeaponItem primaryWeapon, secondaryWeapon;

        QuickSlotsUI quickSlotUI;

        PlayerStats playerStats;
        InputHandler inputHandler;

        private void Awake()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            quickSlotUI = FindObjectOfType<QuickSlotsUI>();
            playerStats = GetComponentInParent<PlayerStats>();
            inputHandler = GetComponentInParent<InputHandler>();

            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isPrimarySlot)
                {
                    primarySlot = weaponSlot;
                }
                else if (weaponSlot.isSecondarySlot)
                {
                    secondarySlot = weaponSlot;
                }   
            }

            WeaponSheathSlot[] weaponSheathSlots = GetComponentsInChildren<WeaponSheathSlot>();
            foreach (WeaponSheathSlot weaponSheathSlot in weaponSheathSlots)
            {
                if (weaponSheathSlot.isPrimarySlot)
                {
                    primarySheathSlot = weaponSheathSlot;
                }
                else if (weaponSheathSlot.isSecondarySlot)
                {
                    secondarySheathSlot = weaponSheathSlot;
                }
            }
        }

        public void SetWeaponItemIsPrimary(WeaponItem weaponItem, bool isPrimary)
        {
            if (isPrimary)
                primaryWeapon = weaponItem;
            else
                secondaryWeapon = weaponItem;

            this.isPrimary = isPrimary;

            Debug.Log("primary: " + weaponItem.itemName);
            LoadWeaponOnSheathSlot();
            quickSlotUI.UpdateWeaponQuickSlotUI(isPrimary, weaponItem);
        }

        public void LoadWeaponOnSlot()
        {
            if (isPrimary)
            {
                primarySheathSlot.UnloadWeapon();
                primarySlot.LoadWeaponModel(primaryWeapon);
                LoadPrimaryWeaponDamageCollider();
            }
            else
            {
                secondarySheathSlot.UnloadWeapon();
                secondarySlot.LoadWeaponModel(secondaryWeapon);
                LoadSecondaryWeaponDamageCollider();
            }
        }

        public void LoadWeaponOnSheathSlot()
        {
            if (isPrimary)
            {
                primarySlot.UnloadWeapon();
                primarySheathSlot.LoadWeaponModel(primaryWeapon);
            }
            else
            {
                secondarySlot.UnloadWeapon();
                secondarySheathSlot.LoadWeaponModel(secondaryWeapon);
            }
        }

        public void SetWeaponBlockingTransform()
        {
            primarySlot.parentOverride.SetLocalPositionAndRotation(primaryWeapon.blockingOffset.transform.position, primaryWeapon.blockingOffset.transform.rotation);
        }

        public void ResetWeaponTransform()
        {
            primarySlot.parentOverride.SetLocalPositionAndRotation(primaryWeapon.defaultOffset.transform.position, primaryWeapon.defaultOffset.transform.rotation);
        }

        #region Handle Weapon's Damage Collider

        private void LoadPrimaryWeaponDamageCollider()
        {
            primaryDamageCollider = primarySlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            primaryDamageCollider.tag = "Player Weapon";
            primaryDamageCollider.currentWeaponDamage = playerInventory.primaryWeapon.baseDamage;
        }

        private void LoadSecondaryWeaponDamageCollider()
        {
            secondaryDamageCollider = secondarySlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            secondaryDamageCollider.tag = "Player Weapon";
            secondaryDamageCollider.currentWeaponDamage = playerInventory.secondaryWeapon.baseDamage;
        }

        public void OpenDamageCollider()
        {
            if (playerManager.isUsingPrimary)
            {
                primaryDamageCollider.EnableDamageCollider();
            }
            else if (playerManager.isUsingSecondary)
            {
                secondaryDamageCollider.EnableDamageCollider();
            }
        }

        public void CloseDamageCollider()
        {
            if (playerManager.isUsingPrimary)
            {
                primaryDamageCollider.DisableDamageCollider();
            }
            else if (playerManager.isUsingSecondary)
            {
                secondaryDamageCollider.DisableDamageCollider();
            }
        }

        #endregion

        #region Handle Weapon's Stamina Consumption

        public void DrainStaminaLightAttack()
        {
            playerStats.TakeStaminaDamage(Mathf.RoundToInt(primaryWeapon.baseStamina * primaryWeapon.lightAttackMultiplier));
        }

        public void DrainStaminaHeavyAttack()
        {
            playerStats.TakeStaminaDamage(Mathf.RoundToInt(primaryWeapon.baseStamina * primaryWeapon.heavyAttackMultiplier));
        }

        #endregion
    }
}
