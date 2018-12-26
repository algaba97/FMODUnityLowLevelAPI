using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //general attributes
    public string Name = "";
    public int Id = -1;

    public bool Stuck;
    public bool LimitedMovement;
    public bool isheld;
    public Vector2[] LegalPoints = new Vector2[0];

    private void Start()
    {
        if (name == "")
            name = gameObject.name;

        if (Stuck)
            LimitedMovement = false;

    }

    //called the frame the interaction is initiated
    public abstract void OnInitiateInteraction(int PlayerID);
    //continiously called while interacting
    public abstract void OnInteraction(int PlayerID);
    //called the frame the interaction has ended
    public abstract void OnEndInteraction(int PlayerID);

}
