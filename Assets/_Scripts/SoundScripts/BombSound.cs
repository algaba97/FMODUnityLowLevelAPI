using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSound : MonoBehaviour {

	AudioSource bombSound;

	void Start () {
		bombSound = GetComponent<AudioSource> ();
		EventManager.instance.StartListening ("EVT_SoundEvent_Bomb_Explode",BombHit);
	}

	void OnDestroy()
	{
		if (EventManager.instance != null) 
			EventManager.instance.StopListening ("EVT_SoundEvent_Bomb_Explode", BombHit);
	}

	// Update is called once per frame
	void BombHit ()
	{
		bombSound.Play ();
	}
}
