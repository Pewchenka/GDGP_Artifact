using UnityEngine;
using UnityEngine.InputSystem;


// Basic player movement

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private float horizontalInput;

    // Jump variables
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;

    [SerializeField] private float jumpSpeed = 8f;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float jumpBufferTime = 0.12f;

    private float lastGroundedTime = float.NegativeInfinity;
    private float lastJumpPressedTime = float.NegativeInfinity;

    [SerializeField, Range(0f, 1f)]
    private float jumpCutMultiplier = 0.5f;

    private bool jumpHeld;
    private bool jumpCutApplied;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = 0f;

        jumpHeld = false;

        if (Keyboard.current == null)
        {
            return;
        }

        jumpHeld = Keyboard.current.spaceKey.isPressed;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            lastJumpPressedTime = Time.time;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            horizontalInput -= 1f;
        }
        
        if (Keyboard.current.dKey.isPressed)
        {
            horizontalInput += 1f;
        }

    }

    private void FixedUpdate()
    {

        // Ground logic
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) != null;
        
        if (isGrounded && rb.linearVelocity.y <= 0f)
        {
            lastGroundedTime = Time.time;
        }

        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Jump with coyote time and jump buffering
        bool hasBufferedJump = Time.time - lastJumpPressedTime <= jumpBufferTime;

        bool canJump = Time.time - lastGroundedTime <= coyoteTime;

        if (hasBufferedJump && canJump)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpSpeed
            );

            jumpCutApplied = false;

            lastJumpPressedTime = float.NegativeInfinity;
            lastGroundedTime = float.NegativeInfinity;
        }

        // Jump cut logic: if the player releases the jump button while moving upwards, reduce the upward velocity
        if (!jumpHeld && !jumpCutApplied && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                rb.linearVelocity.y * jumpCutMultiplier
            );

            jumpCutApplied = true;
        }
    }
}
