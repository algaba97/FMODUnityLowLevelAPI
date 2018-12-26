using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Interactable_Bomb : SCR_Interaction_Object
{
    public GameObject explotion;
    public GameObject fuseFlame;
	public BoxCollider deathTrigger; 

    bool isLit;

    void Start()
    {
        ID = "Bomb";
    }


	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Torch") 
		{
			isLit = true;
			fuseFlame.SetActive (true);
		}
	}

    void OnCollisionEnter(Collision col)
    {
        if (isLit && col.gameObject.tag == "Destructible")
        {
			col.gameObject.GetComponent<MeshRenderer> ().enabled = false;
            EventManager.instance.TriggerEvent("EVT_SoundEvent_Bomb_Explode");
            Instantiate(explotion, transform.position, transform.rotation);
			deathTrigger.enabled = false; 

            Destroy(gameObject, 1f);
        }

    }
}
