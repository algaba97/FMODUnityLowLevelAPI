using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInfo : MonoBehaviour {
	public float timer = 3f;
	public float auxtimer = 0.0f;
	public GameObject UIImage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (UIImage.activeSelf) {
			auxtimer -= Time.deltaTime;

			if (auxtimer < 0.0f) {
				UIImage.SetActive (false);
			}
		}


	}
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Cart") {
			if (!UIImage.activeSelf) {
				UIImage.SetActive (true);
				auxtimer = timer;
			}
		}
	}

}
