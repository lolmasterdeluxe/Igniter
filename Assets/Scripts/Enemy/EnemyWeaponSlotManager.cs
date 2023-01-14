using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class EnemyWeaponSlotManager : MonoBehaviour
    {
        public WeaponItem primaryWeapon;
        public WeaponItem secondaryWeapon;

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

        private void Start()
        {
            LoadWeaponsOnBothSlots();
        }

        public void LoadWeaponOnSlot(WeaponItem weapon, bool isPrimary)
        {
            if (isPrimary)
            {
                //primarySlot.currentWeapon = weapon;
                primarySlot.LoadWeaponModel(weapon);
                LoadWeaponsDamageCollider(true);
            }
            else
            {
                //secondarySlot.currentWeapon = weapon;
                secondarySlot.LoadWeaponModel(weapon);
                LoadWeaponsDamageCollider(false);
            }
        }

        public void LoadWeaponsOnBothSlots()
        {
            if (primaryWeapon != null)
            {
                LoadWeaponOnSlot(primaryWeapon, true);
            }
            if (secondaryWeapon != null)
            {
                LoadWeaponOnSlot(secondaryWeapon, false);
            }
        }

        public void LoadWeaponsDamageCollider(bool isPrimary)
        {
            if (isPrimary)
            {
                primaryDamageCollider = primarySlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
                primaryDamageCollider.tag = "Enemy Weapon";
                primaryDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
            }
            else
            {
                secondaryDamageCollider = secondarySlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
                secondaryDamageCollider.tag = "Enemy Weapon";
                secondaryDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
            }
        }


        public void OpenDamageCollider()
        {
            primaryDamageCollider.EnableDamageCollider();
        }

        public void CloseDamageCollider()
        {
            primaryDamageCollider.DisableDamageCollider();
        }

        #region Handle Weapon's Stamina Consumption

        public void DrainStaminaLightAttack()
        {

        }

        public void DrainStaminaHeavyAttack()
        {

        }

        #endregion

        public void EnableCombo()
        {

        }

        public void DisableCombo()
        {

        }
    }
}
