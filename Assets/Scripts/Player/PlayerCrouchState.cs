using UnityEngine;

public class PlayerCrouchState : PlayerState
{
    public PlayerCrouchState (Player player) : base (player) { }

    public override void Enter()
    {
        base.Enter();

        anim.SetBool("isCrouching", true);
        player.SetColliderHeight(player.slideHeight, player.slideOffset);
    }
    public override void Update()
    {
        base.Update();

        if (JumpPressed)
            player.ChangeState(player.jumpState);
        else if (MoveInput.y > -0.1f && !player.CheckForCeiling())
            player.ChangeState(player.idleState);
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(Mathf.Abs(MoveInput.x) > 0.1f)
            rigidbody2.linearVelocityX = player.facing * player.walkSpeed;
        else
            rigidbody2.linearVelocityX = 0;
    }
    public override void Exit()
    {
        base.Exit();

        anim.SetBool("isCrouching", false);
        player.SetColliderHeight(player.normalHeight, player.normalOffset);
    }
}
