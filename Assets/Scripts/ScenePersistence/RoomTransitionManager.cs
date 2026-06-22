using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTransitionManager : MonoBehaviour
{
    //room that is currently loaded
    private string currentRoom = "";

    void Start()
    { 
        //pass it empty strings so it doesnt load nor unloads but saves current scene
        EnterRoom("", "");
    }
    //doors call this method
    public void EnterRoom(string sceneName, string spawnID)
    {
        StartCoroutine(Transition(sceneName, spawnID));
    }
    //unload current scene load another scene in parallel and save current scene
    private IEnumerator Transition(string sceneName, string spawnID)
    {
        if (!string.IsNullOrEmpty(currentRoom))//this prevents transitions if the game just started
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
}
