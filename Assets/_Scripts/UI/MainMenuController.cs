using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    void Start()
    {

    }


    void Update()
    {

    }

    public void StartGame()
    {
        EventManager.instance.TriggerEvent("StartGame");

    }

    public void Settings()
    {

    }
}
