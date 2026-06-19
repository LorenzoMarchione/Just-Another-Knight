using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    private float horizontalJumpPercent = 0.5f;

    public PlayerWallJumpState (Player player) : base(player) { }

    public override void Enter() //stop momentum on enter and jump oposite from wall mor horizontally
    {
        anim.SetBool("isWallJumping", true);

        rigidbody2.linearVelocity = Vector2.zero;
        rigidbody2.linearVelocity = new Vector2(-player.facing * horizontalJumpPercent, 1f) * player.jumpForce;

        JumpPressed = false;
        JumpReleased = false;
    }
    public override void Update()
    {
        if (!player.isGrounded && player.isTouchingWall && MoveInput.x == player.facing && rigidbody2.linearVelocityY < 0)
            player.ChangeState(player.wallSlideState);
        else if (JumpPressed && player.isTouchingWall)
            player.ChangeState(player.wallJumpState); 
        else if (player.isGrounded && rigidbody2.linearVelocityY <= 0.1f)
            player.ChangeState(player.idleState);
    }
    public override void FixedUpdate()
    {
        player.ApplyVariablegravity();

        if (JumpReleased && rigidbody2.linearVelocityY > 0.1f)
        {
            rigidbody2.linearVelocityY = rigidbody2.linearVelocityY * player.jumpCutMult;
            JumpReleased = false;
        }
    }
    public override void Exit()
    {
        anim.SetBool("isWallJumping", false);
    }
}
