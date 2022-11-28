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

        private void Start()
        {
            //handEquipmentSlotUI = GetComponentsInChildren<HandEquipmentSlotUI>();
        }

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
                    if (i < 4)
                        handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInPrimarySlots[i]);
                    else
                        handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInSecondarySlots[i]);
                }
            }
        }
    }
}
