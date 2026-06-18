using UnityEngine;

public class PlayerDamagedState : PlayerState
{
    private float timer;
    private float knockbackDuration;
    private float knockbackVelocity;
    public PlayerDamagedState (Player player) : base (player) { }

    public void SetParameters(int knocknackDirection)
    {
        knockbackVelocity = knocknackDirection * damage.knockbackForce;
    }
    public override void Enter()
    {
        base.Enter();
        anim.SetBool("isDamaged", true);

        knockbackDuration = damage.knockbackDuration;
        player.rigidbody2.linearVelocityX = knockbackVelocity;
    }
    public override void FixedUpdate()
    {
        knockbackDuration -= Time.deltaTime;
        if(knockbackDuration <= 0 )
        {
            player.rigidbody2.linearVelocity = Vector2.zero;
            player.ChangeState(player.idleState);
        }
    }
    public override void Exit()
    {
        base.Exit();
        anim.SetBool("isDamaged", false);
    }
}
