using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Interactable_ChainHandle : SCR_Interaction_Object
{

    public bool isHeld;
	public GameObject PSGlow; 


    void Update()
    {
        if (hand != null)
            isHeld = true;
        else
            isHeld = false;
		
		if (PSGlow != null)
		if (transform.parent.name != "OBJ_Chain_Line") 
		{
			Destroy (PSGlow);
		}

    }

}
