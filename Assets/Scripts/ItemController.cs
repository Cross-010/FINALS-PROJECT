using UnityEngine;

public class ItemController : MonoBehaviour
{
    private Rigidbody rb;
    private Collider itemCollider;

    public bool isHeld = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        itemCollider = GetComponent<Collider>();
    }

    public void PickUp(Transform holdPoint)
    {
        isHeld = true;

        rb.isKinematic = true;
        itemCollider.enabled = false;

        transform.SetParent(holdPoint);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Throw(Vector3 direction, float force)
    {
        isHeld = false;

        transform.SetParent(null);

        itemCollider.enabled = true;
        rb.isKinematic = false;

        rb.linearVelocity = Vector3.zero;

        rb.AddForce(direction * force, ForceMode.Impulse);
    }
}