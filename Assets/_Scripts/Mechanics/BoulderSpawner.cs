using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawner : MonoBehaviour {

	public GameObject boulder;
	Vector3 spawnSquare;
	Vector3 randomness; 
	float elapsedTime; 
	float timer;

	// Use this for initialization
	void Start () {
		spawnSquare = new Vector3 (transform.parent.position.x,transform.parent.position.y,transform.parent.position.z);
		timer = 2;
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= timer) 
		{
			randomness = new Vector3 (Random.Range (-3, 3), 0, Random.Range (-3, 3));
			Instantiate (boulder, spawnSquare + randomness, Quaternion.identity);
			elapsedTime = 0;
		}
		print (elapsedTime);
	}
}
