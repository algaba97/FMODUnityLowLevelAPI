using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacheteController : MonoBehaviour 
{

	void OnTriggerEnter( Collider other)
	{
		//this does some stuff
		if (other.tag == ("Destructible"))
		{
			GetComponent<MeshRenderer> ().enabled = false;
			transform.GetChild (0).gameObject.SetActive (true);
			GetComponent<BoxCollider> ().enabled = false;
			Destroy(gameObject, 3);
		}

		if (other.tag == ("Player"))
		{
			GetComponent<MeshRenderer> ().enabled = false;
			transform.GetChild (0).gameObject.SetActive (true);
			EventManager.instance.TriggerEvent ("EVT_Player_TriggerDeath");
			Destroy(gameObject, 3);
		}
	}
}