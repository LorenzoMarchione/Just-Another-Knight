using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState (Player player) : base (player) { }

    public override void Enter() //impulse on state enter
    {
        base.Enter();
        anim.SetBool("isJumping", true);

        rigidbody2.linearVelocityY = player.jumpForce;

        JumpPressed = false;
        JumpReleased = false;
    }
    public override void Update()
    {
        base.Update();
        if (!player.isGrounded && player.isTouchingWall && MoveInput.x == player.facing && rigidbody2.linearVelocityY < 0)
            player.ChangeState(player.wallSlideState);
        else if (JumpPressed && player.isTouchingWall)
            player.ChangeState(player.wallJumpState);
        else if (player.isGrounded && rigidbody2.linearVelocityY <= 0.1f)
            player.ChangeState(player.idleState);
    }
    public override void FixedUpdate() //variable gravity for jump and jump cancel on released input
    {
        base.FixedUpdate();
        
        player.ApplyVariablegravity();

        if(JumpReleased && rigidbody2.linearVelocityY > 0)
        {
            rigidbody2.linearVelocityY = rigidbody2.linearVelocityY * player.jumpCutMult;
            JumpReleased = false;
        }

        float speed = RunPressed ? player.runSpeed : player.walkSpeed;
        float targetSpeed = speed * MoveInput.x;
        rigidbody2.linearVelocityX = targetSpeed;
    }
    public override void Exit()
    {
        base.Exit();

        anim.SetBool("isJumping", false);
        player.ApplyVariablegravity();
    }
}
