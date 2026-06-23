using UnityEngine;
using UnityEngine.UIElements;

public class Door : MonoBehaviour
{
    //scene where i want to go
    [SerializeField] private string targetScene;
    //position in wich i spawn inside the scene
    [SerializeField] private string spawnID;
    
    private float cooldown = 2f;
    private float timer;
    private bool canUse;

    private void Awake()
    {
        timer = cooldown;
        canUse = false;
    }
    private void Update()
    {
        if(timer <= 0)
            canUse = true;
        else
            timer -= Time.deltaTime;

}

    //change scenes when going through a door
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RoomTransitionManager manager = collision.GetComponent<RoomTransitionManager>();
        if(manager != null && canUse)
        {
            manager.EnterRoom(targetScene, spawnID);
        }
}
}
