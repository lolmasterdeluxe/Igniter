using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IG
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;
        [SerializeField]
        private GameObject lobby;
        [SerializeField]
        private Transform dungeonSpawnPoint;
        [SerializeField]
        private Transform lobbySpawnPoint;
        [SerializeField]
        private InputHandler inputHandler;
        [SerializeField]
        private DungeonManager dungeonManager;

        public void StartGame()
        {
            dungeonManager.gameObject.SetActive(true);
            lobby.SetActive(false);
            dungeonManager.CreateDungeonLayout();
            player.transform.position = dungeonSpawnPoint.position;
            player.transform.rotation = dungeonSpawnPoint.rotation;
        }

        public void ResetGame()
        {
            dungeonManager.ClearDungeonLayout();
            dungeonManager.gameObject.SetActive(false);
            lobby.SetActive(true);
            player.transform.position = lobbySpawnPoint.position;
            player.transform.rotation = lobbySpawnPoint.rotation;
        }
    }
}
