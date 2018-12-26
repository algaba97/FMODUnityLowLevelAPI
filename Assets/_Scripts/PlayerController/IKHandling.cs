using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandling : MonoBehaviour
{

    Animator anim;

    public float IkWeight = 1;

    public Transform IkTarget;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnAnimatorIK()
    {
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, IkWeight);

        anim.SetIKPosition(AvatarIKGoal.LeftHand, IkTarget.position);
    }
}
