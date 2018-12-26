using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaController : MonoBehaviour {

	public Animator anim;
	public GameObject extendShaft;
	public bool isgrabed;
	Rigidbody rb;
	bool forceAdded;

	void Start () {
		anim = GameObject.Find ("FoldingPart").GetComponent<Animator> ();
		//Stops the animation from playing OnAwake
		anim.speed = 0;
		rb = extendShaft.GetComponent<Rigidbody> ();
	}
	
	void Update () {

		if (transform.parent != null)
			isgrabed = true;

		if (isgrabed) {
			extendShaft.SetActive (true);

			if (!forceAdded) {
				rb.AddForce (Vector3.back * 50);
				forceAdded = true;
			}
		}
		

		anim.Play ("UmbrellaUnfold", 0, -extendShaft.transform.localPosition.y * 0.8f - 0.5f);
	}
}
