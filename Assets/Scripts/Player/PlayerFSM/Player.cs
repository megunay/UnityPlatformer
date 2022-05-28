using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }

    public PlayerMoveState MoveState { get; private set; }

    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Vector2 currentVelocity { get; private set; }
    public int FacingDir { get; private set; }

    [SerializeField] private PlayerData playerData;

    private Vector2 workSpace;

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
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

    public void SetVelocityX(float velocity)
    {
        workSpace.Set(velocity, currentVelocity.y);
        RB.velocity = workSpace;
        currentVelocity = workSpace;
    }

    private void Flip()
    {
        FacingDir *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
        
    }

    public void FlipCheck(int xInput)
    {
        if(xInput != 0 && xInput != FacingDir)
        {
            Flip();
        }
    }
}
