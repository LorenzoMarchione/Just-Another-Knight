using System.Collections.Generic;
using UnityEngine;

public class RoomService : MonoBehaviour
{
    //this script saves all necessary components of this room
    public CameraConfinerProvider provider;
    public ParallaxManager parallaxManager;
    public List<SpawnPoint> spawnPoints;
    public List<Door> doors;

    void Awake()
    {
        ServiceLocator.Register<RoomService>(this);
    }
    public SpawnPoint GetSpawn(string id)
    {
        return spawnPoints.Find(spawn => spawn.spawnID == id);
    }
}
