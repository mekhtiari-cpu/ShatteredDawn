using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    private string currentActiveScene;
    private const string LOADINGSCENENAME = "Loading Scene";
    private const string MAINMENUSCENE = "Main Menu";

    // Start is called before the first frame update
    void Start()
    {
        // Load Main Menu Scene on start
        LoadSceneWithTransition(MAINMENUSCENE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // We Call this function for any scene transitions @maryam.ekihtiari. This should allow use to use the loading scene to appear inbetween scenes
    public void LoadSceneWithTransition(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    private IEnumerator LoadSceneRoutine(string newSceneName)
    {
        currentActiveScene = SceneManager.GetActiveScene().name;

        if (!SceneManager.GetSceneByName(LOADINGSCENENAME).isLoaded)
        {
            SceneManager.LoadScene(LOADINGSCENENAME, LoadSceneMode.Additive);
        }

        CanvasGroup canvas = GetComponent<CanvasGroup>();
        canvas.alpha = 1;
        // Begin async loading
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;

        // Wait for the scene to fully load
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f) // Scene is loaded but not yet activated
            {
                // Activate the scene
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        // Switch the active scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(newSceneName));
        canvas.alpha = 0;

        // Unload the previous active scene if applicable
        if (!string.IsNullOrEmpty(currentActiveScene) && currentActiveScene != LOADINGSCENENAME)
        {
            SceneManager.UnloadSceneAsync(currentActiveScene);
        }

        currentActiveScene = newSceneName;

        if (currentActiveScene == MAINMENUSCENE)
        {
            SoundManager.Instance.PlayMusic(SoundManager.Instance.mainMenuMusic);
        }
        else
        {
            SoundManager.Instance.StopMusic();
        }

        if (currentActiveScene == "Main Scene")
        {
            SoundManager.Instance.PlayAmbience(SoundManager.Instance.ambianceMusic);
        }
        else
        {
            SoundManager.Instance.StopAmbience();
        }

    }
}
