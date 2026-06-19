using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<Vector2> OnDamaged;
    public event Action<Vector2> OnDeath;

    public int maxHealth;
    public int health;

    public void Start()
    {
        health = maxHealth;
    }
    public void ChangeHeatlh(int amount, Vector2 sourcePosition) //call event when damaged or dead
    {
        health += amount;
        if(health > maxHealth) 
            health = maxHealth;
        else if (health <= 0)
            OnDeath?.Invoke(sourcePosition);
        else if(amount < 0)
            OnDamaged?.Invoke(sourcePosition);
    }
}
