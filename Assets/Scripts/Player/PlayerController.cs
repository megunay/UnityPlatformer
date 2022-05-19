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
    private Animator anim;
    private SpriteRenderer spriteR;

    //Character Variables
    private float horizontal;
    [SerializeField]private float moveSpeed = 8f;
    [SerializeField]private float jumpingPower = 7f;


    private void Awake()
    {
        character = new Character();
    }

    private void OnEnable()
    {
        character.Enable();
    }

    private void OnDisable()
    {
        character.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteR = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        //Horizontal Movement
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        UpdateAnimation();

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

    private void UpdateAnimation()
    {
        //Running Animation && Flip Character
        if (horizontal > 0f)
        {
            anim.SetBool("isRunning", true);
            spriteR.flipX = false;
        }
        else if (horizontal < 0f)
        {
            anim.SetBool("isRunning", true);
            spriteR.flipX = true;
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        //Jumping Animation
        if ()
    }
}
