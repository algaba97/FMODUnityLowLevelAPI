using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	GameObject Scenemanager;
	public Button[] buttons;
	bool pauseaux1= false;
	bool pauseaux2= false;
	public float timeButton = 7.0f;
	float yaxis= 0.0f;
	float auxtime =0.0f;
   int active = 1;
	// Use this for initialization

	bool Aaux1 = true;

	//for the slider
	float auxtimeSlider= 0.0f;
	void Start () {
		Scenemanager = GameObject.Find ("SceneAssistant");

			UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
			buttons[0].onClick.AddListener(delegate { Play(); });
			buttons[1].onClick.AddListener(delegate { Levels(); });
		    buttons[2].onClick.AddListener(delegate { Credits(); });
			buttons[3].onClick.AddListener(delegate { Quit(); });

	}
	// Update is called once per frame
	void Update () {
		checkDown ();


		if (Input.GetButton ("Player1_A")) {
			if(!Aaux1)buttons [active - 1].onClick.Invoke ();

			Aaux1 = true;

		} else {// To force the player when is comming from the Pause->Exit to release the button before menu go
			Aaux1 = false;
		}

		if (active == 3) {
			auxtimeSlider -= Time.deltaTime;
			if (auxtimeSlider < 0.0f) {
			
				if (Input.GetAxis ("Player1_Horizontal_Left") == 1) {
					GameObject.Find ("MusicManager").GetComponent<ChangeMusic> ().SetVolume (true);

				}
				else if (Input.GetAxis ("Player1_Horizontal_Left") == -1) {
					GameObject.Find ("MusicManager").GetComponent<ChangeMusic> ().SetVolume (false);

				}
				auxtimeSlider = 0.1f;
			}
		}
}
	// Check if your axis is down or up also check the time
	void checkDown(){
		yaxis = Input.GetAxis ("Player1_Vertical_Left");
		//Debug.Log (yaxis);
		auxtime -= 1.0f; // Thats its very dirty but if timescale = 0 timed.deltatime = 0 always
		if (auxtime < 0.0f) {

			if (yaxis > 0.1f) {
				nextButton (false);
				auxtime = timeButton;
			} 
			else if (yaxis < -0.1f) {


				nextButton (true);
				auxtime = timeButton;
			}

		}
	}
	void nextButton(bool next){
		Debug.Log ("enter nextbutton");
		if (next) {
			if (active < buttons.GetLength (0)) {
				active++;
				UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject (buttons [active-1].gameObject);
			}

		} 
		else {
			if (active > 1) {
				active--;
				UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject (buttons [active-1].gameObject);
			}

		} 

	}
	void Play(){
		Debug.Log ("A");
		Scenemanager.GetComponent<SceneAssistant> ().afterMenu ();
	}
	void Levels(){
		Debug.Log ("B");
		Scenemanager.GetComponent<SceneAssistant> ().loadlLevels ();
	}
	void Credits(){
	}
	void Quit(){
		Debug.Log ("Quit only works in standalone versioN");
		Application.Quit ();
	}
}
