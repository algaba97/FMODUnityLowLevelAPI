using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Visual_DynamicChainLine : MonoBehaviour
{
    public Transform handleTransform;
    public Transform baseTransform;

    LineRenderer lineR;
    void Start()
    {
        lineR = this.GetComponent<LineRenderer>();
    }

    void Update()
    {
        lineR.SetPosition(0, handleTransform.position);
        lineR.SetPosition(1, baseTransform.position);
    }
}
