using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class WeaponSheathSlot : MonoBehaviour
    {
        public Transform backSheathOverride;
        public Transform hipSheathOverride;
        public bool isPrimarySlot;
        public bool isSecondarySlot;

        public GameObject currentWeaponModel;

        public void UnloadWeapon()
        {
            if (currentWeaponModel != null)
            {
                currentWeaponModel.SetActive(false);
            }
        }

        public void UnloadWeaponAndDestroy()
        {
            if (currentWeaponModel != null)
            {
                Destroy(currentWeaponModel);
            }
        }

        public void LoadWeaponModel(WeaponItem weaponItem)
        {
            UnloadWeaponAndDestroy();

            if (weaponItem == null)
            {
                // Unload Weapon
                UnloadWeapon();
                return;
            }

            GameObject model = Instantiate(weaponItem.modelPrefab) as GameObject;
            if (model != null)
            {
                if (weaponItem.sheathType == SheathType.Back)
                {
                    model.transform.parent = backSheathOverride;
                }
                else if (weaponItem.sheathType == SheathType.Hip)
                {
                    model.transform.parent = hipSheathOverride;
                }

                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }

            currentWeaponModel = model;
        }
    }
}
