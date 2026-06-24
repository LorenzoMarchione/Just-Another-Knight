using UnityEngine;
using UnityEngine.UIElements;

public class Door : MonoBehaviour
{
    //scene where i want to go
    [SerializeField] private string targetScene;
    //position in wich i spawn inside the scene
    [SerializeField] private string targetSpawnID;

    //change scenes when going through a door
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RoomTransitionManager manager = collision.GetComponent<RoomTransitionManager>();
        if(manager != null)
        {
            manager.EnterRoom(targetScene, targetSpawnID);
        }
}
}
