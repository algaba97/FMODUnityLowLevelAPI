using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Particle_Glow : MonoBehaviour {

	public string parentName;
	public GameObject psGlow;
	Vector3 startPos;
	bool moveUp;
	public float speed;
	public float floatSpeed;
	// Use this for initialization
	void Start () {
		parentName = transform.parent.name;
		psGlow = transform.GetChild (0).gameObject;
		startPos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		if (psGlow != null) 
		{
			transform.Rotate (Vector3.up * Time.deltaTime * speed);
			if (transform.position.y >= startPos.y + 0.5f)
				moveUp = false;
			else if (transform.position.y <= startPos.y)
				moveUp = true;

			if (moveUp)
				transform.Translate (Vector3.up * Time.deltaTime * floatSpeed);
			else
				transform.Translate (Vector3.down * Time.deltaTime * floatSpeed);
			
			if (transform.parent.name != parentName)
				Destroy (psGlow);


		}
		
	}
}
