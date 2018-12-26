using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlasticMoose.Minecart
{
    public class Tilting : MonoBehaviour
    {
        public Collider left_HalfCollider;
        public Collider right_HalfCollider;
        public bool IsGrabbed;

        [HideInInspector]
        public int PlayerID;
        float tiltSpeed = 4;

        public float tiltPercentage;
        float vertInput;

        public bool InstantTilt;
        void Start()
        {

        }

        void Update()
        {

            if (IsGrabbed)
            {
                if (!InstantTilt)
                {
                    //figure out inputd
                    if (PlayerID == 1)
                        vertInput = Input.GetAxisRaw("Player1_Vertical_Left");
                    else
                        vertInput = Input.GetAxisRaw("Player2_Vertical_Left");

                    tiltPercentage += vertInput * (tiltSpeed * 10) * Time.deltaTime;
                }
                else
                    tiltPercentage = 25;

                if (tiltPercentage > 30)
                    tiltPercentage = 30;
                if (tiltPercentage < 0)
                    tiltPercentage = 0;

                if (tiltPercentage >= 25)
                    DeactivateColliderHalf(PlayerID);

                else
                    EnableColliderHalf(PlayerID);
            }
            //if the player lets go of the minecart it will tilt back to the original position
            else
            {
                EnableColliderHalf(PlayerID);
                tiltPercentage = 0;
            }
        }

        private void DeactivateColliderHalf(int half)
        {

            Debug.Log("deactivated one side of the minecart");
            //0 is left
            if (half == 1)
            {
                left_HalfCollider.enabled = false;
                EventManager.instance.TriggerEvent("EVT_CartTilt_Left_DActivate");
            }

            //1 is right
            else
            {
                right_HalfCollider.enabled = false;
                EventManager.instance.TriggerEvent("EVT_CartTilt_Right_DActivate");
            }
        }

        private void EnableColliderHalf(int half)
        {
            //0 is left
            if (half == 1)
            {
                left_HalfCollider.enabled = true;
                EventManager.instance.TriggerEvent("EVT_CartTilt_Left_Activate");
            }

            //1 is right
            else
            {
                right_HalfCollider.enabled = true;
                EventManager.instance.TriggerEvent("EVT_CartTilt_Right_Activate");
            }
        }
    }
}
