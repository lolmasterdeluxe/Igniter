using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotManager weaponSlotManager;

        [Header("Quick Slot Items")]
        public SpellItem currentSpell;
        public WeaponItem primaryWeapon;
        public WeaponItem secondaryWeapon;
        public ConsumableItem currentConsumable;

        [Header("Current Equipment")]
        public HelmetEquipment currentHelmetEquipment;

        public WeaponItem[] weaponsInPrimarySlots = new WeaponItem[1];  
        public WeaponItem[] weaponsInSecondarySlots = new WeaponItem[1];

        public int currentPrimaryWeaponIndex = 0;
        public int currentSecondaryWeaponIndex = 0;

        public List<WeaponItem> weaponsInventory;
        private void Awake()
        {
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        }

        private void Start()
        {
            primaryWeapon = weaponsInPrimarySlots[currentPrimaryWeaponIndex];
            secondaryWeapon = weaponsInPrimarySlots[currentSecondaryWeaponIndex];
            weaponSlotManager.SetWeaponItemIsPrimary(primaryWeapon, true);
            currentConsumable.currentItemAmount = currentConsumable.maxItemAmount;
            //weaponSlotManager.SetWeaponItemIsPrimary(secondaryWeapon, false);
            Debug.Log("Amt of weps: " + weaponsInPrimarySlots.Length);
        }

        public void ChangePrimaryWeapon(bool plus)
        {
            if (plus)
                currentPrimaryWeaponIndex++;
            else
                currentPrimaryWeaponIndex--;

            if (currentPrimaryWeaponIndex >= weaponsInPrimarySlots.Length)
            {
                currentPrimaryWeaponIndex = 0;
                weaponSlotManager.SetWeaponItemIsPrimary(weaponsInPrimarySlots[currentPrimaryWeaponIndex], true);
            }
            else if (currentPrimaryWeaponIndex < 0)
            {
                currentPrimaryWeaponIndex = weaponsInPrimarySlots.Length - 1;
                weaponSlotManager.SetWeaponItemIsPrimary(weaponsInPrimarySlots[currentPrimaryWeaponIndex], true);
            }

            if (weaponsInPrimarySlots[currentPrimaryWeaponIndex] != null)
            {
                primaryWeapon = weaponsInPrimarySlots[currentPrimaryWeaponIndex];
                weaponSlotManager.SetWeaponItemIsPrimary(weaponsInPrimarySlots[currentPrimaryWeaponIndex], true);
            }
        }
    }
}
