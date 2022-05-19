using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Character characterControls;
    private Rigidbody2D rb;

    private float inputX;

    [SerializeField] private float moveSpeed, jumpForce;

    public Transform groundPoint;
    public LayerMask whatIsGround;


    private bool isGrounded = true;

    public Animator playerAnim;


    private void OnEnable()
    {
        characterControls.Enable();
    }

    private void OnDisable()
    {
        characterControls.Disable();
    }

    private void Awake()
    {
        characterControls = new Character();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

        if(rb.velocity.x > 0f)
        {
            transform.localScale = Vector3.one;
            playerAnim.SetBool("isRunning", true);
        }
        else if(rb.velocity.x < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            playerAnim.SetBool("isRunning", true);
        }
        else if (rb.velocity.x == 0f)
        {
            playerAnim.SetBool("isRunning", false);
        }
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
