using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialLvl1 : MonoBehaviour {

	public Text nextText;
	public Text explaintext;
	public string words;

	[Header("Add canvas/popup ")]
	public GameObject Menu;
	bool Aaux1= false;
	bool Aaux2= false;
	public int imTutorial = 0;


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			popup ();
			explaintext.text = words;
		}
	}
	void Update(){
		Aaux1 = Input.GetButton ("Player1_A");
		if (Menu.activeSelf && ( Aaux1 != Aaux2)) {
			popdown ();
		}

		Aaux2 = Aaux1;
	}
	void popup(){
		
		Time.timeScale = 0;
		Menu.SetActive (true);
		Menu.transform.GetChild(imTutorial).gameObject.SetActive (true);
	}
	void popdown() {
		Time.timeScale = 1;
		GameObject[] aux = GameObject.FindGameObjectsWithTag ("TutorialDelete");
		for ( int i = 0; i< aux.GetLength(0);i++){
			aux[i].SetActive (false);
		}
		Menu.SetActive (false);

	}
}
