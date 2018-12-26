using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlasticMoose.Minecart;

public class Deathtrigger : MonoBehaviour {

	public bool CollideWithBody,CollideWithCart;
	public GameObject psSmoke;
	public MeshRenderer rend;

	void OnTriggerEnter(Collider other)
	{

		if (CollideWithBody) {
			if (other.tag == "Player") {
				print ("Collided with player");
				EventManager.instance.TriggerEvent ("EVT_Player_TriggerDeath");

				if (psSmoke != null) 
				{
					psSmoke.SetActive (true);
					rend.enabled = false;
				}
			}
		}
		if (CollideWithCart){
			if (other.tag == "Cart") {
				print ("Collided with cart");
			
				EventManager.instance.TriggerEvent ("EVT_Player_TriggerDeath");

				if (psSmoke != null) 
				{
					psSmoke.SetActive (true);
					rend.enabled = false;
				}
			}
		}
	}
}