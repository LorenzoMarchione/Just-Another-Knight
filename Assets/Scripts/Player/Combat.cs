using UnityEngine;

public class Combat : MonoBehaviour
{
    public Player player;

    [Header("Attack Settings")]
    public int damage;
    public float attackRadius;
    public float attackCooldown;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public Animator hitFX;

    public bool CanAttack => Time.time >= nextAttackTime; //true if cooldown time has passed
    private float nextAttackTime;

    public void AttackAnimationFinished() //call player when attack animation has ended
    {
        player.AnimationFinished();
    }

    public void Attack() //this function is called by attack animation
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
    public void OnDrawGizmosSelected() //gizmo of attack cast
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
