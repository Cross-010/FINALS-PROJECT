using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    public Transform playerCamera;

    public float mouseSensitivity = 0.15f;

    public float minPitch = -80f;
    public float maxPitch = 80f;

    private float rotationX;

    void Start()
    {
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

            float mouseX = mouseInput.x * mouseSensitivity;

            transform.Rotate(Vector3.up * mouseX);

            float mouseY = mouseInput.y * mouseSensitivity;

            rotationX -= mouseY;

            rotationX = Mathf.Clamp(rotationX,minPitch,maxPitch
            );

            playerCamera.localRotation =
                Quaternion.Euler(rotationX, 0f, 0f);
        }

        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
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
}