using UnityEngine;

public class IdleState : State
{
    private Transform target;
    protected override string AnimBoolName => "isIdling";

    public IdleState(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        base.Enter();
        rigidbody2.linearVelocity = Vector2.zero;
    }
    public override void FixedUpdate()
    {
        //1st check for a target
        target = senses.GetChaseTarget();
        enemy.CurrentTarget = target;
        if (!target)
        {
            stateMachine.ChangeState(new PatrolState(enemy));
            return;
        }
        enemy.FaceTarget(target);
        //2nd check if is we can attack
        if (senses.IsInMeleeRange(target) && combat.CanMeleeAttack())
        {
            stateMachine.ChangeState(new MeleeAttackState(enemy));
            return;
        }
        //3rd check if we can ranged attack
        if (senses.IsInShootingRange(target) && combat.CanRangedAttack())
        {
            stateMachine.ChangeState(new RangedAttackState(enemy));
            return;
        }
        //4th check if we reach our target
        float distance = Mathf.Abs(target.position.x - enemy.transform.position.x);
        if (distance <= config.turnTheshold)
        {
            rigidbody2.linearVelocity = Vector2.zero;
            return;
        }
        //5th check for obstacles
        if (senses.IsHittingWall() || senses.IsAtCliff())
        {
            rigidbody2.linearVelocity = Vector2.zero;
            return;
        }
        //6th we have a target, did not reached it, no obstacles
        stateMachine.ChangeState(new ChaseState(enemy));
    }
}
