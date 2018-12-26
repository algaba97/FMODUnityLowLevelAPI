using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlasticMoose.Minecart 
{
public class LeverController : MonoBehaviour 
	{
	Movement move;
	public GameObject bufferStop;
	Animator anim; 
	public int leverCount;
	public int leversPulled;
	bool stopCart;

	// Use this for initialization
	void Start () {
			stopCart = true;
			move = GameObject.Find ("Minecart").GetComponent<Movement> ();
		anim = bufferStop.GetComponent<Animator> ();
		leverCount = transform.childCount - 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
			//leverCount = 0;
		if (leversPulled >= leverCount) 
		{
				stopCart = false;
			anim.SetBool ("Execute", false);
		}	

		if (stopCart) 
		{
				move.Acceleration = 0;
				move.CurrentSpeed = 0; 
		}

			if (!stopCart) 
			{
				move.Acceleration = 7;
			}

		}
	}
}
