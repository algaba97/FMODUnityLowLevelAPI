using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {
    public FMODEngine FMODengine;
    public List<string> cadena;
    public List<bool> loop;
    // Use this for initialization
    void Start () {
		for(int i = 0; i< cadena.Count; i++)
        {
            FMODengine.CreateSoundPlayer(cadena[i], loop[i]);
            if (loop[i])
            {
                FMODengine.PlayPlayer(i);

            }
        }
	}
	
	// Update is called once per frame
	public void Play (int number) {

        FMODengine.PlayPlayer(number);
    }
}
