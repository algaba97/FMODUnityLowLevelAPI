using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSound : MonoBehaviour {
    public FMODEngine FMODengine;
    public bool loop = true;
    public string cadena = "null";//ejemplo lalalal.wav
   int numero;
	// Use this for initialization
	void Start () {
        if (cadena != "null")
        {
            numero = FMODengine.CreateSound(cadena, loop);
            if (loop) FMODengine.Play(numero);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(cadena != "null" )
        {
           FMODengine.SetPosition(numero, transform.position.x, transform.position.y, transform.position.z);
        }
	}

    public void Play()
    {
        FMODengine.Play(numero);
    }
    
}
