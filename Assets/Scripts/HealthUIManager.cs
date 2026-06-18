using UnityEngine;
using TMPro;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private TMP_Text text;
    private string prefix = "HP: ";
    private int currentHealth;
    private int maxHealth;
    void Start()
    {
        currentHealth = health.health;
        maxHealth = health.maxHealth;
        text.text = prefix + currentHealth + "/" + maxHealth;
    }

    void Update()
    {
        if(currentHealth != health.health)
        {
            currentHealth = health.health;
            text.text = prefix + currentHealth + "/" + maxHealth;
        }
    }
}
