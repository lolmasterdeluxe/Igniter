using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IG
{
    public class QuickSlotsUI : MonoBehaviour
    {
        PlayerInventory playerInventory;
        public List<Image> quickSlotIcon;
        public List<TextMeshProUGUI> quickSlotItemAmount;
        public TextMeshProUGUI quickSlot1itemName;

        private void Start()
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
        }

        private void Update()
        {
            UpdateWeaponQuickSlotUI();
        }

        public void UpdateWeaponQuickSlotUI()
        {
            quickSlotIcon[0].sprite = playerInventory.currentConsumable.itemIcon;
            quickSlotItemAmount[0].text = playerInventory.currentConsumable.currentItemAmount.ToString();
            quickSlot1itemName.text = playerInventory.currentConsumable.itemName;
        }
    }

}
