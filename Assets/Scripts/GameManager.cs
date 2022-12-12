using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.AI.Navigation;

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
        public NavMeshSurface surface;

        private void Awake()
        {
            surface = GetComponent<NavMeshSurface>();
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
    }
}
