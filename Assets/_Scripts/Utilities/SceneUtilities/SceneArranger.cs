using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneArranger : MonoBehaviour
{
    public bool AutoPlacePlayer = true;
    private SceneAssistant sceneAssistant;
    void Start()
    {
        sceneAssistant = GetComponent<SceneAssistant>();
    }

    void Update()
    {
		
        //arrange the scene when the intial load is complete
        if (sceneAssistant.loadState == SceneAssistant.LoadState.doneLoading && sceneAssistant.menu == false)
        {
            ArrangeScene();
        }
    }

    //puts stuff where its supposed to go so i dont have to do it manually, mostly for testing.
    void ArrangeScene()
    {
        if (AutoPlacePlayer)
            PlacePlayer();
    }

    //place the player in the right position for play
    void PlacePlayer()
    {
        Transform player = GameObject.Find("Player_IndirectMovement").transform;

        if (player != null)
        {
			
            //place the player in the minecart
            GameObject playerPos = GameObject.Find("PlayerPos");
            if (playerPos != null)
            {
				Debug.Log ("in");
                player.position = playerPos.transform.position;
                player.rotation = playerPos.transform.rotation;
                player.parent = playerPos.transform;
			
            }
        }
    }
}
