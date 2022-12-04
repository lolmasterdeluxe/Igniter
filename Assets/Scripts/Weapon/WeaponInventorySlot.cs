using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IG
{
    public class WeaponInventorySlot : MonoBehaviour
    {
        PlayerInventory playerInventory;
        WeaponSlotManager weaponSlotManager;
        UIManager uiManager;

        public Image icon;
        WeaponItem item;

        private void Awake()
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
            uiManager = FindObjectOfType<UIManager>();
            weaponSlotManager = FindObjectOfType<WeaponSlotManager>();
        }
        public void AddItem(WeaponItem newItem)
        {
            item = newItem;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }

        public void ClearInventorySlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

        public void EquipThisItem()
        {
            playerInventory.weaponsInventory.Add(playerInventory.weaponsInPrimarySlots[(int)uiManager.equipmentSlotSelected]);
            playerInventory.weaponsInPrimarySlots[(int)uiManager.equipmentSlotSelected] = item;
            playerInventory.weaponsInventory.Remove(item);

            playerInventory.primaryWeapon = playerInventory.weaponsInPrimarySlots[playerInventory.currentPrimaryWeaponIndex];
            // playerInventory.secondaryWeapon = playerInventory.weaponsInSecondarySlots[playerInventory.currentSecondaryWeaponIndex];

            weaponSlotManager.SetWeaponItemIsPrimary(playerInventory.primaryWeapon, true);
            // weaponSlotManager.SetWeaponItemIsPrimary(playerInventory.secondaryWeapon, false);

            uiManager.equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
            uiManager.ResetAllSelectedSlots();
        }
    }
}
