using UnityEngine;
using UnityEngine.SceneManagement;

public static class BootstrapLoader 
{
    private const string BootstrapScene = "Bootstrap";
    private const string MainMenuScene = "MainMenu";

    //this runs a static method right after scene loads
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Init()
    {
        //call OnSceneLoaded method anytime scene manager loads a new scene
        SceneManager.sceneLoaded += OnSceneLoaded;
        EnsureBootstrapLoaded(SceneManager.GetActiveScene());
    }
    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EnsureBootstrapLoaded(scene);
    }
    private static void EnsureBootstrapLoaded(Scene scene)
    {
        //if this is main menu dont load bootstrap
        if (scene.name == MainMenuScene)
            return;
        //if bootstrap is not loaded, load it
        if (!SceneManager.GetSceneByName(BootstrapScene).isLoaded)
            //loads scene in the background
            SceneManager.LoadSceneAsync(BootstrapScene, LoadSceneMode.Additive);
    }
}
