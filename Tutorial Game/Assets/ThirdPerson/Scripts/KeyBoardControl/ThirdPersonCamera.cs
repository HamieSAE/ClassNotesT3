using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 2f, -5f);
    public float rotationSpeed = 2f;
    public float damping = 5f;

    private Vector3 desiredPosition;
    private Quaternion desiredRotation;
    private Vector3 smoothedPosition;

    private void LateUpdate()
    {
        // Calculate desired position and rotation
        desiredPosition = target.position + offset;
        desiredRotation = Quaternion.Euler(target.eulerAngles.x, target.eulerAngles.y, 0f);

        // Smoothly move the camera towards the desired position and rotation
        smoothedPosition = Vector3.Lerp(smoothedPosition, desiredPosition, damping * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }
}
