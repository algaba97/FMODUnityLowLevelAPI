using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  PlasticMoose.Minecart;
public class WheelsSound : MonoBehaviour {
	float velocity;
	public GameObject Player;
	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Minecart");
	}
	
	// Update is called once per frame
	void Update () {
		if (Player != null) {
			velocity = Player.GetComponent<Movement> ().CurrentSpeed;
			if (velocity < 0.0f)
				velocity = -velocity;

			if (velocity > 10.0f)
				GetComponent<AudioSource> ().volume = 1.0f;
			else {
				GetComponent<AudioSource> ().volume = (velocity/ 10.0f);
			}
		}
	}
}
