using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState (Player player) : base (player) { }
    public override void Enter()
    {
        anim.SetBool("isIdle", true);
        rigidbody2.linearVelocityX = 0f;
    }
    public override void Update()
    {
        base.Update();

        if (SpellcastPressed && magic.CanCast(magic.CurrentSpell))
            player.ChangeState(player.spellcastState);
        else if (AttackPressed && combat.CanAttack)
            player.ChangeState(player.attackState);
        else if (JumpPressed)
        {
            JumpPressed = false;
            player.ChangeState(player.jumpState);
        }
        else if (Mathf.Abs(MoveInput.x) > 0.1f)
            player.ChangeState(player.moveState);
        else if (MoveInput.y < -0.1f)
            player.ChangeState(player.crouchState);
        rigidbody2.linearVelocityX = 0;
    }
    public override void Exit()
    {
        anim.SetBool("isIdle", false);
    }
}
