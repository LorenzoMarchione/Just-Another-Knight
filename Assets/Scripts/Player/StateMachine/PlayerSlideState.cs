using UnityEngine;

public class PlayerSlideState : PlayerState
{
    private float slideTimer;
    private float slideStopTimer;

    public PlayerSlideState(Player player) : base(player) { }

    public override void Enter() //slide animation, change collider size and start slide duration, slide stop duration (similar to a slide cooldown)
    {
        base.Enter();

        slideTimer = player.slideDuration;
        slideStopTimer = 0;

        player.SetColliderHeight(player.slideHeight, player.slideOffset);
        anim.SetBool("isSliding", true);
        player.isSliding = true;
    }
    public override void Update() 
    {
        base.Update();

        if (JumpPressed && !player.CheckForCeiling())
            player.ChangeState(player.jumpState);
        else if (slideTimer > 0)
            slideTimer -= Time.deltaTime;
        else if (slideStopTimer <= 0)
            slideStopTimer = player.slideStopDuration;
        //time between slide end and change state to stop from entering another slide immediatly
        else
        {
            slideStopTimer -= Time.deltaTime;
            if(slideStopTimer <= 0)
            {
                if (player.CheckForCeiling() || MoveInput.y < -0.1)
                    player.ChangeState(player.crouchState);
                else
                    player.ChangeState(player.idleState);
            }
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (slideTimer > 0)
            rigidbody2.linearVelocityX = player.slideSpeed * player.facing;
        else
            rigidbody2.linearVelocityX = 0f;
    }
    public override void Exit()
    {
        base.Exit();

        player.SetColliderHeight(player.normalHeight, player.normalOffset);
        anim.SetBool("isSliding", false);
        player.isSliding = false;
    }
}
