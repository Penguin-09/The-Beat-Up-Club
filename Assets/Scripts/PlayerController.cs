using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
    private float horizontalInput;
    private bool isGrounded;
    private bool jumpPressed;

    public float moveSpeed = 4f;
    public float jumpSpeed = 6f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        if (isGrounded)
        {
            Vector3 velocity = rigidBody.linearVelocity;
            velocity.x = horizontalInput * moveSpeed;

            if (jumpPressed)
            {
                velocity.y = jumpSpeed;
                jumpPressed = false;
                isGrounded = false;
            }

            rigidBody.linearVelocity = velocity;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}