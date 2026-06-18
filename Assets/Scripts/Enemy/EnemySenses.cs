using System;
using UnityEngine;

public class EnemySenses : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemyConfig config;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform[] wallCheck;
    [SerializeField] private Transform attackPoint;

    public bool IsAtCliff() => !Physics2D.Raycast(groundCheck.position, Vector2.down, config.groundCheckDistance, config.groundLayer);
    public bool IsHittingWall()
    {
        Vector2 dir = Vector2.right * enemy.Facing;
        foreach (Transform check in wallCheck)
        {
            bool hitWall = Physics2D.Raycast(check.position, dir, config.wallCheckDistance, config.wallLayer);
            if (hitWall)
                return true;
        }
        return false;
    }
    public bool IsPushingAlly() => Physics2D.Raycast(groundCheck.position, Vector2.right * enemy.Facing, config.groundCheckDistance, config.enemyLayer);
    public Transform GetChaseTarget()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, config.chaseRange, config.targetLayer);
        if (!hit)
            return null;
        Player player = hit.GetComponent<Player>();
        if (player.currentState == player.deathState)
            return null;
        return hit.transform;
    }
    public bool IsInMeleeRange(Transform target)
    {
        if(!target)
            return false;
        float distance = Vector2.Distance(attackPoint.position, target.position);
        return distance <= config.meleeRange;
    }
    public bool IsInShootingRange(Transform target)
    {
        if (!target)
            return false;
        float distance = Vector2.Distance(attackPoint.position, target.position);
        return distance <= config.rangedRange;
    }
    private void OnDrawGizmosSelected()
    {
        //Ground Check
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * config.groundCheckDistance);

        //Wall Check
        Gizmos.color = Color.cyan;
        Vector3 dir = Vector3.right * enemy.Facing;
        foreach (Transform check in wallCheck)
        {
            Gizmos.DrawLine(check.position, check.position + dir * config.wallCheckDistance);
        }
        //Chase Check
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(attackPoint.position, config.chaseRange);

        //MeleeCheck
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(attackPoint.position, config.meleeRange);

        //RangedCheck
        Gizmos.color = Color.orange;
        Gizmos.DrawWireSphere(attackPoint.position, config.rangedRange);
    }
}
