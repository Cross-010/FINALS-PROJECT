using UnityEngine;
using UnityEngine.UIElements;

public class ObjectAvoidScript : MonoBehaviour
{
    public float targetVelocity = 10f;
    public int numberOfRays = 17;
    public float angle = 180;

    public float rayRange = 2;

    private void Update()
    {
        var deltaPosition = Vector3.zero;
        for (int i = 0; i < numberOfRays; i++)
        {
            var rot = this.transform.rotation;
            var rotMod = Quaternion.AngleAxis((i / ((float)numberOfRays - 1)) * angle - angle, this.transform.up);
            var dir = rot * rotMod * Vector3.forward;

            var ray = new Ray(this.transform.position, dir);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayRange))
            {
                deltaPosition -= (1.0f / numberOfRays) * targetVelocity * dir;
            }
            else
            {
                deltaPosition += (1.0f / numberOfRays) * targetVelocity * dir;
            }
        }

        this.transform.position += deltaPosition * Time.deltaTime;
    }
    void OnDrawGizmos()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            var rot = this.transform.rotation;
            var rotMod = Quaternion.AngleAxis((i / ((float)numberOfRays - 1)) * angle - angle, this.transform.up);
            var dir = rot * rotMod * Vector3.forward;
            Gizmos.DrawRay(this.transform.position, dir);
        }
    }
}