using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public int numberofLevels = 3;
	public int level = 3;
	public int music = 1; // 1 == active , -1 == inactive
	 GameObject SndL;
	public bool singleplayer = true;
	public List<float> timelevels;


	// Use this for initialization
	void Start () {
		SndL =	GameObject.Find ("SaveandLoad");
		for (int i = 0; i < numberofLevels; i++) {
			timelevels.Add (99.0f);
		}
		Load ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void Load() {
		SndL.GetComponent<SaveandLoad> ().LoadFile ();
	}

	public void Save() {
		SndL.GetComponent<SaveandLoad> ().SaveFile ();
	}
}
