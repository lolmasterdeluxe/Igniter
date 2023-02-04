using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class RightUpperArmModelChanger : MonoBehaviour
    {
        public List<GameObject> rightUpperArmModels;

        private void Awake()
        {
            GetAllRightUpperArmModels();
        }

        private void GetAllRightUpperArmModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; ++i)
            {
                rightUpperArmModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnEquipRightUpperArmModels()
        {
            foreach (GameObject rightUpperArmModels in rightUpperArmModels)
            {
                rightUpperArmModels.SetActive(false);
            }
        }

        public void EquipRightUpperArmModelByName(string rightUpperArmName)
        {
            for (int i = 0; i < rightUpperArmModels.Count; ++i)
            {
                if (rightUpperArmModels[i].name == rightUpperArmName)
                {
                    rightUpperArmModels[i].SetActive(true);
                }
            }
        }
    }
}
