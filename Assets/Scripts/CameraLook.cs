using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    public Transform player;
    public Rigidbody playerRb;

    public float mouseSensitivity = 0.15f;
    public float eyeHeight = 1.6f;

    public float minPitch = -80f;
    public float maxPitch = 80f;

    private float horizontalRotation;
    private float verticalRotation;

    void Start()
    {
        horizontalRotation = player.eulerAngles.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Mouse.current == null)
            return;

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Vector2 mouseInput = Mouse.current.delta.ReadValue();

            horizontalRotation += mouseInput.x * mouseSensitivity;

            verticalRotation -= mouseInput.y * mouseSensitivity;

            verticalRotation = Mathf.Clamp(verticalRotation, minPitch, maxPitch);
        }

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void FixedUpdate()
    {
        // Rotate the Player horizontally with the camera
        if (playerRb != null)
        {
            playerRb.MoveRotation(Quaternion.Euler(0f, horizontalRotation, 0f));
        }
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        // Follow Player's position
        transform.position = player.position + Vector3.up * eyeHeight;

        // Apply camera rotation
        transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
    }
}
