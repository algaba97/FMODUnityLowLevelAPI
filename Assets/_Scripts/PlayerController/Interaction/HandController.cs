using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public int PlayerID;
    //hand rotation attributes
    public bool HandRotationEnabled;

    //grabing attributes
    public LayerMask GrabMask;
    public float GrabRadius;
    public GameObject hand;

    private PlasticMoose.Minecart.Movement cartMovement;
    private ArmController armController;
    private HandController otherHand;
    private Collider[] avalibleObjects;

    private Vector3 TwoHandVector;

    public bool Singleplayer;
    private bool isGrabbing;
    private bool isThrowing;
    private bool grabOnce;
    private bool letGoOnce;
    private bool stuck;
    private bool Stationary;

    private float throwTimer;

    private Interactable interactable;
    private GameObject heldObject;

	//info showed on screen
	bool shieldInfo = false;
	GameObject sInfo;

    private void Start()
    {
        armController = this.GetComponent<ArmController>();
		sInfo = GameObject.Find ("");
		sInfo.SetActive (false);
        if (armController.PlayerID == 1)
            otherHand = GameObject.Find("PlayerHand_Right").GetComponent<HandController>();
        else
            otherHand = GameObject.Find("PlayerHand_Left").GetComponent<HandController>();


    }

    void Update()
    {
        if (isThrowing)
        {
            throwTimer += Time.deltaTime;
            if (throwTimer >= .5f)
            {
                throwTimer = 0;
                isThrowing = false;
            }
        }
		sInfo.SetActive (shieldInfo);
    }

    private void FixedUpdate()
    {
        //is the arm stuck?
        armController.isStuck = stuck;
        //get a vector between the two hands
        TwoHandVector = otherHand.transform.position - this.transform.position;

        //seek objects to grab and update the avalibleObjects array
        SeekObject();

        if (Singleplayer)
        {

            if (PlayerID == 1)
            {
                SetHandRotation(PlayerID);
                //while the trigger is pressed grab an object
                if (Input.GetAxisRaw("Player1_Triggers") > 0 && grabOnce != true && isThrowing != true && letGoOnce != true)
                {
                    GrabObject();
                }
                if (Input.GetAxisRaw("Player1_Triggers") <= 0)
                {
                    grabOnce = false;
                    letGoOnce = false;
                }
            }
            if (PlayerID == 2)
            {
                SetHandRotation(PlayerID);
                //while the trigger is pressed grab an object
                if (Input.GetAxisRaw("Player1_Triggers") < 0 && grabOnce != true && isThrowing != true && letGoOnce != true)
                {
                    GrabObject();
                }
                if (Input.GetAxisRaw("Player1_Triggers") >= 0)
                {
                    grabOnce = false;
                    letGoOnce = false;
                }
            }
        }
        else
        {

            if (PlayerID == 1)
            {
                SetHandRotation(PlayerID);
                //while the trigger is pressed grab an object
                if (Input.GetAxisRaw("Player1_Triggers") > 0 && grabOnce != true && isThrowing != true && letGoOnce != true)
                {
                    GrabObject();
                }
                if (Input.GetAxisRaw("Player1_Triggers") <= 0)
                {
                    grabOnce = false;
                    letGoOnce = false;
                }
            }
            if (PlayerID == 2)
            {
                SetHandRotation(PlayerID);
                //while the trigger is pressed grab an object
                if (Input.GetAxisRaw("Player2_Triggers") > 0 && grabOnce != true && isThrowing != true && letGoOnce != true)
                {
                    GrabObject();
                }
                if (Input.GetAxisRaw("Player2_Triggers") <= 0)
                {
                    grabOnce = false;
                    letGoOnce = false;
                }
            }
        }


        //if the hand is grabbing somthing
        if (isGrabbing == true)
        {
            if (!stuck)
            {
                //match the hands position and rotation
                heldObject.transform.position = hand.transform.position;
                heldObject.transform.rotation = transform.rotation;


            }

            //call the continious interaction callback on the object
            if (heldObject.GetComponent<Interactable>() != null)
                heldObject.GetComponent<Interactable>().OnInteraction(PlayerID);
        }

        //throw object
        if (isGrabbing == false && heldObject != null && !stuck)
        {
            isThrowing = true;
            if (GameObject.Find("Minecart") != null)
                cartMovement = GameObject.Find("Minecart").GetComponent<PlasticMoose.Minecart.Movement>();
            float cartForce = 1;
            Vector3 dir = Vector3.forward;
            if (cartMovement != null)
            {
                cartForce = cartMovement.CurrentSpeed;
                dir = cartMovement.transform.forward;
            }
            heldObject.GetComponent<Rigidbody>().AddForce(dir * (cartForce / 4) * 500);
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            heldObject.GetComponent<Interactable>().isheld = false;
            heldObject = null;
            armController.UnConstarinPointMovement();
        }

        //let go of the object
        else if (isGrabbing == false && heldObject != null && stuck)
        {
            heldObject.GetComponent<Interactable>().isheld = false;
            heldObject = null;
            stuck = false;
            letGoOnce = true;
            armController.UnConstarinPointMovement();
        }
    }

    //registers the object the player is hovering over
    private void SeekObject()
    {
        avalibleObjects = Physics.OverlapSphere(transform.position, GrabRadius, GrabMask);

    }
	//shows grab info on screen
	private void showGrabInfo(){
		if (avalibleObjects.Length > 0 && !isGrabbing)
		{
			for (int i = 0; i < avalibleObjects.Length; i++)
			{
				Debug.Log ("SHiledorl");
			}
		}
	}
    //grabs the object the player is hovering over
    private void GrabObject()
    {
        if (avalibleObjects.Length > 0 && !isGrabbing)
        {
            for (int i = 0; i < avalibleObjects.Length; i++)
            {
                if (((1 << avalibleObjects[i].gameObject.layer) & GrabMask) != 0)
                {

                    heldObject = avalibleObjects[i].gameObject;
                    if (heldObject.GetComponent<Interactable>() != null)
                    {
                        interactable = heldObject.GetComponent<Interactable>();
                        if (!interactable.isheld)
                        {
                            //is the hand grabbing somthing that cant be moved ?
                            stuck = interactable.Stuck;
                            if (interactable.LimitedMovement)
                                armController.ConstarinPointMovement(interactable.LegalPoints);
                            interactable.OnInitiateInteraction(PlayerID);
                            interactable.isheld = true;
                        }
                        else
                        {
                            heldObject = null;
                            break;
                        }
                    }

                    //if the object interacted with is not stuck
                    if (!stuck)
                    {
                        heldObject.GetComponent<Rigidbody>().useGravity = false;
                        heldObject.transform.parent = hand.transform;
                    }

                    isGrabbing = true;
                    grabOnce = true;
                    break;
                }
            }
        }

        else if (isGrabbing)
        {
            //if the object interacted with is not stuck
            if (!stuck)
            {
                heldObject.transform.parent = null;
                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                heldObject.GetComponent<Rigidbody>().useGravity = true;
            }
            if (heldObject.GetComponent<Interactable>() != null)
            {
                heldObject.GetComponent<Interactable>().OnEndInteraction(PlayerID);
                interactable = null;
            }

            isGrabbing = false;
        }
    }

    private void SetHandRotation(int playerId)
    {
        if (playerId == 1)
        {
            switch ((int)armController.armPos.x)
            {
                case (0):
                    this.transform.localRotation = Quaternion.Euler(0, 0, 90);
                    break;
                case (1):
                    this.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;
                case (2):
                    this.transform.localRotation = Quaternion.Euler(0, 0, 45);
                    break;
                case (3):
                    this.transform.localRotation = Quaternion.Euler(0, 0, -90);
                    break;
            }
        }
        else
        {
            switch ((int)armController.armPos.x)
            {
                case (0):
                    this.transform.localRotation = Quaternion.Euler(0, 0, 90);
                    break;
                case (1):
                    this.transform.localRotation = Quaternion.Euler(180, 0, -180);
                    break;
                case (2):
                    this.transform.localRotation = Quaternion.Euler(0, 180, 45);
                    break;
                case (3):
                    this.transform.localRotation = Quaternion.Euler(0, 0, -90);
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //visualize the grab radious of the sphere
        Gizmos.DrawWireSphere(transform.position, GrabRadius);
    }
}
