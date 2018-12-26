using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leveling : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (GameObject.Find ("GameManager").GetComponent<GameManager> ().singleplayer) {
			gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
