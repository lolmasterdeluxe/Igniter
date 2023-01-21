using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.AI.Navigation;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace IG
{
    public class GameManager : MonoBehaviour
    {
        PlayerManager playerManager;
        PlayerAnimatorManager playerAnimatorManager;
        InputHandler inputHandler;
        [SerializeField]
        private GameObject player;
        [SerializeField]
        private GameObject lobby;
        [SerializeField]
        private Transform dungeonSpawnPoint;
        [SerializeField]
        private Transform lobbySpawnPoint;
        [SerializeField]
        private DungeonManager dungeonManager;
        public NavMeshSurface surface;
        [SerializeField]
        private CanvasGroup deathScreen;
        [SerializeField]
        private CanvasGroup endScreen;

        private void Awake()
        {
            surface = GetComponent<NavMeshSurface>();
            playerManager = FindObjectOfType<PlayerManager>();
            playerAnimatorManager = FindObjectOfType<PlayerAnimatorManager>();
            inputHandler = FindObjectOfType<InputHandler>();
        }

        public void StartGame()
        {
            dungeonManager.gameObject.SetActive(true);
            lobby.SetActive(false);
            dungeonManager.CreateDungeonLayout();
            surface.BuildNavMesh();
            player.transform.position = dungeonSpawnPoint.position;
            player.transform.rotation = dungeonSpawnPoint.rotation;
        }

        public void ResetGame()
        {
            dungeonManager.ClearDungeonLayout();
            dungeonManager.gameObject.SetActive(false);
            lobby.SetActive(true);
            surface.RemoveData();
            player.transform.position = lobbySpawnPoint.position;
            player.transform.rotation = lobbySpawnPoint.rotation;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene("MenuScene");
        }

        public void ActivateDeathScreen()
        {
            deathScreen.gameObject.SetActive(true);
            deathScreen.DOFade(1, 1);
        }

        public void ActivateEndScreen()
        {
            playerAnimatorManager.FreezePlayer();
            endScreen.gameObject.SetActive(true);
            endScreen.DOFade(1, 1);
        }
    }
}
