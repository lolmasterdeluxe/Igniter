using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class HipModelChanger : MonoBehaviour
    {
        public List<GameObject> hipModels;

        private void Awake()
        {
            GetAllHipModels();
        }

        private void GetAllHipModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; ++i)
            {
                hipModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnEquipHipModels()
        {
            foreach (GameObject hipModels in hipModels)
            {
                hipModels.SetActive(false);
            }
        }

        public void EquipHipModelByName(string hipName)
        {
            for (int i = 0; i < hipModels.Count; ++i)
            {
                if (hipModels[i].name == hipName)
                {
                    hipModels[i].SetActive(true);
                }
            }
        }
    }
}