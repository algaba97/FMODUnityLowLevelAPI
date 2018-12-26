using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChange : MonoBehaviour {

	AudioSource song;
	public AudioClip newSong;

	// Use this for initialization
	void Start () {
		song = GameObject.Find ("FollowCam").GetComponent<AudioSource> ();
	}
	
	void OnTriggerEnter(Collider other)
	{
		song.clip = newSong;
		song.Play ();
	}
}
