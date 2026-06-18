using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;

    private EnemyConfig config;
    private Enemy enemy;
    public float lastAttacktime;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        config = enemy.Config;
    }
    public bool CanMeleeAttack() => Time.time >= lastAttacktime + config.meleeCooldown;
    public bool CanRangedAttack() => Time.time >= lastAttacktime + config.rangedCooldown;
    public void PerformMeleeAtack()
    {
        lastAttacktime = Time.time;
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, config.meleeRange, config.targetLayer);
        if (!hit)
            return;
        Health health = hit.GetComponent<Health>();
        if (health != null)
        {
            health.ChangeHeatlh(-config.meleeDamage, transform.position);
        }

    }
    public void PerformRangedAttack()
    {
        lastAttacktime = Time.time;
        Vector2 fireDirection = (enemy.CurrentTarget.position - attackPoint.position).normalized;
        float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        GameObject newProjectile = Instantiate(config.projectilePrefab, attackPoint.position, rotation);
        Projectile projectile = newProjectile.GetComponent<Projectile>();
        projectile.Damage = config.rangedDamage;
        projectile.LifeTime = config.projectileLifetime;

        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        rb.linearVelocity = fireDirection * config.projectileSpeed;
    }
}
