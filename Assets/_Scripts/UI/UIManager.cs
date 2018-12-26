using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
	public GameObject Pause;
	bool pause = false;
	bool pauseaux1= false;
	bool pauseaux2= false;
	float yaxis= 0.0f;
	 float auxtime =0.0f;
	public float timeButton = 3.0f;
	public Button[] buttons;


	public int active = 1;

	public SceneAssistant sceneAssistant;

	//when u have finished te level 
	public GameObject afterlevel;
	bool finish = false;
	GameObject GOTime;
	GameObject GM;
	bool showInfo = false;
	// Use this for initialization
	public bool sInfo = false;
	public GameObject auxsInfo ;
	public bool lastlevel = false;
	int sinfcount = 0;
	void Start () {
		try {sceneAssistant = GameObject.Find("SceneAssistant").GetComponent<SceneAssistant>();}
		catch{
		}
		GOTime = GameObject.Find ("OBJ_TrackTimer"); //GetComponent<SCR_TrackTimer_Timer> ();
		GM = GameObject.Find("GameManager");
		//auxsInfo = GameObject.Find ("ShieldInfo");
		//auxsInfo.SetActive (false);
		UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
		buttons[0].onClick.AddListener(delegate { Resume(); });
		buttons[1].onClick.AddListener(delegate { Restart(); });
		buttons[2].onClick.AddListener(delegate { Quit(); });
		Pause.SetActive (false);


	}
	
	// Update is called once per frame
	void Update () {
		if (!sInfo)
			sinfcount--;
		sInfo = false;
		checkPause ();
		
	}

	void LateUpdate() {
		
	}
	//Just check if u have used "Start" then check also the jostick and the "A" to do actions and move in the PauseMenu
	void checkPause(){
		//Debug.Log ("enter pause update");
		pauseaux2 = pauseaux1;
		pauseaux1 = Input.GetButton ("Player1_Start");
	 
		if (pauseaux1 && !pauseaux2) {


			pause = !pause;
			Debug.Log ("truE");Debug.Log (pause);
			if (pause) {
				Pause.SetActive (true);
				active = 1;
				nextButton (true);
				nextButton (false);
				Pause.GetComponent<Animator> ().SetBool ("Open", true);
				Time.timeScale = 0;

			} 
			else {
				Resume ();
			}
		}
		if (finish)
			clickafterfinish ();
		if (pause) {
			checkDown ();
			checkAction ();
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
	void Resume(){
		if (Pause.activeSelf) {
			Pause.GetComponent<Animator> ().SetBool ("Open", false);
			Pause.SetActive (false);
			Time.timeScale = 1;
			pauseaux1 = true;
			pauseaux2 = false;
			pause = false;
			Debug.Log ("Resume");
		}
	}

	void Restart(){
		Time.timeScale = 1;
		//sceneAssistant.ReloadAllCurrentScenes ();
		sceneAssistant.Restart ();




	}
	void  Quit(){
		Debug.Log ("Quit");/*
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
		*/
		Time.timeScale = 1;
		sceneAssistant.MenufromPause ();

	}
	void checkAction(){
		if (Input.GetButton ("Player1_A")) {
			buttons [active - 1].onClick.Invoke ();
		}
	}

	// activate the next or rpevious button
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
	public void finishlevel() {
		
		finish = true;
		if (!showInfo) {// to avoid call this function multiple times
			showInfo = true;
			afterlevel.SetActive (true);
			int level = GOTime.GetComponent<SCR_TrackTimer_Timer> ().level;
			//unlock a level
			if (GM.GetComponent<GameManager> ().level <= GOTime.GetComponent<SCR_TrackTimer_Timer> ().level)
				GM.GetComponent<GameManager> ().level++;

			//check the times
			if (GM.GetComponent<GameManager> ().timelevels [level - 1] > GOTime.GetComponent<SCR_TrackTimer_Timer> ().trackTime) {
			
				GameObject.Find ("TimeText2").GetComponent<Text> ().text = "Your time :" + GameObject.Find ("TimeText").GetComponent<Text> ().text
				+ " Best Time: " + GOTime.GetComponent<SCR_TrackTimer_Timer> ().FloatToTime (GM.GetComponent<GameManager> ().timelevels [level - 1], "0:00.00");
			
				GM.GetComponent<GameManager> ().timelevels [level - 1] = GOTime.GetComponent<SCR_TrackTimer_Timer> ().trackTime;
			
			} else {
				GameObject.Find ("TimeText2").GetComponent<Text> ().text = "Your time :" + GameObject.Find ("TimeText").GetComponent<Text> ().text
				+ " Best Time: " + GOTime.GetComponent<SCR_TrackTimer_Timer> ().FloatToTime (GM.GetComponent<GameManager> ().timelevels [level - 1], "0:00.00");
				afterlevel.transform.GetChild (1).transform.gameObject.SetActive (true);
			}
			GM.GetComponent<GameManager> ().Save ();
		}
}
	void clickafterfinish() {
		if (Input.GetButton ("Player1_A")) {
			if(lastlevel)sceneAssistant.nextLevel ();
			else Restart();
		} else if (Input.GetButton ("Player1_Y")) {
			Restart ();
		}
	}
}
