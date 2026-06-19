using UnityEngine;

public class DamagedState : State
{
    protected override string AnimBoolName => "isDamaged";
    private float knockbackVelocity;
    private float knockbackDuration;

    public DamagedState(Enemy enemy, int knockbackDirection) : base(enemy) 
    { 
        knockbackVelocity = knockbackDirection * config.knockbackForce;
    }

    public override void Enter()
    {
        base.Enter();
        knockbackDuration = config.knockbackDuration;
        rigidbody2.linearVelocityX = knockbackVelocity;
    }
    public override void FixedUpdate() //run knockback and change state at knockback end if it isnt falling
    {
        knockbackDuration -= Time.fixedDeltaTime;
        if(knockbackDuration <= 0)
        {
            rigidbody2.linearVelocityX = 0;
            if(!senses.IsAtCliff())
                stateMachine.ChangeState(new IdleState(enemy));
        }
    }
}
