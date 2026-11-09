using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerInput playerInput;
    public Transform cameraTransform;

    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float rotationSpeed = 10f;
    public bool isGrounded;

    private InputAction moveAction;
    private InputAction jumpAction;
    private Rigidbody rb;

    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();

        if (jumpAction.WasPressedThisFrame() && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        Quaternion cameraRotation = Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0f);

       Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        float horizontal;
        float vertical;

        if (Mathf.Abs(moveInput.x) < 0.15f)
        {
            horizontal = 0f;
        }
        else
        {
            horizontal = moveInput.x;
        }

        if (Mathf.Abs(moveInput.y) < 0.15f)
        {
            vertical = 0f;
        }
        else
        {
            vertical = moveInput.y;
        }

        Vector3 move = forward * vertical + right * horizontal;

        rb.linearVelocity = new Vector3(move.x * moveSpeed, rb.linearVelocity.y, move.z * moveSpeed);

        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);

            Quaternion smoothRotation = Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            rb.MoveRotation(smoothRotation);
        }
    }

    void OnCollisionEnter(Collision collision)
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
