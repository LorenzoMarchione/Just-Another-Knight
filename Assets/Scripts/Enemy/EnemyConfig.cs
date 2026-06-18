using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyConfig")]

public class EnemyConfig : ScriptableObject
{
    [Header("General")]
    public float turnTheshold = 0.2f;

    [Header("Patrol")]
    public float patrolSpeed = 5f;
    public float groundCheckDistance = 0.7f;
    public float wallCheckDistance = 3f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public LayerMask enemyLayer;

    [Header("Chase")]
    public float chaseSpeed = 7.5f;
    public float chaseRange = 5f;
    public LayerMask targetLayer;

    [Header("Attack")]
    public float meleeRange = 1.5f;
    public int meleeDamage = 2;
    public float meleeCooldown = 1f;

    [Header("Ranged Attack")]
    public float rangedRange = 4.5f;
    public int rangedDamage = 1;
    public float rangedCooldown = 2f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 12;
    public float projectileLifetime = 3;

    [Header("Damaged")]
    public float knockbackForce = 0.2f;
    public float knockbackDuration = 30f;

}
