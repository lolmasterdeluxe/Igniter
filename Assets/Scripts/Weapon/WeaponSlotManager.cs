using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot primarySlot;
        WeaponHolderSlot secondarySlot;

        WeaponSheathSlot primarySheathSlot;
        WeaponSheathSlot secondarySheathSlot;

        DamageCollider primaryDamageCollider;
        DamageCollider secondaryDamageCollider;

        private bool isPrimary;
        public WeaponItem primaryWeapon, secondaryWeapon;

        QuickSlotsUI quickSlotUI;

        PlayerStats playerStats;

        private void Awake()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            quickSlotUI = FindObjectOfType<QuickSlotsUI>();
            playerStats = GetComponentInParent<PlayerStats>();

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

        #region Handle Weapon's Damage Collider

        private void LoadPrimaryWeaponDamageCollider()
        {
            primaryDamageCollider = primarySlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        private void LoadSecondaryWeaponDamageCollider()
        {
            secondaryDamageCollider = secondarySlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        public void OpenPrimaryDamageCollider()
        {
            primaryDamageCollider.EnableDamageCollider();
        }

        public void OpenSecondaryDamageCollider()
        {
            secondaryDamageCollider.EnableDamageCollider();
        }

        public void ClosePrimaryDamageCollider()
        {
            primaryDamageCollider.DisableDamageCollider();
        }

        public void CloseSecondaryDamageCollider()
        {
            secondaryDamageCollider.DisableDamageCollider();
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
