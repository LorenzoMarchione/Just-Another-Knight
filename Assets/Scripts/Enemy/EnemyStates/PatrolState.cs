using UnityEngine;

public class PatrolState : State
{
    protected override string AnimBoolName => "isWalking";
    public PatrolState(Enemy enemy) : base(enemy) { }

    public override void FixedUpdate()
    {
        if(senses.GetChaseTarget())
        {
            stateMachine.ChangeState(new ChaseState(enemy));
            return;
        }
        if (senses.IsHittingWall() || senses.IsAtCliff() || senses.IsPushingAlly())
        {
            enemy.Flip();
            return;
        }
        rigidbody2.linearVelocityX = config.patrolSpeed * enemy.Facing;
    }

}
