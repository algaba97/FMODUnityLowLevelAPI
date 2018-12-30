using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Minecart_VisualController : MonoBehaviour
{
    Animator anim;


    void OnEnable()
    {

        anim = GetComponent<Animator>();
        EventManager.instance.StartListening("EVT_CartTilt_Right", TiltRight);
        EventManager.instance.StartListening("EVT_CartTilt_BackRight", TiltBackRight);
        EventManager.instance.StartListening("EVT_CartTilt_Left", TiltLeft);
        EventManager.instance.StartListening("EVT_CartTilt_BackLeft", TiltBackLeft);


    }
    void OnDestroy()
    {
        if (EventManager.instance != null)
        {
            EventManager.instance.StopListening("EVT_CartTilt_Right", TiltRight);
            EventManager.instance.StopListening("EVT_CartTilt_BackRight", TiltBackRight);
            EventManager.instance.StopListening("EVT_CartTilt_Left", TiltLeft);
            EventManager.instance.StopListening("EVT_CartTilt_BackLeft", TiltBackLeft);
        }
    }

    void TiltLeft()
    {
        anim.SetBool("TiltLeft", true);
		anim.SetBool("TiltRight", false);
    }
    void TiltBackLeft()
    {
        anim.SetBool("TiltLeft", false);
		anim.SetBool("TiltRight", false);
	
    }
    void TiltRight()
    {
        anim.SetBool("TiltRight", true);
		anim.SetBool("TiltLeft", false);
    }
    void TiltBackRight()
    {
        anim.SetBool("TiltRight", false);
		anim.SetBool("TiltLeft", false);

    }
}
