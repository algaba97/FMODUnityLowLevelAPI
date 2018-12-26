using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoController : MonoBehaviour 
{

	public GameObject[] jumpPoints;
	GameObject oldPosition;
	GameObject newPosition;
	public GameObject player;

	public float timeDeath = 10f;
	float timer = 0;

	public bool stickToPlayer;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (stickToPlayer) 
		{
			transform.parent= player.transform;
			transform.localPosition = new Vector3 (-0.88f, transform.localPosition.y, transform.localPosition.z);
			timer += Time.deltaTime;
			if (timeDeath < timer) {
				
				EventManager.instance.TriggerEvent ("EVT_Player_TriggerDeath");
				Destroy (this.gameObject);
			}
			//Checking for a torch
			if (GameObject.Find ("PlayerController_Right").GetComponent<SCR_Player_HandController> ().HaveIGotATorch () || GameObject.Find ("PlayerController_Left").GetComponent<SCR_Player_HandController> ().HaveIGotATorch ()) {
				GameObject.Find ("GameManager").GetComponent<vibration> ().lowvibrate (0.01f);
				Destroy (this.gameObject);
			}
/*			transform.position = Vector3.MoveTowards (transform.position, newPosition.transform.position, Time.deltaTime * speed);

			elapsedTime += Time.deltaTime;
			if (elapsedTime >= repositionFrequency) 
			{
				EventManager.instance.TriggerEvent ("EVT_Player_TriggerDeath");
				elapsedTime = 0;
				newPosition = jumpPoints [Random.Range (0, jumpPoints.Length)];
				while (newPosition == oldPosition) {
					newPosition = jumpPoints [Random.Range (0, jumpPoints.Length)];
					print ("samePos");
				}

				oldPosition = newPosition;

			}
		*/}
	}

	void OnTriggerEnter(Collider other)

	{
		Debug.Log("Enter Colldier");
		Debug.Log(other.tag);

		if (other.tag == "Cart") {
			player = other.gameObject; 
			stickToPlayer = true;
			print ("player hit");
			if (GameObject.Find ("SceneAssistant") != null) {
				Debug.Log (" Vibrate");
				GameObject.Find ("GameManager").GetComponent<vibration> ().lowvibrate (timeDeath);
			}
		}

	}
}
