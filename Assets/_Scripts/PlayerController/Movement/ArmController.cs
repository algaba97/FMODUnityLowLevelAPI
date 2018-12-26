using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public int PlayerID;
    public float HandSpeed = 3.0f;

    public Vector3 Vel_P1;
    public Vector3 Vel_P2;

    //single player velocity
    private Vector3 vel_Left;
    private Vector3 vel_Right;

    public float constraintThres;
    public LayerMask contraintMask;

    private Rigidbody playerRigid;
    public bool isStuck;

    //Layered snap approach
    public bool LayeredSnapMovement;
    public bool verySimpleMovement;
    public Transform[][] Points = new Transform[4][];

    public Transform TopLayer;
    public Transform TopMidLayer;
    public Transform BotMidLayer;
    public Transform BotLayer;

    private List<Vector2> legalPoints;
    public Vector2 armPos;
    private Vector2 armPosOld;

    private float r_Analog_threshold = 0.40f;
    private float armDelay = 0.1f;
    private float rotationAngle;
    private float rotationAngleOld;
    public int layerIndex = 1;

    //flag
    public bool SinglePlayer;
    public bool FourAxisControll = true;
    private bool constrainedPointMovement;
    private bool layerSwitched;
    private bool movingArm;
    private bool bumperHeld;
    //cooldown
    private float layerSwitchCooldown = 0.25f;
    //timer
    private float layerSwitchTimer = 0f;

    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();

        if (verySimpleMovement)
        {
            LayeredSnapMovement = false;
            Points = new Transform[3][];

            Points[2] = TopMidLayer.GetComponentsInChildren<Transform>();
            Points[1] = BotMidLayer.GetComponentsInChildren<Transform>();
            Points[0] = BotLayer.GetComponentsInChildren<Transform>();
        }

        if (LayeredSnapMovement)
        {
            Points[1] = BotMidLayer.GetComponentsInChildren<Transform>();
            Points[0] = BotLayer.GetComponentsInChildren<Transform>();
        }
    }

    void FixedUpdate()
    {
        if (!isStuck)
        {
            if (SinglePlayer)
            {
                vel_Left = new Vector3(-Input.GetAxisRaw("Player1_Horizontal_Left"), 0, Input.GetAxis("Player1_Vertical_Left"));
                vel_Right = new Vector3(Input.GetAxisRaw("Player1_Horizontal_Right"), 0, Input.GetAxis("Player1_Vertical_Right"));

                if (PlayerID == 1 && vel_Left != Vector3.zero)
                {
                    SnapToPointMove(vel_Left);
                }
                else if (PlayerID == 2 && vel_Right != Vector3.zero)
                {
                    SnapToPointMove(vel_Right);
                }

                if (PlayerID == 1 && Input.GetAxis("player1_LB") > 0.9f && !bumperHeld)
                {
                    //toggle layer right
                    if (layerIndex == 2)
                        MoveToLayer(-1);
                    else if (layerIndex == 1)
                        MoveToLayer(1);

                    bumperHeld = true;
                }
                if (PlayerID == 2 && Input.GetAxis("player1_RB") > 0.9f && !bumperHeld)
                {
                    //toggle layer left
                    if (layerIndex == 2)
                        MoveToLayer(-1);
                    else if (layerIndex == 1)
                        MoveToLayer(1);

                    bumperHeld = true;
                }

                if (PlayerID == 1 && Input.GetAxis("player1_LB") < 0.1f)
                {
                    bumperHeld = false;
                }
                if (PlayerID == 2 && Input.GetAxis("player1_RB") < 0.1f)
                {
                    bumperHeld = false;
                }
            }
            else
            {

                Vel_P1 = new Vector3(-Input.GetAxisRaw("Player1_Horizontal_Left"), Input.GetAxis("Player1_Vertical_Right"), Input.GetAxisRaw("Player1_Vertical_Left"));
                Vel_P2 = new Vector3(Input.GetAxisRaw("Player2_Horizontal_Left"), Input.GetAxis("Player2_Vertical_Right"), Input.GetAxisRaw("Player2_Vertical_Left"));

                if (verySimpleMovement)
                {
                    if (PlayerID == 1 && Vel_P1 != Vector3.zero)
                    {
                        SnapToPointMove(Vel_P1);

                    }
                    else if (PlayerID == 2 && Vel_P2 != Vector3.zero)
                    {
                        SnapToPointMove(Vel_P2);
                    }
                }
                else if (LayeredSnapMovement)
                {
                    if (PlayerID == 1 && Vel_P1 != Vector3.zero)
                    {
                        SnapToPointMove(Vel_P1);
                    }
                    else if (PlayerID == 2 && Vel_P2 != Vector3.zero)
                    {
                        SnapToPointMove(Vel_P2);
                    }
                }
                else
                {
                    if (PlayerID == 1 && Input.GetAxis("Player1_Triggers") >= 0)
                    {
                        MovePhysics(Vel_P1);
                    }
                    else if (PlayerID == 2 && Input.GetAxis("Player2_Triggers") >= 0)
                    {
                        MovePhysics(Vel_P2);
                    }
                }
            }
        }

        if (layerSwitched)
        {
            layerSwitchTimer += Time.deltaTime;
            if (layerSwitchTimer >= layerSwitchCooldown)
            {
                layerSwitched = false;
                layerSwitchTimer = 0;
            }
        }
    }
    void MovePhysics(Vector3 velocity)
    {
        playerRigid.AddRelativeForce(velocity * HandSpeed, ForceMode.Impulse);
    }

    //interpolate between the two points
    void SnapToPointMove(Vector3 input)
    {
        Vector3 point = Vector3.zero;
        float X = input.x;
        float Z = input.z;
        float Y = input.y;

        //Debug.Log(Y);
        rotationAngle = Mathf.Atan2(Z, X) * 180 / Mathf.PI;
        float absRotationAngle = Mathf.Abs(rotationAngle);

        if (Mathf.Abs(X) < r_Analog_threshold && Mathf.Abs(Z) < r_Analog_threshold)
        {
            rotationAngle = rotationAngleOld;
        }
        else if (Mathf.Abs(X) > r_Analog_threshold || Mathf.Abs(Z) > r_Analog_threshold)
        {
            rotationAngleOld = rotationAngle;
        }

        if (Y > 0.8f && !layerSwitched)
        {
            MoveToLayer(1);

        }
        else if (Y < -0.8f && !layerSwitched)
        {
            MoveToLayer(-1);

        }
        //dont run unless there is input
        if (X != 0 || Z != 0)
        {
            if (FourAxisControll)
            {
                //top layer is only one point
                if (layerIndex == 2)
                {
                    StartCoroutine(IMoveBetweenPoints(transform.localPosition, Points[layerIndex][1], armDelay));
                    return;
                }
                //Front
                else if (rotationAngle <= 150f && rotationAngle > 75f)
                {
                    StartCoroutine(IMoveBetweenPoints(transform.localPosition, Points[layerIndex][4], armDelay));
                }

                //FrontMid
                // else if (rotationAngle <= 75f && rotationAngle > 0f)
                //    StartCoroutine(IMoveBetweenPoints(transform.localPosition, Points[layerIndex][3], armDelay));

                //BackMid
                else if (rotationAngle <= 0f && rotationAngle > -75f)
                    StartCoroutine(IMoveBetweenPoints(transform.position, Points[layerIndex][2], armDelay));

                //Back
                //  else if (rotationAngle <= -75f && rotationAngle > -150f)
                //      StartCoroutine(IMoveBetweenPoints(transform.position, Points[layerIndex][1], armDelay));

                //Root pos
                else if (rotationAngle <= -150f || rotationAngle > 150f)
                    StartCoroutine(IMoveBetweenPoints(transform.position, Points[layerIndex][0], armDelay));
            }

            //Two axis controll
            else
            {
                //Bottom layer is only one point
                if (layerIndex == 0)
                {

                    ChangePosition(layerIndex, 0);

                    return;

                }
                //Front
                else if (rotationAngle <= 150f && rotationAngle > 45f)
                {
                    ChangePosition(layerIndex, 4);
                }

                //BackMid
                else if (rotationAngle <= 45f && rotationAngle > -90f)
                {

                    ChangePosition(layerIndex, 2);
                }

                //Root pos
                else if (rotationAngle <= -90f || rotationAngle > 150f)
                {

                    ChangePosition(layerIndex, 0);
                }
            }
        }
    }

    private void ChangePosition(int layerNum, int posNumber)
    {
        int pos = posNumber;
        if (layerNum == 0)
            pos = 0;

        if (constrainedPointMovement)
        {
            if (!legalPoints.ToList().Any(a => a == new Vector2(layerNum, posNumber)))
            {
                return;
            }
        }

        StartCoroutine(IMoveBetweenPoints(transform.position, Points[layerNum][pos], armDelay));
        armPos = new Vector2(layerNum, pos);
    }

    void MoveToLayer(int dir)
    {

        if (constrainedPointMovement)
        {

            Vector2? closestLegalPoint = GetClosestLegalPoint(dir);

            if (closestLegalPoint == null)
            {
                return;
            }

            layerIndex = (int)closestLegalPoint.Value.x;

            ChangePosition(layerIndex, (int)closestLegalPoint.Value.y);
        }
        else
        {
            layerIndex = Mathf.Clamp(layerIndex + dir, 1, 2);
            ChangePosition(layerIndex, (int)armPos.y);
        }
        layerSwitched = true;

    }

    private Vector2? GetClosestLegalPoint(int v)
    {
        int layerSeek = layerIndex;
        while (layerSeek < 4 && layerSeek >= 0)
        {
            layerSeek += v;

            var allLegalPointsInLayer = legalPoints.Where(point => point.x == layerSeek);

            if (!allLegalPointsInLayer.Any())
            {
                break;
            }

            return allLegalPointsInLayer.OrderBy(a => Vector3.Distance(Points[layerSeek][(int)a.y].position, transform.position)).First();
        }
        return null;
    }

    public void ConstarinPointMovement(Vector2[] _legalPoints)
    {
        legalPoints = _legalPoints.ToList();
        constrainedPointMovement = true;

    }
    public void UnConstarinPointMovement()
    {
        legalPoints = null;
        constrainedPointMovement = false;
    }

    //contstrain the movement of the hand
    void Contstrain(ref Vector3 velocity)
    {
        int[] dir = new int[3];
        dir[0] = 1;
        dir[1] = 1;
        dir[2] = 1;

        if (velocity.x < 0)
            dir[0] = -1;

        if (velocity.y < 0)
            dir[1] = -1;

        if (velocity.z < 0)
            dir[2] = -1;

        //raycast x,y,z depending on velocity
        Ray[] rays = new Ray[3];
        rays[0] = new Ray(this.transform.position, new Vector3(dir[0], 0, 0));
        rays[1] = new Ray(this.transform.position, new Vector3(0, dir[1], 0));
        rays[2] = new Ray(this.transform.position, new Vector3(0, 0, dir[2]));
        //check collition and constrain movement
        for (int i = 0; i < 3; i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(rays[i], out hit, constraintThres, contraintMask))
            {
                //constrain the velocity in the direction of the obstruction
                if (i == 0 && Mathf.Sign(velocity.x) == Mathf.Sign(dir[i]))
                    velocity = new Vector3(0, velocity.y, velocity.z);

                if (i == 1 && Mathf.Sign(velocity.y) == Mathf.Sign(dir[i]))
                    velocity = new Vector3(velocity.x, 0, velocity.z);

                if (i == 2 && Mathf.Sign(velocity.z) == Mathf.Sign(dir[i]))
                    velocity = new Vector3(velocity.x, velocity.y, 0);
            }
        }
    }

    IEnumerator IMoveBetweenPoints(Vector3 pointA, Transform pointB, float time)
    {
        if (!movingArm)
        {                     // Do nothing if already moving
            movingArm = true;                 // Set flag to true
            float t = 0f;
            while (t < 1.0f)
            {
                t += Time.deltaTime / time; // Sweeps from 0 to 1 in time seconds
                transform.position = Vector3.Lerp(transform.position, pointB.position, t); // Set position proportional to t
                yield return new WaitForEndOfFrame();         // Leave the routine and return here in the next frame
            }
            movingArm = false;             // Finished moving
        }
    }

    void OnDrawGizmos()
    {
        Ray rayX = new Ray(this.transform.position, new Vector3(constraintThres, 0, 0));
        Ray rayY = new Ray(this.transform.position, new Vector3(0, constraintThres, 0));
        Ray rayZ = new Ray(this.transform.position, new Vector3(0, 0, constraintThres));

        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayX);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(rayY);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(rayZ);
    }
}