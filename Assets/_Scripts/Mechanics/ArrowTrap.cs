using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour {

	public GameObject arrow;
	public Transform[] spawnPoints;
	private IEnumerator _corutine;


	private IEnumerator ArrowFlowSK8(float waitTimer)
	{
		while (true) 
		{
			yield return new WaitForSeconds (waitTimer);
			for (int i = 0; i < spawnPoints.Length; i++) 
			{
				Instantiate (arrow, spawnPoints [i].position, spawnPoints [i].rotation);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		gameObject.GetComponent<BoxCollider> ().enabled = false;
		_corutine = ArrowFlowSK8 (1);
		StartCoroutine (_corutine);
	}
}
