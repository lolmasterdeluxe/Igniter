using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IG
{
    public class MenuManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
