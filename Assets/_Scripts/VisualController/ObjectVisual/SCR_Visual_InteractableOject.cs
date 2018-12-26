using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SCR_Visual_InteractableOject : MonoBehaviour
{
    private SCR_Interaction_Object thisObject;

    void Start()
    {
        thisObject = GetComponentInParent<SCR_Interaction_Object>();
    }

    void Update()
    {

    }
}
