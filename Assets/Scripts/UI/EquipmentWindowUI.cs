using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public enum EquipmentSlot
    {
        PrimarySlot01,
        PrimarySlot02,
        PrimarySlot03,
        PrimarySlot04,
        SecondarySlot01,
        SecondarySlot02,
        SecondarySlot03,
        SecondarySlot04,
    }

    public class EquipmentWindowUI : MonoBehaviour
    {
        private EquipmentSlot equipmentSlot;
        [SerializeField]
        HandEquipmentSlotUI[] handEquipmentSlotUI;

        public void SelectEquipmentSlot(int slot)
        {
            equipmentSlot = (EquipmentSlot)slot;
        }

        public void LoadWeaponsOnEquipmentScreen(PlayerInventory playerInventory)
        { 
            for (int i = 0; i < handEquipmentSlotUI.Length; ++i)
            {
                if (handEquipmentSlotUI[i].equipmentSlot == (EquipmentSlot)i)
                {
                    if (i < 4 && i < playerInventory.weaponsInPrimarySlots.Length)
                        handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInPrimarySlots[i]);
                    else if (i >= 4 && i - 4 < playerInventory.weaponsInSecondarySlots.Length)
                        handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInSecondarySlots[i - 4]);
                }
            }
        }
    }
}
