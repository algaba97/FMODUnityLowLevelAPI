using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAssistant_EventHook : MonoBehaviour
{

    SceneAssistant assistant;

    void Awake()
    {
        EventManager.instance.StartListening("StartGame", StartGame);
    }

    void OnDestroy()
    {
        if (EventManager.instance != null)
            EventManager.instance.StopListening("StartGame", StartGame);
    }

    void StartGame()
    {
        assistant = GetComponent<SceneAssistant>();
        //load the main game components
        assistant.LoadScenePreset(0, false);
        Debug.Log("Hookstart");
    }
}
