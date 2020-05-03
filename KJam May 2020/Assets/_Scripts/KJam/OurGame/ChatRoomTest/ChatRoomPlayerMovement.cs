using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KJam.OurGame.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class ChatRoomPlayerMovement : MonoBehaviourPun
    {
        [SerializeField] private float movementSpeed = 0;

        private CharacterController controller = null;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
        }
        void Update()
        {
            if (photonView.IsMine)
            {
                TakeInput();
            }
        }
        private void TakeInput()
        {
            Vector3 movement = new Vector3
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = 0f,
                z = Input.GetAxisRaw("Vertical")
            }.normalized;

            controller.SimpleMove(movement * movementSpeed);
        }
    }
}

