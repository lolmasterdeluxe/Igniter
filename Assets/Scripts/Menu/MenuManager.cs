using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IG
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField]
        GameObject menuScreen;
        [SerializeField]
        GameObject controlsScreen;
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
            SceneManager.LoadScene(sceneName);
        }

        public void StartGame()
        {

            StartCoroutine(ShowControls());
        }

        public IEnumerator ShowControls()
        {
            menuScreen.SetActive(false);
            controlsScreen.SetActive(true);

            yield return new WaitForSeconds(3);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");
            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
