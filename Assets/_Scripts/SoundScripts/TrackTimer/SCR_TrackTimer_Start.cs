using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_TrackTimer_Start : MonoBehaviour
{
	public GameObject lightBall;
    public bool active;
	Material ballMat;
	Light ballLight;
	public Animator anim;

	void Start()
	{
		ballMat = lightBall.GetComponent<Renderer> ().material;
		ballLight = lightBall.transform.GetChild (0).GetComponent<Light>();
	}

    void OnTriggerEnter(Collider other)
    {
        active = true;
		ballMat.color = Color.green;
		ballLight.color = Color.green;
		anim.SetBool ("Activate", true);

    }
}
