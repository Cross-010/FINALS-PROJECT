using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public PlayerInput playerInput;
    public Camera playerCamera;
    public Transform holdPoint;

    public float pickupDistance = 3f;
    public float throwForce = 10f;

    public AudioSource audioSource;
    public AudioClip pickupSound;
    public AudioClip throwSound;

    private InputAction interactAction;
    private InputAction throwAction;

    private ItemController heldItem;

    void Start()
    {
        interactAction = playerInput.actions["Interact"];
        throwAction = playerInput.actions["Throw"];
    }

    void Update()
    {
        if (interactAction.WasPressedThisFrame())
        {
            PickUpItem();
        }

        if (throwAction.WasPressedThisFrame() && heldItem != null)
        {
            ThrowItem();
        }
    }

    void PickUpItem()
    {
        if (heldItem != null)
            return;

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, pickupDistance))
        {
            ItemController item =
                hit.collider.GetComponentInParent<ItemController>();

            if (item != null && !item.isHeld)
            {
                heldItem = item;
                heldItem.PickUp(holdPoint);
            }

            if (audioSource != null && pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }
       
        }
    }

    void ThrowItem()
    {
        Vector3 throwDirection = playerCamera.transform.forward;

        heldItem.Throw(throwDirection, throwForce);

        heldItem = null;

        if (audioSource != null && throwSound != null)
        {
            audioSource.PlayOneShot(throwSound);
        }
    }
}
