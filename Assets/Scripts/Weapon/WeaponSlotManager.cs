using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot primarySlot;
        WeaponHolderSlot secondarySlot;

        DamageCollider primaryDamageCollider;
        DamageCollider secondaryDamageCollider;

        private void Awake()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
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
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isPrimary)
        {
            if (isPrimary)
            {
                primarySlot.LoadWeaponModel(weaponItem);
                LoadPrimaryWeaponDamageCollider();
            }
            else
            {
                secondarySlot.LoadWeaponModel(weaponItem);
                LoadSecondaryWeaponDamageCollider();
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
    }
}
