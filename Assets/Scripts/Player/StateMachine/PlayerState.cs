using UnityEngine;

public abstract class PlayerState 
{
    //core components
    protected Player player;
    protected Animator anim;
    protected Rigidbody2D rigidbody2;
    protected Combat combat;
    protected Magic magic;
    protected Damage damage;

    //input properties
    protected bool JumpPressed { get => player.jumpPressed; set => player.jumpPressed = value;  }
    protected bool JumpReleased { get => player.jumpReleased; set => player.jumpReleased = value; }
    protected bool RunPressed { get => player.runPressed; }
    protected bool AttackPressed { get => player.attackPressed; }
    protected bool SpellcastPressed { get => player.spellcastPressed; }
    protected Vector2 MoveInput { get => player.moveInput; }
    
    public PlayerState (Player player) //constructor
    {
        this.player = player;
        this.anim = player.anim;
        this.rigidbody2 = player.rigidbody2;
        this.combat = player.combat;
        this.magic = player.magic;
        this.damage = player.damage;
    }
    public virtual void Enter() { } //logic for starting state
    public virtual void Exit() { } //logic for ending state

    public virtual void Update() { } //logic for statechange conditions
    public virtual void FixedUpdate() { } //physic logic for state
    public virtual void AnimationFinished() { } //special case for state actions based on animation
}
