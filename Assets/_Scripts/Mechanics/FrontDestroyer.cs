using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDestroyer : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{   //Destroy everything on the layer DestroyIfFront if it hits the front of the cart.
		if (other.gameObject.layer == 12) 
		{
			Destroy (other.gameObject);
		}
	} 
}
