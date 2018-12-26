using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Player_HandToHandInteraction : MonoBehaviour
{
    public SCR_Player_HandController LeftHand;
    public SCR_Player_HandController RightHand;

    private bool SwapLeft;
    private bool SwapRight;

    public bool combineLeft;
    public bool combineRight;

    private bool hasSwaped;
    public bool hasCombined;

    //Flag
    bool LeftDelayComp = false;
    bool RightDelayComp = false;

    //Timer
    float leftDelayTimer = 0f;
    float RightDelayTimer = 0f;

    void Update()
    {
        if (SwapLeft && SwapRight && !hasSwaped)
            SwapObjects();

        SwapLeft = false;
        SwapRight = false;

        //trigger events for combine visual
        if (LeftDelayComp && RightDelayComp && !hasCombined)
            StartCombineObjects();



        if (combineLeft)
        {
            leftDelayTimer += Time.deltaTime;
            if (leftDelayTimer > 0.458f)
                LeftDelayComp = true;
        }
        else
        {
            leftDelayTimer = 0;
            LeftDelayComp = false;
        }
        if (combineRight)
        {
            RightDelayTimer += Time.deltaTime;
            if (RightDelayTimer > 0.458f)
                RightDelayComp = true;
        }
        else
        {
            RightDelayTimer = 0;
            RightDelayComp = false;
        }

        if (hasCombined)
        {
            combineLeft = false;
            combineRight = false;

            RightDelayTimer = 0;
            leftDelayTimer = 0;

            EventManager.instance.TriggerEvent("Visual_Player_StopCombineLeft");
            EventManager.instance.TriggerEvent("Visual_Player_StopCombineRight");
        }
    }

    //Swaps object between the two hands
    void SwapObjects()
    {
        GameObject LeftObject = LeftHand.HeldObject;
        GameObject RightObject = RightHand.HeldObject;

        if (LeftObject != null)
            LeftObject.transform.position = RightHand.transform.position;

        if (RightObject != null)
            RightObject.transform.position = LeftHand.transform.position;

        LeftHand.HeldObject = RightObject;
        RightHand.HeldObject = LeftObject;

        LeftObject = null;
        RightObject = null;

        hasSwaped = true;
        StartCoroutine(Cooldown());
    }

    //start combining objects, trigger events
    private void StartCombineObjects()
    {
        if (LeftHand.HeldObject != null && RightHand.HeldObject != null)
        {

            EventManager.instance.TriggerEvent("Visual_Player_Combine");
            hasCombined = true;
            Invoke("CombineObject", 0.3f);
        }
    }

    //Combine Two Objects functions
    private void CombineObject()
    {

        SCR_Interaction_Object LeftObject = LeftHand.HeldObject.GetComponent<SCR_Interaction_Object>();
        SCR_Interaction_Object RightObject = RightHand.HeldObject.GetComponent<SCR_Interaction_Object>();

        if (!LeftObject.combined || !RightObject.combined)
        {
            RightObject.CombineObject(LeftObject.ID);
            LeftObject.CombineObject(RightObject.ID);

            StartCoroutine(Cooldown());
        }
    }

    //SWAP activators
    public void ActivateSwap(SCR_Player_ArmController.PLAYERID playerID)
    {
        if (playerID == SCR_Player_ArmController.PLAYERID.player1)
            SwapLeft = true;

        if (playerID == SCR_Player_ArmController.PLAYERID.player2)
            SwapRight = true;

    }

    //COMBINE activators
    public void ActivateCombine(SCR_Player_ArmController.PLAYERID playerID)
    {
        if (playerID == SCR_Player_ArmController.PLAYERID.player1 && !LeftHand.HeldObject.GetComponent<SCR_Interaction_Object>().combined)
        {
            EventManager.instance.TriggerEvent("Visual_Player_CombineLeft");
            combineLeft = true;
        }

        if (playerID == SCR_Player_ArmController.PLAYERID.player2 && !RightHand.HeldObject.GetComponent<SCR_Interaction_Object>().combined)
        {
            EventManager.instance.TriggerEvent("Visual_Player_CombineRight");
            combineRight = true;
        }
    }

    public void DeActivateCombine(SCR_Player_ArmController.PLAYERID playerID)
    {
        if (playerID == SCR_Player_ArmController.PLAYERID.player1)
        {
            EventManager.instance.TriggerEvent("Visual_Player_StopCombineLeft");
            combineLeft = false;
            hasCombined = false;
        }

        if (playerID == SCR_Player_ArmController.PLAYERID.player2)
        {
            EventManager.instance.TriggerEvent("Visual_Player_StopCombineRight");
            combineRight = false;
            hasCombined = false;
        }


    }

    //utility
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        hasSwaped = false;
    }

    IEnumerator LeftCombineDelay()
    {
        yield return new WaitForSeconds(0.458f);
        combineLeft = true;
    }

    IEnumerator RightCombineDelay()
    {
        yield return new WaitForSeconds(0.458f);
        combineRight = true;
    }

    private void ToggleBool(ref bool _bool)
    {
        _bool = !_bool;
    }

    private void ToggleBool(ref bool _bool, bool forcedVal)
    {
        _bool = forcedVal;
    }
}
