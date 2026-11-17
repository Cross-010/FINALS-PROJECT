
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput playerInput;
    public CameraController cameraController;

    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float rotationSpeed = 120f;

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
            rb.linearVelocity = new Vector3
            (
                rb.linearVelocity.x, jumpForce, rb.linearVelocity.z
            );

            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 forward = cameraController.Forward;
        Vector3 right = cameraController.Right;

        Vector3 move = forward * moveInput.y + right * moveInput.x;

        if (move.sqrMagnitude > 1f)
            move.Normalize();

        rb.linearVelocity = new Vector3
        (
            move.x * moveSpeed, rb.linearVelocity.y, move.z * moveSpeed
        );

        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);

            Quaternion smoothRotation = Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime
            );

            rb.MoveRotation(smoothRotation);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}