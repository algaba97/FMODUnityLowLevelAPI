using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Minecart_Tilt : MonoBehaviour
{
    public Collider left_HalfCollider;
    public Collider right_HalfCollider;

    float tiltDuration = 0.5f;
    public bool canTilt = true;
    private bool isTilting = false;

    public void StartTilt(SCR_Player_ArmController.PLAYERID playerID, bool right)
    {
        if (isTilting)
        {
            if (right)
            {
                //Invoke("TiltBackLeftInstant", 0f);
                TiltBackLeftInstant();
                return;
            }
            else
            {
                //Invoke("TiltBackRightInstant", 0f);
                TiltBackRightInstant();
                return;
            }
        }
        if (canTilt)
        {
            if (playerID != SCR_Player_ArmController.PLAYERID.player2)
            {
                //Invoke("TiltCartLeft", 0f);
                TiltCartLeft();
				if(GetComponent<AudioSource> () != null)
					GetComponent<AudioSource> ().Play ();
            }
            else
            {
                //Invoke("TiltCartRight", 0f);
				TiltCartRight();
				if(GetComponent<AudioSource> () != null)
					GetComponent<AudioSource> ().Play ();
            }

            canTilt = false;
        }
    }

    void TiltCartLeft()
    {
        Debug.Log("3");
        EventManager.instance.TriggerEvent("EVT_CartTilt_Left");
        //Debug.Log("Tilt Left");
        DeactivateColliderHalf(0);
        //StartCoroutine(TiltBackLeft());
        isTilting = true;
    }

    void TiltCartRight()
    {
        Debug.Log("4");
        EventManager.instance.TriggerEvent("EVT_CartTilt_Right");
        //Debug.Log("Tilt Right");
        DeactivateColliderHalf(1);
        //StartCoroutine(TiltBackRight());
        isTilting = true;
    }

    private void DeactivateColliderHalf(int half)
    {

        //   Debug.Log("deactivated one side of the minecart");
        //0 is left
        if (half == 1)
        {
            left_HalfCollider.enabled = false;
        }

        //1 is right
        else
        {
            right_HalfCollider.enabled = false;
        }
    }

    private void EnableColliderHalf(int half)
    {
        //0 is left
        if (half == 1)
        {
            left_HalfCollider.enabled = true;
        }

        //1 is right
        else
        {
            right_HalfCollider.enabled = true;
        }
    }

    void TiltBackLeftInstant()
    {
        Debug.Log("1");
        EventManager.instance.TriggerEvent("EVT_CartTilt_BackLeft");
        StartCoroutine(CoolDown(0));
        isTilting = false;
    }

    void TiltBackRightInstant()
    {
        Debug.Log("2");
        EventManager.instance.TriggerEvent("EVT_CartTilt_BackRight");
        StartCoroutine(CoolDown(1));
        isTilting = false;
    }
    IEnumerator TiltBackLeft()
    {

        yield return new WaitForSeconds(0f);
        EventManager.instance.TriggerEvent("EVT_CartTilt_BackLeft");

        StartCoroutine(CoolDown(0));
    }

    IEnumerator TiltBackRight()
    {

        yield return new WaitForSeconds(0f);
        EventManager.instance.TriggerEvent("EVT_CartTilt_BackRight");

        StartCoroutine(CoolDown(1));
    }

    IEnumerator CoolDown(int id)
    {
        yield return new WaitForSeconds(0f);
        EnableColliderHalf(id);
        canTilt = true;
    }
}
