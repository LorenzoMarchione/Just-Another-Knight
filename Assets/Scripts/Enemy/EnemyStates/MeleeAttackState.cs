using UnityEngine;

public class MeleeAttackState : State
{
    protected override string AnimBoolName => "isAttacking1";

    public MeleeAttackState(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        base.Enter();
        rigidbody2.linearVelocity = Vector2.zero;
    }
    public override void OnAnimationFinished()
    {
        stateMachine.ChangeState(new IdleState(enemy));
    }
}
