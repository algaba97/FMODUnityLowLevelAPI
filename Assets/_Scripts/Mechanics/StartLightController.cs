using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLightController : MonoBehaviour {

	public GameObject[] lights;
	public Material green;
	public bool triggerActivated;
	public int nextLight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (triggerActivated) 
		{
			lights [nextLight -1].GetComponent<Renderer> ().material = green;
			triggerActivated = false;
		}

	}
}
