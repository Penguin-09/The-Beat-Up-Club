using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool jumpPressed;
    private bool sideStepLeftPressed;
    private bool sideStepRightPressed;

    public float backwardMoveSpeed = 2f;
    public float forwardMoveSpeed = 3f;
    public float jumpSpeed = 5;
    public float sideStepSpeed = 30f;
    

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

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

    public void OnSideStepLeft(InputValue value)
    {
        sideStepLeftPressed = true;
    }

    public void OnSideStepRight(InputValue value)
    {
        sideStepRightPressed = true;
    }

    void FixedUpdate()
    {
        Vector3 velocity = rigidBody.linearVelocity;

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
        else if (isGrounded)
        {
            float moveSpeed = moveInput.x < 0 ? backwardMoveSpeed : forwardMoveSpeed;
            velocity.x = moveInput.x * moveSpeed;
        }

        if (jumpPressed)
        {
            velocity.y = jumpSpeed;
            jumpPressed = false;
            isGrounded = false;
        }

            rigidBody.linearVelocity = velocity;
        
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}