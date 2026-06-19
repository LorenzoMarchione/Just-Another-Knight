using UnityEngine;

public abstract class CollectibleSO : ScriptableObject
{
    //base config for items
    [Header("General Settings")]
    public string itemName;
    public Sprite icon;
    public Sprite itemSprite;

    public abstract void Collect(Player player);
}
