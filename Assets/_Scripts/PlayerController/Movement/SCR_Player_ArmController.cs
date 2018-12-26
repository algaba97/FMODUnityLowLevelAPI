using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SCR_Player_HandController))]
public class SCR_Player_ArmController : MonoBehaviour
{
    public enum ACTIONSTATE
    {
        idle,
        grabbing,
        throwing,
        swapping,
        activating,
        combining,
        stopCombining,
        tilting,
        stopTilting,

    }
    public ACTIONSTATE actionstate;
    public bool tiltsideRight;

    public enum PLAYERID
    {
        player1,
        player2,
    }
    public PLAYERID playerid;

    public bool Singleplayer = false;

    private SCR_Player_HandController handController;
    private SCR_Player_HandToHandInteraction handToHandController;
    private SCR_Minecart_Tilt minecartTilt;

    //Public Flags
    public bool GrabbingDisabled;
    public bool ThrowingDisabled;
    public bool SwappingDisabled;
    public bool UsingDisabled;
    public bool combiningDisabled;
    public bool tiltingDisabled;


    //Flags
    private bool inputHeld;
    public bool actionComplete = true;

    public bool ActionComplete
    {
        get
        {
            return actionComplete;
        }

        set
        {
            actionComplete = value;
        }
    }

    void Start()
    {
        handController = GetComponent<SCR_Player_HandController>();
        handController.playerid = playerid;
        handController.armController = this;
        handToHandController = GameObject.FindObjectOfType<SCR_Player_HandToHandInteraction>();

        minecartTilt = GameObject.FindObjectOfType<SCR_Minecart_Tilt>();
    }

    void Update()
    {
        if (minecartTilt == null)
            minecartTilt = GameObject.FindObjectOfType<SCR_Minecart_Tilt>();

        //read input from the player
        if (Singleplayer)
            actionstate = ReadInputSP();
        else
            actionstate = ReadInputMP();

        if (actionComplete)
            switch (actionstate)
            {
                case ACTIONSTATE.idle:          //if the player is idling
                                                //Debug.Log("Idle " + playerid);
                    break;
                case ACTIONSTATE.activating:    //if the player is trying to activate an item
                                                //Debug.Log("Activate " + playerid);
                    break;
                case ACTIONSTATE.grabbing:      //if the player is trying to grab an item
                    handController.GrabObject();
                    //Debug.Log("Grab " + playerid);
                    break;
                case ACTIONSTATE.swapping:      //if the player is trying to swap items between hands
                    handToHandController.ActivateSwap(playerid);
                    //Debug.Log("Swap " + playerid);
                    break;
                case ACTIONSTATE.throwing:      //if the player is trying to throw an item
                    handController.SetupThrowObject();
                    //Debug.Log("Throw " + playerid);
                    break;

                case ACTIONSTATE.combining:      //if the player is trying to combine items in both hands
                    handToHandController.ActivateCombine(playerid);
                    //Debug.Log("combine " + playerid);
                    break;
                case ACTIONSTATE.stopCombining:      //if the player is trying to combine items in both hands
                    handToHandController.DeActivateCombine(playerid);
                    //Debug.Log("combine " + playerid);
                    break;

                case ACTIONSTATE.tilting:      //if the player is trying to tilt the minecart
                    if (minecartTilt != null)
                        minecartTilt.StartTilt(playerid, tiltsideRight);
                    break;

                case ACTIONSTATE.stopTilting:      //if the player has stopped trying to tilt the minecart
                    if (minecartTilt != null)
                        minecartTilt.StartTilt(playerid, tiltsideRight);
                    break;
            }
    }

    //reads the input of the player and calls functions -Singleplayer
    ACTIONSTATE ReadInputSP()
    {
        if (playerid == PLAYERID.player1)
        {
            //inputs that cant be held down
            if (!inputHeld)
            {
                if (Input.GetAxis("Player1_Trigger_Left") > 0.8f && !tiltingDisabled)
                {
                    inputHeld = true;
                    tiltsideRight = false;
                    return ACTIONSTATE.tilting;
                }
                if (Input.GetAxis("Player1_LB") > 0.8f && !GrabbingDisabled)
                {
                    inputHeld = true;
                    return ACTIONSTATE.grabbing;
                }
                if (Input.GetAxis("Player1_LB") > 0.8f && GrabbingDisabled && !ThrowingDisabled)
                {
                    inputHeld = true;
                    return ACTIONSTATE.throwing;
                }

                /* if (Input.GetAxis("Player1_Thumb_Left") > 0.8 && !ThrowingDisabled)     //can only be pressed
                 {
                     inputHeld = true;
                     return ACTIONSTATE.throwing;
                 }*/
                /*if (Input.GetAxis("Player1_DPad_Horizontal") < -0.8f && !tiltingDisabled)
                {
                    inputHeld = true;
                    return ACTIONSTATE.tilting;
                }*/
                if (Input.GetKeyDown("joystick button 1") && !tiltingDisabled)
                {

                    tiltsideRight = false;
                    return ACTIONSTATE.tilting;
                }
                if (Input.GetKeyUp("joystick button 4") && GrabbingDisabled)
                {
                    Debug.Log("let gæu");
                    return ACTIONSTATE.stopCombining;
                }
            }
            else
            {
                if (Input.GetAxis("Player1_Trigger_Left") < 0.1f && Input.GetAxis("Player1_LB") < 0.1f && Input.GetAxis("Player1_Thumb_Left") < 0.1f && Input.GetAxis("Player1_DPad_Horizontal") == 0f)
                    inputHeld = false;
            }
        }
        else
        {
            if (!inputHeld)
            {
                if (Input.GetAxis("Player1_Trigger_Right") > 0.8f && !tiltingDisabled)
                {
                    inputHeld = true;
                    tiltsideRight = true;
                    return ACTIONSTATE.tilting;
                }
                if (Input.GetAxis("Player1_RB") > 0.8f && !GrabbingDisabled)
                {
                    inputHeld = true;
                    return ACTIONSTATE.grabbing;
                }
                if (Input.GetAxis("Player1_RB") > 0.8f && GrabbingDisabled && !ThrowingDisabled)             //can be held
                {
                    inputHeld = true;
                    return ACTIONSTATE.throwing;
                }
                /*if (Input.GetAxis("Player1_Thumb_Right") > 0.8 && !ThrowingDisabled)
                {
                    inputHeld = true;
                    return ACTIONSTATE.throwing;
                }*/

                if (Input.GetKeyDown("joystick button 2") && !tiltingDisabled)
                {

                    tiltsideRight = true;
                    return ACTIONSTATE.tilting;
                }


                if (Input.GetKeyUp("joystick button 5") && GrabbingDisabled && !combiningDisabled)
                {
                    Debug.Log("let go");
                    return ACTIONSTATE.stopCombining;
                }

            }
            else
            {
                if (Input.GetAxis("Player1_Trigger_Right") < 0.1f && Input.GetAxis("Player1_RB") < 0.1f && Input.GetAxis("Player1_Thumb_Right") < 0.1f)
                    inputHeld = false;
            }
        }

        return ACTIONSTATE.idle;
    }

    //reads the input of the player and calls functions -Multiplayer
    ACTIONSTATE ReadInputMP()
    {
        return ACTIONSTATE.idle;
    }
}
