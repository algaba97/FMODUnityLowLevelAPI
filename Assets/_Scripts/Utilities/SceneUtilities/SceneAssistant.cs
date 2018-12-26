using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneAssistant : MonoBehaviour
{
    public enum LoadState
    {
        idle,
        doneLoading,
        loading
    }
    public LoadState loadState;

    //scene presets
    public ScenePreset PresetToLoadOnStart;
    public List<ScenePreset> ScenePresets;

    private ScenePreset currentPreset;

    //idividual scenes
    public List<string> LoadedScenes = new List<string>();

    //flags
    public bool initialLoadComplete = false;
    public bool dontLoadOnStart = false;
    //counters
    private int loadIndex;
    private float loadTimer;

	public bool menu = true;
	public string[] scenenames;
	int lastscene = -1;
    void Start()
    {
        if (!dontLoadOnStart)
            LoadScenePreset(PresetToLoadOnStart, false);
        else
            loadState = SceneAssistant.LoadState.doneLoading;
    }

    void Update()
    {
        //determine if the load is complete
        if (loadState == LoadState.loading)
        {
            if (loadIndex < currentPreset.Scenes.Count)
                loadTimer += Time.deltaTime;

            if (loadIndex == currentPreset.Scenes.Count)
            {
                loadState = LoadState.doneLoading;
                Debug.Log("Loaded " + loadIndex + " scenes, in " + loadTimer + " sec");
            }
        }
        //if all the scenes are loaded, record the time and flag inital load as compleate.
        if (loadState == LoadState.loading)
        {
            if (loadIndex == currentPreset.Scenes.Count)
            {
                loadState = LoadState.doneLoading;
            }
        }

        if (loadState == LoadState.doneLoading)
        {
            loadTimer += Time.deltaTime;

            if (loadTimer >= 2)
            {
                loadState = LoadState.idle;
                loadTimer = 0;
            }
        }
    }
	public void Restart() {
		SceneManager.UnloadSceneAsync (scenenames [lastscene]);
		SceneManager.LoadSceneAsync (scenenames [lastscene],LoadSceneMode.Additive);

	}

	public void nextLevel(){
		SceneManager.UnloadSceneAsync (scenenames [lastscene]);
		lastscene++;
		SceneManager.LoadSceneAsync (scenenames [lastscene],LoadSceneMode.Additive);
	}

    //reload all the currently loaded scenes
	public void ReloadAllCurrentScenes()
    {
        //reset the loading index to accuratley count the scenes being loaded
        loadIndex = 0;
        //flag as loading scenes
        loadState = LoadState.loading;

        //create a buffer to draw from when reloading scenes
        string[] tempBuffer = LoadedScenes.ToArray();

        //unload all scenes
        for (int i = 0; i < LoadedScenes.Count; i++)
        {
            UnLoadSceneAsync(LoadedScenes[i]);
        }
        LoadedScenes.Clear();

        //load all the scenes from the buffer
        for (int i = 0; i < tempBuffer.Length; i++)
        {
            LoadSceneAsync(tempBuffer[i]);
        }
    }

    //Only used for the intial load that happens at the start of the game
    void InitalLoadSceneAsync(string _sceneName)
    {

        StartCoroutine(ILoadSceneAsync(_sceneName));
    }

    //unload a scene
   public void UnLoadSceneAsync(string _sceneName)
    {
        if (LoadedScenes.Contains(_sceneName))
        {
            //unload scene and remove from the loaded scenes list
            SceneManager.UnloadSceneAsync(_sceneName);
        }

    }

    public void LoadSceneAsync(string _sceneName)
    {
        StartCoroutine(ILoadSceneAsync(_sceneName));
    }

    public void LoadScenePreset(int index, bool additive)
    {
        //reset the loading index to accuratley count the scenes being loaded
        loadIndex = 0;
        currentPreset = ScenePresets[index];
        //flag as loading scenes
        loadState = LoadState.loading;

        if (!additive)
        {
            //unload all scenes
            for (int i = 0; i < LoadedScenes.Count; i++)
            {
                UnLoadSceneAsync(LoadedScenes[i]);

            }
            LoadedScenes.Clear();
        }
        //load preset scenes
        for (int i = 0; i < currentPreset.Scenes.Count; i++)
        {
            StartCoroutine(ILoadSceneAsync(currentPreset.Scenes[i].ToString()));
        }
    }
	public void afterMenu(){
		UnLoadSceneAsync ("Menu");
		menu = false;
		lastscene = 2;
		SceneManager.LoadSceneAsync (scenenames [2],LoadSceneMode.Additive);
	

	}

	public void Levelstogame(int number){
		
		SceneManager.UnloadScene("Levels");
		menu = false;
		lastscene = number;
		SceneManager.LoadSceneAsync (scenenames [number],LoadSceneMode.Additive);
	}

	public void loadlLevels() {
		SceneManager.UnloadScene ("Menu");
		menu = false;
		SceneManager.LoadSceneAsync (scenenames [1],LoadSceneMode.Additive);
	}
	public void LoadMenu(){
		SceneManager.UnloadSceneAsync (scenenames [1]);
		SceneManager.LoadSceneAsync (scenenames [0],LoadSceneMode.Additive);
	}
	public void MenufromPause() {
		SceneManager.UnloadSceneAsync (scenenames [lastscene]);
		SceneManager.LoadSceneAsync (scenenames [0],LoadSceneMode.Additive);
	}
    public void LoadScenePreset(ScenePreset preset, bool additive)
    {
        //reset the loading index to accuratley count the scenes being loaded
        loadIndex = 0;
        currentPreset = preset;
        //flag as loading scenes
        loadState = LoadState.loading;

        if (!additive)
        {
            //unload all scenes
            for (int i = 0; i < LoadedScenes.Count; i++)
            {
                UnLoadSceneAsync(LoadedScenes[i]);

            }
            LoadedScenes.Clear();
        }
        //load preset scenes
        for (int i = 0; i < currentPreset.Scenes.Count; i++)
        {
            StartCoroutine(ILoadSceneAsync(currentPreset.Scenes[i].ToString()));
        }
    }

    //async loading of a scene
    IEnumerator ILoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            //not loaded
            if (asyncLoad.progress < 0.9f)
            {

            }
            //ready to activate
            else
            {
                asyncLoad.allowSceneActivation = true;
                if (!LoadedScenes.Contains(sceneName))
                    LoadedScenes.Add(sceneName);
                yield return null;
            }
        }
        //increment the load index when the scene is loaded.
        loadIndex++;
        yield return null;
    }
}
