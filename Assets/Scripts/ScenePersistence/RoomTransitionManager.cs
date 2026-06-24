using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTransitionManager : MonoBehaviour
{
    //this script is the bridge between bootstrap scene an room scenes
    //control screen fade from transitions
    [SerializeField] private ScreenFader screenFader;
    [SerializeField] private CameraManager cameraManager;
    //room that is currently loaded
    private string currentRoom = "";
    private bool isTransitioning;

    void Start()
    { 
        //pass it empty strings so it doesnt load nor unloads but saves current scene
        EnterRoom("", "");
    }
    //doors call this method
    public void EnterRoom(string sceneName, string spawnID)
    {
        //this is to stop player from immediatly going to another room while transition isnt finished
        if (isTransitioning)
            return;
        StartCoroutine(Transition(sceneName, spawnID));
    }
    //unload current scene load another scene in parallel and save current scene
    private IEnumerator Transition(string sceneName, string spawnID)
    {
        isTransitioning = true;
        //fade to black screen before transition
        yield return screenFader.Fade(0f, 1f, 0.5f);
        //this prevents transitions if the game just started
        if (!string.IsNullOrEmpty(currentRoom))
        {
            yield return SceneManager.UnloadSceneAsync(currentRoom);
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        //this is to ensure that bootstrap scene doesn't get unloaded
        //save loaded scene
        Scene newScene = SceneManager.GetSceneByName(sceneName);
        //activate saved scene
        if(newScene.IsValid())
            SceneManager.SetActiveScene(newScene);
        //save current activated scene
        currentRoom = SceneManager.GetActiveScene().name;
        SetupRoom(spawnID);
        SetupCameraConfiner();
        //wait before fading to allow camera to relocate
        yield return new WaitForSeconds(0.75f);
        //fade to clean screen after transition
        ResetParallax();
        yield return screenFader.Fade(1f, 0f, 1f);
        isTransitioning = false;
    }
    //set spawnpoint of player for this room
    private void SetupRoom(string spawnID)
    {
        //find all spawns in the room
        SpawnPoint[] spawns = FindObjectsByType<SpawnPoint>();
        SpawnPoint spawnToUse = spawns[0];

        if (!string.IsNullOrEmpty(spawnID))
        {
            //search for desired spawn, save it and move player to spawn position
            foreach (SpawnPoint spawn in spawns)
            {
                if (spawn.spawnID == spawnID)
                {
                    spawnToUse = spawn;
                    transform.position = spawn.transform.position;
                    break;
                }
            }
        }
    }
    //find script with scene confiner and give it to cinemachine trough camera manager
    private void SetupCameraConfiner()
    {
        CameraConfinerProvider provider = FindAnyObjectByType<CameraConfinerProvider>();
        cameraManager.SetConfiner(provider.confiner);
    }
    private void ResetParallax()
    {
        ParallaxManager parallax = FindAnyObjectByType<ParallaxManager>();
        if(parallax != null) 
        {
            parallax.Initialize(cameraManager.camTransform);
        }
    }
}
