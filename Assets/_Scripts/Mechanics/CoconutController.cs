using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlasticMoose.Minecart;

public class CoconutController : MonoBehaviour {

	public GameObject psLeaf;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			EventManager.instance.TriggerEvent ("EVT_Player_TriggerDeath");
			psLeaf.SetActive (true);
			Destroy (gameObject);
		}

		if (other.tag == "Destructible") 
		{
			psLeaf.SetActive (true);
			Destroy (gameObject);
		}

	}

}
