using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
	//Scene manager to load scenes and bla bla bla
	GameObject Scenemanager;

	float xaxis;//X axis
	public float auxtime =0.0f;
	public float timeButton =1.5f;
	public int active = 1;
	public Button[] buttons;
	public GameObject[] levels;
	public GameObject aux;

	int maxlevel = -1;


	// Use this for initialization
	void Start () {
		Scenemanager = GameObject.Find ("SceneAssistant");
		maxlevel = 2;
		for (int i = 0; i <= maxlevel; i++) {
			Debug.Log (i);
			levels [i].SetActive (true);
			levels [i].transform.GetChild (0).gameObject.SetActive (false);
			levels [i].transform.GetChild (1).gameObject.GetComponent<Text> ().text = "Time :" + FloatToTime(GameObject.Find ("GameManager").GetComponent<GameManager> ().timelevels [i],"0:00.00");
			levels [i].SetActive (false);

		}
		levels [0].SetActive (true);

	}
	
	// Update is called once per frame
	void Update () {
		checkJoystick ();
		endAnimation ();
		checkA ();

	}
	void endAnimation() {
		for (int i = 0; i < levels.GetLength (0); i++) {
			if (levels [i].GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("levels2") && levels [i].GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime >= 1.0f) {
				levels [i].SetActive (false);
				Debug.Log ("inside");
			}
		}
	}
	void checkJoystick(){
		xaxis = -Input.GetAxis ("Player1_Horizontal_Left");
		//Debug.Log (yaxis);
		auxtime -= Time.deltaTime;
		if (auxtime < 0.0f) {// levels[active-1].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("levels1") && levels[active-1].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime>=1.0f 

			if (xaxis > 0.1f) {
				nextButton (false);
				auxtime = timeButton;
			} 
			else if (xaxis < -0.1f) {


				nextButton (true);
				auxtime = timeButton;
			}

		}
	}
	void nextButton(bool next) {
		Debug.Log ("enter nextbutton");
		if (next) {
			if (active < buttons.GetLength (0)) {
				active++;
				levels[active -2].GetComponent<Animator> ().SetBool ("next", true);
				levels [active - 1].SetActive (true);
				levels[active -1].GetComponent<Animator> ().SetBool ("next", false);
			
			}

		} 
		else {
			if (active > 1) {
				active--;
				levels[active ].GetComponent<Animator> ().SetBool ("next", true);
				levels [active-1].SetActive (true);
				levels[active -1].GetComponent<Animator> ().SetBool ("next", false);
			}

		} 
	}

	void checkA(){
		if (Input.GetButtonDown ("Player1_A")) {
			Scenemanager.GetComponent<SceneAssistant> ().Levelstogame (active+1);
			// This is for know which level needs to be load ,buttons [active - 1].transform.parent.gameObject.GetComponent<LevelUnlock> ().number;
		}
		if (Input.GetButtonDown ("Player1_B")) {
			Scenemanager.GetComponent<SceneAssistant> ().LoadMenu ();
		
		}
	}
	public string FloatToTime(float toConvert, string format)
	{
		switch (format)
		{
		case "00.0":
			return string.Format("{0:00}:{1:0}",
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert * 10) % 10));//miliseconds
			break;
		case "#0.0":
			return string.Format("{0:#0}:{1:0}",
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert * 10) % 10));//miliseconds
			break;
		case "00.00":
			return string.Format("{0:00}:{1:00}",
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert * 100) % 100));//miliseconds
			break;
		case "00.000":
			return string.Format("{0:00}:{1:000}",
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert * 1000) % 1000));//miliseconds
			break;
		case "#00.000":
			return string.Format("{0:#00}:{1:000}",
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert * 1000) % 1000));//miliseconds
			break;
		case "#0:00":
			return string.Format("{0:#0}:{1:00}",
				Mathf.Floor(toConvert / 60),//minutes
				Mathf.Floor(toConvert) % 60);//seconds
			break;
		case "#00:00":
			return string.Format("{0:#00}:{1:00}",
				Mathf.Floor(toConvert / 60),//minutes
				Mathf.Floor(toConvert) % 60);//seconds
			break;
		case "0:00.0":
			return string.Format("{0:0}:{1:00}.{2:0}",
				Mathf.Floor(toConvert / 60),//minutes
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert * 10) % 10));//miliseconds
			break;
		case "#0:00.0":
			return string.Format("{0:#0}:{1:00}.{2:0}",
				Mathf.Floor(toConvert / 60),//minutes
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert * 10) % 10));//miliseconds
			break;
		case "0:00.00":
			return string.Format("{0:0}:{1:00}.{2:00}",
				Mathf.Floor(toConvert / 60),//minutes
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert * 100) % 100));//miliseconds
			break;
		case "#0:00.00":
			return string.Format("{0:#0}:{1:00}.{2:00}",
				Mathf.Floor(toConvert / 60),//minutes
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert * 100) % 100));//miliseconds
			break;
		case "0:00.000":
			return string.Format("{0:0}:{1:00}.{2:000}",
				Mathf.Floor(toConvert / 60),//minutes
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert * 1000) % 1000));//miliseconds
			break;
		case "#0:00.000":
			return string.Format("{0:#0}:{1:00}.{2:000}",
				Mathf.Floor(toConvert / 60),//minutes
				Mathf.Floor(toConvert) % 60,//seconds
				Mathf.Floor((toConvert * 1000) % 1000));//miliseconds
			break;
		}
		return "error";
	}
}
