using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSound : MonoBehaviour {

	AudioSource arrowSound;

	// Use this for initialization
	void OnEnable () {
		arrowSound = GetComponent<AudioSource> ();
		EventManager.instance.StartListening ("EVT_SoundEvent_Arrow_Wood",ArrowHit);
	}

	void OnDestroy()
	{
		if (EventManager.instance != null) 
			EventManager.instance.StopListening ("EVT_SoundEvent_Arrow_Wood", ArrowHit);
	}
	
	// Update is called once per frame
	void ArrowHit ()
	{
		arrowSound.Play();
	}
}
