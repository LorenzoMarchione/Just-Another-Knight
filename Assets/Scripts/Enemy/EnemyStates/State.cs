using UnityEngine;

public abstract class State
{
    protected Enemy enemy;
    protected EnemyConfig config;
    protected EnemySenses senses;
    protected EnemyCombat combat;
    public StateMachine stateMachine;
    protected Rigidbody2D rigidbody2;
    protected Animator anim;
    protected virtual string AnimBoolName => null;

    protected State (Enemy enemy)
    {
        this.enemy = enemy;
        rigidbody2 = enemy.Rigidbody2;
        config = enemy.Config;
        senses = enemy.Senses;
        anim = enemy.Anim;
        stateMachine = enemy.StateMachine;
        combat = enemy.Combat;
    }

    public virtual void Enter() //run animation of state 
    {
        if(!string.IsNullOrEmpty(AnimBoolName)) 
            anim.SetBool(AnimBoolName, true);
    }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void OnAnimationFinished() { }
    public virtual void Exit() //stop animation of state
    {
        if (!string.IsNullOrEmpty(AnimBoolName))
            anim.SetBool(AnimBoolName, false);
    }
}
