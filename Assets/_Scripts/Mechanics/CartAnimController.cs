using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartAnimController : MonoBehaviour
{

    Animator anim;

    // Use this for initialization
    void OnEnable()
    {
        anim = GetComponent<Animator>();
        EventManager.instance.StartListening("EVT_CartTilt_Right_DActivate", TiltLeft);
        EventManager.instance.StartListening("EVT_CartTilt_Right_Activate", TiltBackLeft);
        EventManager.instance.StartListening("EVT_CartTilt_Left_DActivate", TiltRight);
        EventManager.instance.StartListening("EVT_CartTilt_Left_Activate", TiltBackRight);
    }
    void OnDestroy()
    {
        if (EventManager.instance != null)
        {
            EventManager.instance.StopListening("EVT_CartTilt_Right_DActivate", TiltLeft);
            EventManager.instance.StopListening("EVT_CartTilt_Right_Activate", TiltBackLeft);
            EventManager.instance.StopListening("EVT_CartTilt_Left_DActivate", TiltRight);
            EventManager.instance.StopListening("EVT_CartTilt_Left_Activate", TiltBackRight);
        }
    }

    void TiltLeft()
    {
        anim.SetInteger("CartState", 1);
        print("TiltLeft");
    }
    void TiltBackLeft()
    {
        anim.SetInteger("CartState", 0);
        //print ("TiltBackLeft");
    }
    void TiltRight()
    {
        anim.SetInteger("CartState", 2);
        print("TiltRight");
    }
    void TiltBackRight()
    {
        anim.SetInteger("CartState", 0);
        print("TiltBackRight");
    }

}
