using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IG
{
    public class QuickSlotsUI : MonoBehaviour
    {
        public Image primaryWeaponIcon;
        public Image secondaryWeaponIcon;

        public void UpdateWeaponQuickSlotUI(bool isPrimary, WeaponItem weapon)
        {
            if (isPrimary)
            {
                if (weapon.itemIcon != null)
                {
                    primaryWeaponIcon.sprite = weapon.itemIcon;
                    primaryWeaponIcon.enabled = true;
                }
                else
                {
                    primaryWeaponIcon.sprite = null;
                    primaryWeaponIcon.enabled = false;
                }
                
            }
            else
            {
                if (weapon.itemIcon != null)
                {
                    secondaryWeaponIcon.sprite = weapon.itemIcon;
                    secondaryWeaponIcon.enabled = true;
                }
                else
                {
                    secondaryWeaponIcon.sprite = null;
                    secondaryWeaponIcon.enabled = false;
                }
            }
        }
    }

}
