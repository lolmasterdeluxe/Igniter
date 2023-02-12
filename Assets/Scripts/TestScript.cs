using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    
    public class TestScript : MonoBehaviour
    {
        public GameObject randomPrefab;
        // Start is called before the first frame update
        void Start()
        {
            Transform transform = gameObject.transform;
            Vector3 position = transform.position + transform.forward * 2;
            Instantiate(randomPrefab);
            randomPrefab.transform.position = position;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
