using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private List<CollectibleSO> lootTable = new List<CollectibleSO>();
    [SerializeField] private GameObject lootPrefab;
    [SerializeField] private float spawnDelay = 0.2f;
    [SerializeField] private float launchForce = 4.0f;

    private PlayerInput playerInput;
    private bool isOpened;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerInput>(out var input))
            playerInput = input;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerInput>(out var input))
            if (input == playerInput)
                playerInput = null;
    }
    private void Update()
    {
        if (isOpened || playerInput == null)
            return;
        if(playerInput.actions["Interact"].WasPressedThisFrame())
            StartCoroutine(OpenChestRoutine());

    }
    private IEnumerator OpenChestRoutine()
    {
        isOpened = true;
        anim.Play("ChestOpen");

        yield return new WaitForSeconds(spawnDelay);

        foreach (CollectibleSO loot in lootTable)
        {
            Loot newLoot = Instantiate(lootPrefab, transform.position, Quaternion.identity).GetComponent<Loot>();
            newLoot.Initialize(loot);

            Rigidbody2D rigidbody2 = newLoot.GetComponent<Rigidbody2D>();

            Vector2 direction = new Vector2(Random.Range(-0.2f, 0.2f), 1).normalized;
            rigidbody2.AddForce(direction * launchForce, ForceMode2D.Impulse);

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
