using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityForce;
    [SerializeField] private float fallBonus;
    [SerializeField] private float maxFallSpeed;

    [SerializeField] private float acceleration;
    [SerializeField] private float drag;

    [SerializeField] private float dashForce;
    [SerializeField] private float postDashTimer;
    private float currentDashTimer;

    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform bottomLeftCorner, bottomRightCorner;


    private float directionalInputs;
    private bool requestJump;
    private bool pressingJump;
    private bool requestDash;
    [HideInInspector] public Vector2 dashTilePosition;


    private Rigidbody2D rb;

    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump")) requestJump = requestDash = true;
        pressingJump = Input.GetButton("Jump");

        if      (Input.GetAxisRaw("Horizontal") > 0) directionalInputs = 1;
        else if (Input.GetAxisRaw("Horizontal") < 0) directionalInputs = -1;
        else    directionalInputs = 0;

        animator.SetFloat("Horizontal Velocity", Mathf.Abs(rb.velocity.x));
        animator.SetBool("Is Grounded", isGrounded);
    }

    private void FixedUpdate()
    {
        float horizontalVelocity = rb.velocity.x;
        float verticalVelocity = rb.velocity.y;

        isGrounded = Physics2D.OverlapArea(bottomLeftCorner.position, bottomRightCorner.position, 1 << LayerMask.NameToLayer("Ground"));

        // accelerate in the desired direction
        if ((directionalInputs > 0 && rb.velocity.x < moveSpeed) || (directionalInputs < 0 && rb.velocity.x > -moveSpeed))
        {
            horizontalVelocity = Mathf.Lerp(horizontalVelocity, directionalInputs * moveSpeed, acceleration);
        }

        if (currentDashTimer > 0)   // same deceleration as vertical speed if dashed
        {
            if (horizontalVelocity > 0) horizontalVelocity -= gravityForce * Time.fixedDeltaTime;
            else horizontalVelocity += gravityForce * Time.fixedDeltaTime;
        }
        else    // else normal deceleration
        {
            horizontalVelocity = Mathf.Lerp(horizontalVelocity, 0, drag);
            if (Mathf.Abs(horizontalVelocity) <= moveSpeed)    
            {
                
            }
        }

        // jump velocity
        if (requestJump)
        {
            requestJump = false;
            if (isGrounded)
            {
                verticalVelocity = jumpForce;
            }
        }

        // fall velocity
        if (verticalVelocity > 0 && !pressingJump && currentDashTimer <= 0)
        {
            verticalVelocity -= gravityForce * fallBonus * Time.fixedDeltaTime;
        }
        else
        {
            if (verticalVelocity > -maxFallSpeed)
            {
                verticalVelocity -= gravityForce * Time.fixedDeltaTime;
            }
        }

        rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);

        if (requestDash)
        {
            requestDash = false;
            if (!isGrounded && dashTilePosition != Vector2.zero)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce((dashTilePosition - (Vector2)transform.position).normalized * dashForce, ForceMode2D.Impulse);
                dashTilePosition = Vector2.zero;
                currentDashTimer = postDashTimer;
            }
        }

        if (currentDashTimer > 0)
        {
            currentDashTimer -= Time.fixedDeltaTime;
        }

        if (horizontalVelocity != 0)
        {
            if (horizontalVelocity > 0.001f) transform.localScale = new Vector3(1, 1, 1);
            else if (horizontalVelocity < -0.001f) transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
