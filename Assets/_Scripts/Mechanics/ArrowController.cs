using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlasticMoose.Minecart;

public class ArrowController : MonoBehaviour {

	public float speed;
	public float lifeTime;
	public bool collideWithBody, collideWithCart;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.parent == null)
		transform.Translate (Vector3.forward * Time.deltaTime * speed);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Shield") {
			EventManager.instance.TriggerEvent ("EVT_SoundEvent_Arrow_Wood");
			transform.parent = col.transform;
			GetComponent<Rigidbody> ().isKinematic = true;
		}
		if (collideWithBody) 
		{
			if (col.gameObject.tag == "Player")
				EventManager.instance.TriggerEvent ("EVT_Player_TriggerDeath");
			return;
		}
		if (collideWithCart) 
		{
			if (col.gameObject.tag == "Cart")
				EventManager.instance.TriggerEvent ("EVT_Player_TriggerDeath");
		}
	}
}
