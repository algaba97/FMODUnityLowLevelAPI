using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlasticMoose.Minecart;

public class Interactable_MinecartHandle : Interactable
{
    public Tilting connectedTilt;

    void Start()
    {
        if (connectedTilt == null)
            Debug.LogError("de e nå som mangle henn. Det mangle nå på hennin ->" + this.gameObject.name);
    }

    void Update()
    {

    }

    //called the frame the interaction is initiated
    public override void OnInitiateInteraction(int playerID)
    {
        connectedTilt.IsGrabbed = true;
        connectedTilt.PlayerID = playerID;
    }
    //continiously called while interacting
    public override void OnInteraction(int playerID)
    {

    }
    //called the frame the interaction has ended
    public override void OnEndInteraction(int playerID)
    {
        connectedTilt.IsGrabbed = false;
        connectedTilt.PlayerID = playerID;
    }
}
