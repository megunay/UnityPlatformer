using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Character character;  //New Input System Variable
    private Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    

    //Character Variables
    private float horizontal;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpingPower = 7f;

    [Header("Dash")]
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing;
    [SerializeField] private float dashPower;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    private TrailRenderer dashTrail;
    [SerializeField] private float dashGravity;
    private float normalGravity;
    private float waitTime;

    //Animations
    private Animator anim;
    private SpriteRenderer spriteR;
    private enum movementState { idle, running, jumping, falling, dash };


    private void Awake()
    {
        character = new Character();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
        dashTrail = GetComponent<TrailRenderer>();
        normalGravity = rb.gravityScale;
        canDash = true;
    }

    private void OnEnable()
    {
        character.Enable();
    }

    private void OnDisable()
    {
        character.Disable();
    }

    void Update()
    {
        //Horizontal Movement
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        UpdateAnimation();

        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.C) && canDash)
        {
            StartCoroutine(Dash());
        }

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void Move(InputAction.CallbackContext context)
    {
        //Read Movement Value
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        //Variable Jump Height  -- Buggy??
        if(context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
    }

    //public void Dash(InputAction.CallbackContext context)    //Problematic
    //{
    //    if(context.performed && canDash)
    //    {
    //        StartCoroutine(Dash());
    //    }
    //}


    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if(horizontal > 0f)
        {
            rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        }
        else if(horizontal < 0f)
        {
            rb.velocity = new Vector2(-transform.localScale.x * dashPower, 0f);
        }
        dashTrail.emitting = true;
        yield return new WaitForSeconds(dashTime);
        dashTrail.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void UpdateAnimation()
    {
        movementState state;
        //Running Animation && Flip Character
        if (horizontal > 0f)
        {
            state = movementState.running;
            spriteR.flipX = false;
        }
        else if (horizontal < 0f)
        {
            state = movementState.running;
            spriteR.flipX = true;
        }
        else
        {
            state = movementState.idle;
        }
        anim.SetInteger("state", 0);

        //Jumping Animation
        if(rb.velocity.y > 0.1f)
        {
            state = movementState.jumping;
        }
        else if(rb.velocity.y < -0.1f)
        {
            state = movementState.falling;
        }

        anim.SetInteger("state", (int)state);

        //Dashing Animation
        if (isDashing)
        {
            state = movementState.dash;
        }
    }
}
