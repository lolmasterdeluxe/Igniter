using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class RightHandModelChanger : MonoBehaviour
    {
        public List<GameObject> rightHandModels;

        private void Awake()
        {
            GetAllRightHandModels();
        }

        private void GetAllRightHandModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; ++i)
            {
                rightHandModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnEquipRightHandModels()
        {
            foreach (GameObject rightHandModels in rightHandModels)
            {
                rightHandModels.SetActive(false);
            }
        }

        public void EquipRightHandModelByName(string rightHandName)
        {
            for (int i = 0; i < rightHandModels.Count; ++i)
            {
                if (rightHandModels[i].name == rightHandName)
                {
                    rightHandModels[i].SetActive(true);
                }
            }
        }
    }
}
