using NUnit.Framework.Constraints;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Loot : MonoBehaviour
{
    private Player player;
    [SerializeField] private CollectibleSO collectibleSO;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public Animator anim;
    public TMP_Text itemMessage;

    [SerializeField] private bool canBeCollected = false;
    [SerializeField] private float collectDelay;

    public void Initialize(CollectibleSO collectibleSO) //create item based on config
    {
        this.collectibleSO = collectibleSO;
        spriteRenderer.sprite = collectibleSO.itemSprite;
        StartCoroutine(EnableCollection());
    }
    private IEnumerator EnableCollection() //wait before item can be collected after creation
    {
        yield return new WaitForSeconds(collectDelay);
        canBeCollected = true;
    }
    private void OnTriggerEnter2D(Collider2D collision) //collect item on contact
    {
        player = collision.GetComponent<Player>();
        if (player == null || !canBeCollected)
            return;
        CollectItem();
    }
    private void OnTriggerExit2D(Collider2D collision) //deassign player on exit contact
    {
        if (collision.CompareTag("Player"))
            player = null;
    }
    private void CollectItem() //show animated text when collected and destroy object
    {
        itemMessage.text = "Found " + collectibleSO.itemName;
        anim.Play("CollectLoot");
        collectibleSO.Collect(player);
        Destroy(gameObject, 1);
    }
}
