using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool jumpPressed;
    private bool sideStepLeftPressed;
    private bool sideStepRightPressed;
    public float backwardMoveSpeed = 3.6f;
    public float forwardMoveSpeed = 4.8f;
    public float moveAcceleration = 45f;
    public float moveDeceleration = 70f;
    public float turnDeceleration = 95f;
    public float moveInputDeadzone = 0.1f;
    public float jumpSpeed = 5f;
    public float sideStepSpeed = 30f;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Check for horizontal movement input
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // Check for jump input
    public void OnJump(InputValue value)
    {
        float v = 0f;
        // Button actions may be represented as float (0/1)
        try { v = value.Get<float>(); } catch { }
        if (v > 0.5f && isGrounded)
        {
            jumpPressed = true;
        }
    }

    // Check for side step left input
    public void OnSideStepLeft(InputValue value)
    {
        if (isGrounded)
        {
            sideStepLeftPressed = true;
        }
    }

    // Check for side step right input
    public void OnSideStepRight(InputValue value)
    {
        if (isGrounded)
        {
            sideStepRightPressed = true;
        }
    }

    void FixedUpdate()
    {
        Vector2 velocity = rigidBody2D.linearVelocity;

        // Only allow movement if the player is grounded
        if (isGrounded)
        {
            if (sideStepLeftPressed)
            {
                velocity.x = -sideStepSpeed;
                sideStepLeftPressed = false;
            }
            else if (sideStepRightPressed)
            {
                velocity.x = sideStepSpeed;
                sideStepRightPressed = false;
            }
            else
            {
                // Calculate the target horizontal speed based on horizontal input
                float horizontalInput = Mathf.Abs(moveInput.x) >= moveInputDeadzone ? moveInput.x : 0f;
                float targetSpeed = horizontalInput * ((horizontalInput > 0.01f) ? forwardMoveSpeed : backwardMoveSpeed);

                // Determine the appropriate acceleration rate
                float accelerationRate;
                bool noDirectionalInput = Mathf.Abs(targetSpeed) <= 0.01f;
                bool turningAround = !noDirectionalInput && Mathf.Sign(targetSpeed) != Mathf.Sign(velocity.x) && Mathf.Abs(velocity.x) > 0.01f;

                if (noDirectionalInput)
                {
                    accelerationRate = moveDeceleration;
                }
                else if (turningAround)
                {
                    accelerationRate = turnDeceleration;
                }
                else
                {
                    accelerationRate = moveAcceleration;
                }

                // Apply the velocity to the rigidbody
                velocity.x = Mathf.MoveTowards(velocity.x, targetSpeed, accelerationRate * Time.fixedDeltaTime);
            }

            // Give the rigidbody vertical velocity if the jump button was pressed
            if (jumpPressed)
            {
                velocity.y = jumpSpeed;
                jumpPressed = false;
                isGrounded = false;
            }
        }

        rigidBody2D.linearVelocity = velocity;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}