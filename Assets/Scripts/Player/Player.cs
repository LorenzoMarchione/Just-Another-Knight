using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngineInternal;

public class Player : MonoBehaviour
{
    //Player also functions as a statemachine
    public PlayerState currentState;

    public PlayerWallJumpState wallJumpState;
    public PlayerIdleState idleState;
    public PlayerJumpState jumpState;
    public PlayerMoveState moveState;
    public PlayerCrouchState crouchState;
    public PlayerSlideState slideState;
    public PlayerAttackState attackState;
    public PlayerSpellcastState spellcastState;
    public PlayerWallSlideState wallSlideState;
    public PlayerDamagedState damagedState;
    public PlayerDeathState deathState;

    //input values
    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public bool runPressed;
    [HideInInspector] public bool jumpPressed;
    [HideInInspector] public bool jumpReleased;
    [HideInInspector] public bool attackPressed;
    [HideInInspector] public bool spellcastPressed;

    //wich direction is the player facing
    [HideInInspector] public int facing = 1;
    
    [Header("Relevant Layers")]
    public LayerMask floor;
    public LayerMask wall;

    //Main mechanics besides movement
    [Header("Core Components")]
    public Combat combat;
    public Magic magic;
    public Health health;

    [Header("Movement Settings")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    public float jumpCutMult;
    public float fallGravity;
    public float jumpGravity;
    public float normalGravity;

    [Header("Slide Settings")]
    public float slideDuration;
    public float slideSpeed;
    public float slideStopDuration = 0.15f;
    public float slideHeight;
    public Vector2 slideOffset;
    public float normalHeight;
    public Vector2 normalOffset;
    [HideInInspector] public bool isSliding;

    [Header("Ground Check Settings")]
    public bool isGrounded;
    public float groundCheckWidth;
    public float groundCheckHeight;
    private Vector2 groundedCheckSize;
    public Transform groundCheck;

    [Header("Crouch Check Settings")]
    public float headCheckRadius = 0.2f;
    public Transform headCheck;

    [Header("Wall Check Settings")]
    public float wallCheckRadius = 0.15f;
    public bool isTouchingWall;
    public Transform wallCheck;

    [Header("Inspector components")]
    public Damage damage;
    public Rigidbody2D rigidbody2;
    public Animator anim;
    public PlayerInput input;
    public SpriteRenderer sprite;
    public BoxCollider2D boxCollider;

    public void Awake()
    {
        //we instantiate all possible player states
        idleState = new PlayerIdleState(this);
        jumpState = new PlayerJumpState(this);
        moveState = new PlayerMoveState(this);
        crouchState = new PlayerCrouchState(this);
        slideState = new PlayerSlideState(this);
        attackState = new PlayerAttackState(this);
        spellcastState = new PlayerSpellcastState(this);
        wallJumpState = new PlayerWallJumpState(this);
        wallSlideState = new PlayerWallSlideState(this);
        damagedState = new PlayerDamagedState(this);
        deathState = new PlayerDeathState(this);

        //creating vector used for overlapBox using desired dimensions
        groundedCheckSize = new Vector2(groundCheckWidth, groundCheckHeight);
    }
    //start the game in idle state
    private void Start()
    {
        ChangeState(idleState);
    }
    void Update()
    {
        //run update method of current state
        currentState.Update(); 
        
        //this is to prevent changing directions while sliding
        Flip(); 
        AnimationHandler();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate(); //run fixedUpdate of current state

        CheckForGround(); //save bool to check for ground contact
        CheckForWall(); //same for walls
    }

    public void ChangeState(PlayerState newState) //control for stateMachine
    {
        if(currentState != null)
            currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
    private void OnMove(InputValue value) //reads move inputs on input detection and transforms it into maximum value
    {
        moveInput = value.Get<Vector2>();
        if (moveInput.x > 0.5)
        {
            moveInput.x = 1f;
        }
        else if (moveInput.x < -0.5)
        {
            moveInput.x = -1f;
        }
    }
    private void OnSprint(InputValue value) //reads sprint input on input detection
    {
        runPressed = value.isPressed;
    }
    private void OnJump(InputValue value)  //reads jump inputs on input detection and sends true only if can walljump or has enough vertical space and ground
    {
        if (value.isPressed)
        {
            if(isGrounded && !CheckForCeiling() || isTouchingWall)
                jumpPressed = true;
            jumpReleased = false;
        }
        else 
            jumpReleased = true;
    }
    public void OnAttack(InputValue value) //reads attack input on input detection
    {
        attackPressed = value.isPressed;
    }
    public void OnSpellcast(InputValue value) //reads spell use input on input detection
    {
        spellcastPressed = value.isPressed;
    }
    public void OnNext(InputValue value) //input for spell scrolling
    {
        if (value.isPressed)
            magic.NextSpell();
    }
    public void OnPrevious(InputValue value) //input for spell scrolling
    {
        if (value.isPressed)
            magic.PreviousSpell();
    }
    public void ApplyVariablegravity() //change gravity for better feeling while falling or jumping
    {
        if (rigidbody2.linearVelocityY < -0.1f)
            rigidbody2.gravityScale = fallGravity;
        else if (rigidbody2.linearVelocityY > 0.1f)
            rigidbody2.gravityScale = jumpGravity;
        else
            rigidbody2.gravityScale = normalGravity;
    }
    public void SetColliderHeight(float height, Vector2 offset) //change hitbox size and position based on values
    {
        boxCollider.size = new Vector2 (boxCollider.size.x, height);
        boxCollider.offset = offset;
    }
    private void Flip() //change playeObject horizontal direction based on input
    {
        if (currentState == deathState || isSliding)
            return;
        if(moveInput.x > 0.1f)
            facing = 1;
        if(moveInput.x < -0.1f)
            facing = -1;
        gameObject.transform.localScale = new Vector3(facing, 1, 1);
    }
    public void AnimationFinished() //run logic of finished animation for current state
    {
        currentState.AnimationFinished();
    }
    private void AnimationHandler() //multiple use animation variables
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rigidbody2.linearVelocityY);
    }
    private void CheckForGround() //check if player is touching the ground
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundedCheckSize, 0f, floor);
    }
    public bool CheckForCeiling() //check if player is right beaneath a ceiling
    {
        return Physics2D.OverlapCircle(headCheck.position, headCheckRadius, floor);
    }
    private void CheckForWall() //check if player is touching a wall
    {
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wall);
    }
    private void OnDrawGizmosSelected() //gizmos for visualizing raycasts
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(headCheck.position, headCheckRadius);

        Gizmos.color = Color.yellow;
        Vector2 target = new Vector2(transform.position.x + 5f * facing, transform.position.y);
        Gizmos.DrawWireSphere(target, 0.69f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheck.position, groundedCheckSize);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);
    }
}