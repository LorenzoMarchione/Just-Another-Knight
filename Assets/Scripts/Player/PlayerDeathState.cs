using UnityEngine;

public class PlayerDeathState : PlayerState
{
    private string deathScreen = "DeathScreen";
    private float knockbackDuration;
    private float knockbackVelocity;
    private SceneChanger changer;
    private bool isTimeSlow;
    public PlayerDeathState (Player player) : base(player) { }

    public void SetParameters(int knocknackDirection, SceneChanger changer)
    {
        knockbackVelocity = knocknackDirection * damage.knockbackForce;
        this.changer = changer;
    }
    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0.3f;
        isTimeSlow = true;
        anim.SetBool("isDead", true);

        knockbackDuration = damage.knockbackDuration;
        player.rigidbody2.linearVelocityX = knockbackVelocity;
    }
    public override void FixedUpdate()
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
        changer.sceneToLoad = deathScreen;
        changer.ChengeSceneNow();
    }
    public override void Exit()
    {
        base.Exit();
        anim.SetBool("isDead", false);
    }
}
