using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geometry : MonoBehaviour
{
     public FMODEngine FMODengine;
    // Start is called before the first frame update
    public float directOclusion;
    public float reberOclusion;
    public Transform[] transforms;
    void Start()
    {

        FMOD.VECTOR[] verts = new FMOD.VECTOR[transforms.Length];
        
        for(int i = 0; i <= transforms.Length - 1; i++)
        {
           
            verts[i].x = transforms[i].position.x;
            verts[i].y = transforms[i].position.y;
            verts[i].z = transforms[i].position.z;

        }

       FMODengine.createGeometry(directOclusion, reberOclusion, verts);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
