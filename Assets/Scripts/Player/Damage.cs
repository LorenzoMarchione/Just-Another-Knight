using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private Player player;
    public Health health;

    [Header("KnockBack Settings")]
    public float knockbackDuration = 0.2f;
    public float knockbackForce = 10f;

    public void OnEnable() //suscribe to health events on player creation
    {
        health.OnDamaged += HandleDamage;
        health.OnDeath += HandleDeath;
    }
    public void OnDisable() //unsuscribe to damage event in pleyer destruction (death is not used for now)
    {
        health.OnDamaged -= HandleDamage;
    }
    public void HandleDamage(Vector2 sourcePosition)//when damaged change to damaged state and give it knockback direction for calculation
    {
        int knockbackDir = 0;
        knockbackDir = transform.position.x > sourcePosition.x ? 1 : -1;

        player.damagedState.SetParameters(knockbackDir);
        player.ChangeState(player.damagedState);
    }
    public void HandleDeath(Vector2 sourcePosition)//when damaged and dead change to death state and give it knockback direction for calculation
    {
        int knockbackDir = 0;
        knockbackDir = transform.position.x > sourcePosition.x ? 1 : -1;

        player.deathState.SetParameters(knockbackDir);
        player.ChangeState(player.deathState);
    }
}
