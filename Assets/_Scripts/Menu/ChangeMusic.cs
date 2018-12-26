using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ChangeMusic : MonoBehaviour {

	public AudioMixer audioMixer;
	GameObject aux ;
	// Use this for initialization
	float volume = -40.0f;
	void Start () {
		aux = GameObject.Find ("Negative");
		aux.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetVolume(bool more)
	{
		if (more) {
			if(aux.activeSelf) aux.SetActive (false);
			volume += 10.0f;
			if (volume > 0)
				volume = 0;
		}
		else {
			volume -= 10.0f;
			if (volume <= -80.0f) {
				aux.SetActive (true);
				volume = -80.0f;
			}
		}
		audioMixer.SetFloat ("Volume", volume);
		GameObject.Find ("SliderVolume").GetComponent<Slider> ().value = volume;
	}
}
