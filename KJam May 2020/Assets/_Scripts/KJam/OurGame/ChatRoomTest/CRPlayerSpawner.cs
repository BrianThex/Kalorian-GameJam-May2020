using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KJam.OurGame.Player
{
    public class CRPlayerSpawner : MonoBehaviour
    {
        public static CRPlayerSpawner instance;

        [SerializeField] private GameObject basePrefab = null;

        [SerializeField] private Transform spawnPoint1 = null;
        [SerializeField] private Transform spawnPoint2 = null;

        private Transform spawnPoint;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            SetSpawnLocation(Network.MainMenuNetworkHandler.instance.playerNumber);
        }

        public void SetSpawnLocation(int playerNumber)
        {
            switch (playerNumber)
            {
                case 1:
                    spawnPoint = spawnPoint1;
                    PhotonNetwork.Instantiate(basePrefab.name, spawnPoint.position, Quaternion.identity);
                    break;
                case 2:
                    spawnPoint = spawnPoint2;
                    PhotonNetwork.Instantiate(basePrefab.name, spawnPoint.position, Quaternion.identity);
                    break;
                default:
                    Debug.Log($"Could not set player spawn point for player {playerNumber}");
                    break;
            }
        }
    }
}

