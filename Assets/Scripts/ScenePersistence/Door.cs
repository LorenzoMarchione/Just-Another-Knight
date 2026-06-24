using UnityEngine;
using UnityEngine.UIElements;

public class Door : MonoBehaviour
{
    //scene where i want to go
    [SerializeField] private string targetScene;
    //position in wich i spawn inside the scene
    [SerializeField] private string targetSpawnID;
    [SerializeField] private Collider2D lockDoor;

    //change scenes when going through a door
    private void OnTriggerStay2D(Collider2D collision)
    {
        RoomTransitionManager manager = collision.GetComponent<RoomTransitionManager>();
        if(manager != null)
        {
            manager.EnterRoom(targetScene, targetSpawnID);
        }
    }
    public void LockDoor(bool locked)
    {
        lockDoor.enabled = locked;
    }


}
