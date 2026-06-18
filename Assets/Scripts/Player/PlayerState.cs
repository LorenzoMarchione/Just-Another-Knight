using UnityEngine;

public abstract class PlayerState 
{
    protected Player player;
    protected Animator anim;
    protected Rigidbody2D rigidbody2;
    protected Combat combat;
    protected Magic magic;
    protected Damage damage;

    protected bool JumpPressed { get => player.jumpPressed; set => player.jumpPressed = value;  }
    protected bool JumpReleased { get => player.jumpReleased; set => player.jumpReleased = value; }
    protected bool RunPressed { get => player.runPressed; }
    protected bool AttackPressed { get => player.attackPressed; }
    protected bool SpellcastPressed { get => player.spellcastPressed; }
    protected Vector2 MoveInput { get => player.moveInput; }
    
    public PlayerState (Player player)
    {
        this.player = player;
        this.anim = player.anim;
        this.rigidbody2 = player.rigidbody2;
        this.combat = player.combat;
        this.magic = player.magic;
        this.damage = player.damage;
    }
    public virtual void Enter() { }
    public virtual void Exit() { }


    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void AnimationFinished() { }
}
