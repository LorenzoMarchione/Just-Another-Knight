using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState (Player player) : base(player) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (SpellcastPressed && magic.CanCast(magic.CurrentSpell))
            player.ChangeState(player.spellcastState);
        else if (AttackPressed && combat.CanAttack)
            player.ChangeState(player.attackState);
        else if (JumpPressed)
            player.ChangeState(player.jumpState);
        else if (Mathf.Abs(MoveInput.x) < 0.1f)
            player.ChangeState(player.idleState);
        else if (player.isGrounded && RunPressed && MoveInput.y < -0.1f)
            player.ChangeState(player.slideState);
        else
            anim.SetBool("isRunning", true);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        float speed = RunPressed ? player.runSpeed : player.walkSpeed;
        rigidbody2.linearVelocityX = speed * player.facing;
    }

    public override void Exit()
    {
        base.Exit();

        anim.SetBool("isRunning", false);
    }
}
