using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotManager weaponSlotManager;

        public WeaponItem primaryWeapon;
        public WeaponItem secondaryWeapon;

        private void Awake()
        {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        }

        private void Start()
        {
            weaponSlotManager.LoadWeaponOnSlot(primaryWeapon, true);
            weaponSlotManager.LoadWeaponOnSlot(secondaryWeapon, false);
        }
    }
}
