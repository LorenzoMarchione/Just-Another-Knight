using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    private PlayerInput playerInput;
    private SceneChanger changer;
    [SerializeField] private string sceneToChange = "WinScene";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerInput>(out var input))
        {
            playerInput = input;
            changer = collision.GetComponent<SceneChanger>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerInput>(out var input))
            if (input == playerInput)
            {
                playerInput = null;
                changer = null;
            }

    }
    private void Update()
    {
        if (playerInput == null || changer == null)
            return;
        if (playerInput.actions["Interact"].WasPressedThisFrame())
        {
            changer.sceneToLoad = sceneToChange;
            changer.ChengeSceneNow();
        }

    }
}
