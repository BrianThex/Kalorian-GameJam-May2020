using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KJam.OurGame.ChatRoomTest
{
    public class CRPlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab = null;

        private void Start()
        {
            PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        }
    }
}

