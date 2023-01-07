using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IG
{
    public class UIManager : MonoBehaviour
    {
        public PlayerInventory playerInventory;
        public EquipmentWindowUI equipmentWindowUI;

        [Header("UI Windows")]
        public GameObject hudWindow;
        public GameObject selectWindow;
        public GameObject weaponInventoryWindow;
        public GameObject equipmentScreenWindow;
        public GameObject interactAlertPopUp;
        public TextMeshProUGUI interactAlertPopUpText;

        [Header("Equipment Window Slot Selected")]
        public EquipmentSlot equipmentSlotSelected;

        [Header("Weapon Inventory")]
        public GameObject weaponInventorySlotPrefab;
        public Transform weaponInventorySlotsParent;
        WeaponInventorySlot[] weaponInventorySlots;

        private void Start()
        {
            weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
            equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
        }

        public void UpdateUI()
        {
            #region Weapon Inventory Slots
            for (int i = 0; i < weaponInventorySlots.Length; ++i)
            {
                if (i < playerInventory.weaponsInventory.Count)
                {
                    if (weaponInventorySlots.Length < playerInventory.weaponsInventory.Count)
                    {
                        Instantiate(weaponInventorySlotPrefab, weaponInventorySlotsParent);
                        weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
                    }
                    weaponInventorySlots[i].AddItem(playerInventory.weaponsInventory[i]);
                }
                else
                {
                    weaponInventorySlots[i].ClearInventorySlot();
                }
            }

            #endregion
        }

        public void OpenSelectWindow()
        {
            selectWindow.SetActive(true);
        }

        public void CloseSelectWindow()
        {
            selectWindow.SetActive(false);
        }

        public void CloseAllInventoryWindow()
        {
            weaponInventoryWindow.SetActive(false);
            equipmentScreenWindow.SetActive(false);
            ResetAllSelectedSlots();
        }

        public void ResetAllSelectedSlots()
        {
            equipmentSlotSelected = 0;
        }

        public void ActivateInteractAlertPopup(string text)
        {
            interactAlertPopUp.SetActive(true);
            interactAlertPopUpText.text = text;
            Debug.Log("+Alert pop up");
        }

        public void DeactivateInteractAlertPopup()
        {
            interactAlertPopUp.SetActive(false);
            interactAlertPopUpText.text = "";
            Debug.Log("-Alert pop up");
        }
    }
}
