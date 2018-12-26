using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePlayer : MonoBehaviour {
	bool done = false;
	// Use this for initialization
	void Start () {
		
		Place ();

	}
	
	// Update is called once per frame
	void Update () {
		if (!done)
			Place ();
	}
	void Place(){
			Transform player = GameObject.Find("Player_IndirectMovement").transform;

		if (player != null)
		{

			//place the player in the minecart
			GameObject playerPos = GameObject.Find("PlayerPos");
			if (playerPos != null)
			{
				Debug.Log ("in");
				player.position = playerPos.transform.position;
				player.rotation = playerPos.transform.rotation;
				player.parent = playerPos.transform;
				done = true;
			}
		}
	}

}
