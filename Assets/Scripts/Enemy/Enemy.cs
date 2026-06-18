using UnityEngine;

public class Enemy: MonoBehaviour
{
    //Components
    public Rigidbody2D Rigidbody2 {  get; private set; }
    public StateMachine StateMachine { get; private set; }
    public EnemySenses Senses { get; private set; }
    public Animator Anim {  get; private set; }
    public EnemyCombat Combat { get; private set; }
    public EnemyConfig Config;

    //Variables
    public int Facing { get; private set; } = 1;
    public Transform CurrentTarget { get; set; }

    private void Awake()
    {
        Rigidbody2 = GetComponent<Rigidbody2D>();
        Senses = GetComponent<EnemySenses>();
        Anim = GetComponent<Animator>();
        Combat = GetComponent<EnemyCombat>();
        StateMachine = new StateMachine();
    }
    public void Start()
    {
        StateMachine.Initialize(new PatrolState(this));
    }
    private void Update() => StateMachine.CurrentState?.Update();
    private void FixedUpdate() => StateMachine.CurrentState?.FixedUpdate();
    public void OnAnimationFinished() => StateMachine.CurrentState?.OnAnimationFinished();
    public void FaceTarget(Transform target)
    {
        float offset = target.position.x - transform.position.x;
        int direction = offset > 0 ? 1 : -1;
        if (direction != Facing)
            Flip();
    }
    public void Flip() 
    { 
        Facing *= -1;

        Vector3 scale = transform.localScale;
        scale.x = Facing;
        transform.localScale = scale;
    }
}
