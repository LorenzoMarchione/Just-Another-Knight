using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private SceneChanger changer;
    public Health health;

    [Header("KnockBack Settings")]
    public float knockbackDuration = 0.2f;
    public float knockbackForce = 10f;

    public void OnEnable()
    {
        health.OnDamaged += HandleDamage;
        health.OnDeath += HandleDeath;
    }
    public void OnDisable()
    {
        health.OnDamaged -= HandleDamage;
    }
    public void HandleDamage(Vector2 sourcePosition)
    {
        int knockbackDir = 0;
        knockbackDir = transform.position.x > sourcePosition.x ? 1 : -1;

        player.damagedState.SetParameters(knockbackDir);
        player.ChangeState(player.damagedState);
    }
    public void HandleDeath(Vector2 sourcePosition)
    {
        int knockbackDir = 0;
        knockbackDir = transform.position.x > sourcePosition.x ? 1 : -1;

        player.deathState.SetParameters(knockbackDir, changer);
        player.ChangeState(player.deathState);
    }
}
