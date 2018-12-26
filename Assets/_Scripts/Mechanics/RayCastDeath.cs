using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RayCastDeath : MonoBehaviour 
{
	Vector3 fwd;
	public LayerMask mask;
	public bool CollideWithBody,CollideWithCart;

	// Use this for initialization
	void Start () {
		fwd = transform.TransformDirection (Vector3.forward);
	}
	
	// Update is called once per frame
	void Update () 
	{
		RaycastHit hit;
		if (Physics.Raycast (transform.position, fwd, out hit, 5,mask)) 
		{
			if (CollideWithBody) 
			{
				if (hit.collider.gameObject.tag == "Player")
					EventManager.instance.TriggerEvent ("EVT_Player_TriggerDeath");
				return;
			}

			if (CollideWithCart) 
			{
				if (hit.collider.gameObject.tag == "Cart")
					EventManager.instance.TriggerEvent ("EVT_Player_TriggerDeath");
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay (transform.position, fwd * 5);
	}
}
