using UnityEngine;

public class PlayerDeathState : PlayerState
{
    private float knockbackDuration;
    private float knockbackVelocity;
    private bool isTimeSlow;
    public PlayerDeathState (Player player) : base(player) { }

    public void SetParameters(int knocknackDirection)
    {
        knockbackVelocity = knocknackDirection * damage.knockbackForce;
    }
    public override void Enter() //same as damaged state with time slow for dramatic effect
    {
        base.Enter();
        Time.timeScale = 0.3f;
        isTimeSlow = true;
        anim.SetBool("isDead", true);

        knockbackDuration = damage.knockbackDuration;
        player.rigidbody2.linearVelocityX = knockbackVelocity;
    }
    public override void FixedUpdate() //normal time when knockback ends
    {
        knockbackDuration -= Time.deltaTime;
        if (knockbackDuration <= 0)
        {
            if (isTimeSlow)
            {
                Time.timeScale = 1;
                isTimeSlow = false;
            }
            if(player.isGrounded)
                player.rigidbody2.linearVelocity = Vector2.zero;
        }
    }
    public override void AnimationFinished()
    {
    }
    public override void Exit()
    {
        base.Exit();
        anim.SetBool("isDead", false);
    }
}
