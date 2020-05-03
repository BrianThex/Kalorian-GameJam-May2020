using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KJam.OurGame
{
	public class DontDestroyOnLoad : MonoBehaviour
{
        public static DontDestroyOnLoad instance;
        void Awake()
        {
            DontDestroyOnLoad(this);

            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

