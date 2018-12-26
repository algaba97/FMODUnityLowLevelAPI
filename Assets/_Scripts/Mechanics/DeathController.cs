using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlasticMoose.Minecart;
public class DeathController : MonoBehaviour
{

    public SceneAssistant sceneAssistant;
    public int health;
    public GameObject healthBar;
    float elapsedTime;
    public float invulnerableTime;
    public bool canDie;
	Movement move;
	public float speedLoss;

    void OnEnable()
    {
        health = 3;
        if (GameObject.Find("SceneAssistant") != null)
            sceneAssistant = GameObject.Find("SceneAssistant").GetComponent<SceneAssistant>();
        EventManager.instance.StartListening("EVT_Player_TriggerDeath", KillPlayer);
        healthBar = GameObject.Find("Panel");
		move = gameObject.GetComponent<Movement> ();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    void OnDestroy()
    {
        if (EventManager.instance != null)
        {
            EventManager.instance.StopListening("EVT_Player_TriggerDeath", KillPlayer);
        }
    }

    void KillPlayer()
    {
        if (elapsedTime > invulnerableTime && canDie)
        {
			if (GameObject.Find ("SceneAssistant") != null) {
				GameObject.Find ("GameManager").GetComponent<vibration> ().vibrate (0.5f);
			}
            health--;
			move.CurrentSpeed = move.CurrentSpeed - speedLoss;
            if (healthBar != null)
                healthBar.transform.GetChild(health + 4).gameObject.SetActive(false);
            elapsedTime = 0;
        }

        if (health <= 0)
        {
            print("Died");
			if (sceneAssistant != null)
				sceneAssistant.Restart ();
        }
    }
}