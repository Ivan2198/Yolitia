using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneToUnload;
    public string loaderScene;
    public string sceneToLoad;
    
    void Start()
    {
        UnloadStartScene();
    }

    private void UnloadStartScene()
    {
        if (SceneManager.GetSceneByName(sceneToUnload).isLoaded)
        {
            SceneManager.sceneUnloaded += OnStartUnloaded;
            SceneManager.UnloadSceneAsync(sceneToUnload);
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(sceneToUnload), sceneToUnload, null);
        }
    }

    private void OnStartUnloaded(Scene scene)
    {
        SceneManager.sceneUnloaded -= OnStartUnloaded;
        SceneManager.sceneLoaded += OnEndLoaded;
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
    }

    private void OnEndLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnEndLoaded;
        UnloadLoader();
    }

    private void UnloadLoader()
    {
        SceneManager.UnloadSceneAsync(loaderScene);
    }
}
