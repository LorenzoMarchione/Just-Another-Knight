using NUnit.Framework;
using System;
using System.Collections;
using Unity.VisualScripting;
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
    //lock for transitions
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
        if (!string.IsNullOrEmpty(spawnID))
            yield return screenFader.Fade(0f, 1f, 0.5f);
        //this prevents transitions if the game just started
        if (!string.IsNullOrEmpty(currentRoom))
        {
            yield return SceneManager.UnloadSceneAsync(currentRoom);
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        RoomService service = ServiceLocator.Get<RoomService>();
        foreach (Door door in service.doors)
            door.LockDoor(true);
        //this is to ensure that bootstrap scene doesn't get unloaded
        //save loaded scene
        Scene newScene = SceneManager.GetSceneByName(sceneName);
        //activate saved scene
        if(newScene.IsValid())
            SceneManager.SetActiveScene(newScene);
        //save current activated scene
        currentRoom = SceneManager.GetActiveScene().name;
        //wait 1 frame to let room register its roomservices
        yield return null;
        //save room service
        SetupRoom(service, spawnID);
        SetupCameraConfiner(service);
        //wait before fading to allow camera to relocate
        yield return new WaitForSeconds(0.75f);
        //re initialize parallax after camera has positioned itself
        ResetParallax(service);
        //unlock player controls after pitch black screen and before fade
        //fade to clean screen after transition
        if(!string.IsNullOrEmpty(spawnID))
            yield return screenFader.Fade(1f, 0f, 1f);
        isTransitioning = false;
        foreach (Door door in service.doors)
            door.LockDoor(false);
    }
    //set spawnpoint of player for this room
    private void SetupRoom(RoomService service, string spawnID)
    {
        if (string.IsNullOrEmpty(spawnID))
            return;
        //get spawn from room service
        SpawnPoint spawnToUse = service.GetSpawn(spawnID);
        if(spawnToUse != null)
        {
            transform.position = spawnToUse.transform.position;
        }
    }
    //get camera confiner from room service
    private void SetupCameraConfiner(RoomService service)
    {
        if(service.provider != null)
            cameraManager.SetConfiner(service.provider.confiner);
    }
    //get parallax manager from room service
    private void ResetParallax(RoomService service)
    {
        if(service.parallaxManager != null)
            service.parallaxManager.Initialize(cameraManager.camTransform);
    }
}
