
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 Forward
    {
        get
        {
            return Quaternion.Euler(Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * Vector3.forward;
        }
    }

    public Vector3 Right
    {
        get
        {
            return Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * Vector3.right;
        }
    }
}