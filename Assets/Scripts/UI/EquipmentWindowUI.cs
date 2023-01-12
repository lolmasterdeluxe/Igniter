using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public enum EquipmentSlot
    {
        PrimarySlot01,
        PrimarySlot02,
        SecondarySlot01,
        SecondarySlot02,
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
                    if (i < 2 && i < playerInventory.weaponsInPrimarySlots.Length)
                        handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInPrimarySlots[i]);
                    else if (i >= 2 && i - 2 < playerInventory.weaponsInSecondarySlots.Length)
                        handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInSecondarySlots[i - 2]);
                }
            }
        }
    }
}
