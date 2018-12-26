using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicalController_SceneLoading : MonoBehaviour
{
    SceneAssistant sceneAssistant;
    public GameObject loadingScreen;

    void Start()
    {
        sceneAssistant = this.GetComponent<SceneAssistant>();
        loadingScreen = GameObject.Find("LoadingScreen");
    }

    void Update()
    {
        if (sceneAssistant.loadState == SceneAssistant.LoadState.loading)
        {
            if (loadingScreen != null)
                loadingScreen.SetActive(true);
        }
        else
        {
            if (loadingScreen != null)
                loadingScreen.SetActive(false);
        }
    }
}
