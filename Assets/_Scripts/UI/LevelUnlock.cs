using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlock : MonoBehaviour {
	public GameObject unlock;
	public bool locked = true; 
	public int number = -1;
	// Use this for initialization
	void Start () {
		changeunlock (locked);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void changeunlock(bool change) {
		unlock.SetActive (change);
	}
}
