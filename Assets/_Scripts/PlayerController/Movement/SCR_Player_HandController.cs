using System.Collections;
using System.Collections.Generic;
using PlasticMoose.Minecart;
using UnityEngine;

public class SCR_Player_HandController : MonoBehaviour
{
    public SCR_Player_ArmController armController;
    public Transform HandTransform;
    public Transform PlayerTransform;
    public SCR_Player_ArmController.PLAYERID playerid;
    public LayerMask objectLayer;
    public GameObject avalibleObject;

    public float grabRadius = 1f;

    private bool isThrowing;
    private Movement cartMovement;

    private GameObject heldObject;
    private SCR_Interaction_Object heldObjectInteract;
    private Rigidbody heldObjectRigid;

    //CoolDown
    private bool onCooldown;
    private float grabCooldown = 1.1f;
    private float cooldownTime;

    //Anim timing
    public float GrabTiming;
    public float ThrowTiming;

    //Flags
    private bool grab;
    private bool throwing;
    private bool holdingItem;

    //info showed on screen
    bool shieldInfo = false;
    GameObject sInfo;

    public GameObject LT;
    public GameObject RT;


    public GameObject HeldObject
    {
        get
        {
            return heldObject;
        }

        set
        {
            heldObject = value;
        }
    }
    public bool HaveIGotATorch()
    {
        if (heldObject == null)
            return false;
        else
            return heldObject.GetComponent<SCR_Interaction_Object>().ID == "Torch";
    }

    void Start()
    {
		try {
			LT = GameObject.Find("LTImage");
			LT.SetActive(false);
			RT = GameObject.Find("RTImage");
			RT.SetActive(false);
		}
		catch{
		}

    }

    void Update()
    {
        //Collider[] AvalibleObjects = Physics.OverlapSphere(transform.position, grabRadius, objectLayer);
        Collider[] AvalibleObjects = Physics.OverlapBox(gameObject.transform.position, new Vector3(transform.localScale.x, transform.localScale.y * 1.5f, transform.localScale.z * 6), gameObject.transform.rotation, objectLayer);

        for (int i = 0; i < AvalibleObjects.Length; i++)
        {
            if (AvalibleObjects[i].GetComponent<SCR_Interaction_Object>() != null)
            {
                if (AvalibleObjects[i].GetComponent<SCR_Interaction_Object>().avalible)
                {
                    if (playerid == SCR_Player_ArmController.PLAYERID.player1)
                    {
                        if (LT != null)
                            LT.SetActive(true);
                    }
                    else
                    {
                        if (RT != null)
                            RT.SetActive(true);
                    }
                    avalibleObject = AvalibleObjects[i].gameObject;
                    // if you are near to a shield

                    break;

                }
				else
                {
                    if (playerid == SCR_Player_ArmController.PLAYERID.player1)
                    {
                        if (LT != null)
                            LT.SetActive(false);
                    }
                    else
                    {
                        if (RT != null)
                            RT.SetActive(false);
                    }
                    avalibleObject = null;
                }
            }
            else
				
                avalibleObject = null;
        }
		if (AvalibleObjects.Length == 0) {
			if (LT != null) {
				LT.SetActive (false);
				RT.SetActive (false);
			}
		}
	
				

        if (holdingItem && heldObject != null)
        {

            heldObject.transform.parent = HandTransform;
            heldObject.transform.localPosition = Vector3.zero;
            heldObject.transform.rotation = HandTransform.rotation;

        }

        if (AvalibleObjects.Length == 0)
            avalibleObject = null;

        if (onCooldown)
        {
            cooldownTime += Time.deltaTime;

            if (cooldownTime >= grabCooldown)
            {

                onCooldown = false;
                cooldownTime = 0;
            }
        }

    }

    public void GrabObject()
    {
        if (!onCooldown)
        {

            onCooldown = true;
            Debug.Log("call me");



            if (avalibleObject != null && heldObject == null)
            {
                armController.ActionComplete = false;
                armController.GrabbingDisabled = true;

                heldObject = avalibleObject;
                heldObjectInteract = heldObject.GetComponent<SCR_Interaction_Object>();
                heldObjectRigid = heldObject.GetComponent<Rigidbody>();
                heldObjectInteract.avalible = false;
                armController.combiningDisabled = !heldObjectInteract.canCombine;

                if (playerid == SCR_Player_ArmController.PLAYERID.player1)
                    EventManager.instance.TriggerEvent("Visual_Player_GrabLeft");
                else
                    EventManager.instance.TriggerEvent("Visual_Player_GrabRight");

                Invoke("MoveObject", GrabTiming);
            }
            else
            {
                if (playerid == SCR_Player_ArmController.PLAYERID.player1)
                    EventManager.instance.TriggerEvent("Visual_Player_GrabLeft");
                else
                    EventManager.instance.TriggerEvent("Visual_Player_GrabRight");

                if (playerid == SCR_Player_ArmController.PLAYERID.player1)
                    EventManager.instance.TriggerEvent("Visual_Player_FailGrabLeft");
                else
                    EventManager.instance.TriggerEvent("Visual_Player_FailGrabRight");
            }
        }
    }


    //setsup the grab object and moves it to the player hand
    void MoveObject()
    {


        heldObject.transform.parent = HandTransform;

        heldObjectInteract.hand = this;
        heldObjectRigid.useGravity = false;
        heldObjectRigid.velocity = Vector3.zero;

        armController.ActionComplete = true;

        holdingItem = true;
    }

    public void SetupThrowObject()
    {
        if (heldObject != null && !throwing)
        {
            throwing = true;

            Invoke("ExecuteThrowObject", ThrowTiming);
            if (playerid == SCR_Player_ArmController.PLAYERID.player1)
                EventManager.instance.TriggerEvent("Visual_Player_ThrowLeft");
            else
                EventManager.instance.TriggerEvent("Visual_Player_ThrowRight");
        }
    }

    private void ExecuteThrowObject()
    {
        if (GameObject.Find("Minecart") != null)
            cartMovement = GameObject.Find("Minecart").GetComponent<PlasticMoose.Minecart.Movement>();
        float cartForce = 4;
        Vector3 dir = PlayerTransform.forward;
        if (cartMovement != null)
        {
            cartForce = cartMovement.CurrentSpeed;
            dir = cartMovement.transform.forward;
        }
        heldObjectRigid.AddForce(dir * (cartForce / 4) * 500);
        heldObjectRigid.useGravity = true;
        heldObjectInteract.hand = null;
        armController.combiningDisabled = false;
        //heldObject.GetComponent<SCR_Interaction_Object>().avalible = true;    //enable if player should be able to pick up the object he just threw away
        heldObject.transform.parent = null;
        heldObject = null;
        armController.GrabbingDisabled = false;
        holdingItem = false;
        throwing = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (avalibleObject != null)
            Gizmos.DrawLine(this.transform.position, avalibleObject.transform.position);

        Gizmos.color = Color.red;

        //Gizmos.DrawWireSphere(HandTransform.position, grabRadius);
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.localScale.x, transform.localScale.y * 1.5f, transform.localScale.z * 6));
        //Gizmos.Draw

    }
}
