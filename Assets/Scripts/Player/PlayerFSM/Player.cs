using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }

    public PlayerMoveState MoveState { get; private set; }

    public PlayerJumpState JumpState { get; private set; }

    public PlayerInAirState InAirState { get; private set; }
    
    public PlayerLandState LandState { get; private set; }

    [SerializeField] private PlayerData playerData;

    #endregion

    #region Components
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion

    #region Check Transforms
    [SerializeField] private Transform groundCheck;
    #endregion

    #region Other Variables
    public Vector2 currentVelocity { get; private set; }
    public int FacingDir { get; private set; }

    private Vector2 workSpace;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();

        FacingDir = 1;

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        currentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        workSpace.Set(velocity, currentVelocity.y);
        RB.velocity = workSpace;
        currentVelocity = workSpace;
    }

    public void SetVelocityY(float velocity)
    {
        workSpace.Set(currentVelocity.x, velocity);
        RB.velocity = workSpace;
        currentVelocity = workSpace;
    }

    #endregion

    #region Check Functions

    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public void FlipCheck(int xInput)
        {
            if(xInput != 0 && xInput != FacingDir)
            {
                Flip();
            }
        }

    

    #endregion

    #region Other Functions
    private void Flip()
    {
        FacingDir *= -1;
        transform.Rotate(0f, 180f, 0f);
        
    }

    #endregion



}
