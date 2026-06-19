using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState (Player player) : base (player) { }

    public override void Enter() //attack animation and stop momentum from previous state
    {
        base.Enter();

        anim.SetBool("isAttacking", true);
        rigidbody2.linearVelocityX = 0f;
    }
    public override void AnimationFinished() //change state when attack is finished
    {
        if (Mathf.Abs(MoveInput.x) > 0.1)
            player.ChangeState(player.moveState);
        else
            player.ChangeState(player.idleState);
    }
    public override void Exit()
    {
        base.Exit();

        anim.SetBool("isAttacking", false);
    }
}
