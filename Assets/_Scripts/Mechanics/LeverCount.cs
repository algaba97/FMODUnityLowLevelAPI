using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlasticMoose.Minecart{
public class LeverCount : MonoBehaviour {

	LeverController lever;
		Animator anim;
		AudioSource crankSound;
		public GameObject startLights;
		StartLightController startLightSC;


	void Start()
	{
			anim = GetComponent<Animator> ();
			crankSound = GetComponent<AudioSource> ();
			lever = transform.parent.parent.GetComponent<LeverController> ();
			startLightSC = startLights.GetComponent<StartLightController> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Hand") 
		{
			lever.leversPulled++;
				anim.SetBool ("Crank", true);
				crankSound.Play ();
				gameObject.GetComponent<BoxCollider> ().enabled = false;
				startLightSC.nextLight++;
				startLightSC.triggerActivated = true; 
		}
	}
}
}
