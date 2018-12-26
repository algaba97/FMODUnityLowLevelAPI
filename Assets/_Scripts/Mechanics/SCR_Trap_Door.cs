using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Trap_Door : MonoBehaviour
{
    public SCR_Interactable_ChainHandle connectedChainActivator;

    public float doorSpeed;
    public bool isHeld;
    bool doorInPlace;

    void Update()
    {

        if (connectedChainActivator != null)
            isHeld = connectedChainActivator.isHeld;

        if (transform.localRotation.x >= 0.7f)
        {
            doorSpeed = 0;
            doorInPlace = true;
        }

        if (isHeld && !doorInPlace)
        {
            doorSpeed = 0.5f;
            transform.Rotate(Vector3.right * doorSpeed);
        }
        else if (!isHeld && !doorInPlace)
        {
            if (transform.rotation.x <= 0)
                return;
            doorSpeed = 5;
            transform.Rotate(Vector3.left * doorSpeed);
        }
    }
}
