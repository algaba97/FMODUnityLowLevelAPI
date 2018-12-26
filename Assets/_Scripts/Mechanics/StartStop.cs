using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlasticMoose.Minecart
{
    public class StartStop : MonoBehaviour
    {
        public Movement move;

        // Use this for initialization
        void Start()
        {
            //move.Acceleration = 0;
            //move.CurrentSpeed = 0;
        }

        void OnTriggerExit(Collider other)
        {
            move.Acceleration = 7.5f;
        }
    }
}
