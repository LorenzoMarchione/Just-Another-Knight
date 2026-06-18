using UnityEngine;

public class ChaseState : State
{
    private Transform target;
    protected override string AnimBoolName => "isWalking";

   public ChaseState(Enemy enemy) : base(enemy) { }

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
        //2nd check if is we can melee attack
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
        if(distance <= config.turnTheshold)
        {
            stateMachine.ChangeState(new IdleState(enemy));
            return;
        }
        //5th check for obstacles
        if(senses.IsHittingWall() || senses.IsAtCliff())
        {
            stateMachine.ChangeState(new IdleState(enemy));
        }
        //6th chase the target
        rigidbody2.linearVelocityX = config.chaseSpeed * enemy.Facing;
    }
    public override void Exit()
    {
        base.Exit();
        rigidbody2.linearVelocity = Vector2.zero;
    }
}
