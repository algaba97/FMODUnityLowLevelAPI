using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverb : MonoBehaviour
{
    public FMODEngine FMODengine;
    public float min;
    public float max;
    public bool desactivar;
    // Start is called before the first frame update
    void Start()
    {
        FMODengine.CreateReverb3d(min, max);
        desactivar = false;
    }

    // Update is called once per frame
    void Update()
    {
       if(!desactivar) FMODengine.setRever3d(min,max, transform.position.x, transform.position.y, transform.position.z);
       else FMODengine.setRever3d(0,0,0,0,0);

    }
}
