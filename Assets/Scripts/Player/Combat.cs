using UnityEngine;

public class Combat : MonoBehaviour
{
    public Player player;

    [Header("Attack Variables")]
    public int damage;
    public float attackRadius;
    public float attackCooldown;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public Animator hitFX;

    public bool CanAttack => Time.time >= nextAttackTime;
    private float nextAttackTime;

    public void AttackAnimationFinished()
    {
        player.AnimationFinished();
    }

    public void Attack()
    {
        if (!CanAttack)
            return;

        nextAttackTime = Time.time + attackCooldown;
        Collider2D enemy = Physics2D.OverlapCircle(attackPoint.position, attackRadius, enemyLayer);

        if (enemy != null)
        {
            hitFX.Play("HitFX");
            enemy.GetComponent<Health>().ChangeHeatlh(-damage, transform.position);
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
