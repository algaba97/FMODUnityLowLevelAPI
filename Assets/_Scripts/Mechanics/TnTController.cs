using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlasticMoose.Minecart;

public class TnTController : MonoBehaviour {

	public GameObject explotion;
	public GameObject fuseFlame;

	bool isLit;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Torch") 
		{
		isLit = true;
			fuseFlame.SetActive (true);
	    }
 	}

	void OnCollisionEnter(Collision col)
	{
		if (isLit && col.gameObject.tag == "Destructible") 
		{
			Destroy (col.gameObject);
			EventManager.instance.TriggerEvent ("EVT_SoundEvent_Bomb_Explode");
			Instantiate (explotion, transform.position, transform.rotation);


			Destroy (gameObject, 1f);
		}

	}
}

