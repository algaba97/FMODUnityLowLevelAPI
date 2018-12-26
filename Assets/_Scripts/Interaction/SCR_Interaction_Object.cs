using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Interaction_Object : MonoBehaviour
{
    public bool avalible = true;
    public string ID;
    public bool combined;

    //Attributes
    public bool canCombine = true;

    [HideInInspector]
    public SCR_Player_HandController hand;

    public virtual void CombineObject(string objectID)
    {

    }

    public virtual void UseObject()
    {

    }
}
