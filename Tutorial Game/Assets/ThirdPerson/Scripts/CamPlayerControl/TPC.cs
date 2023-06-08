using UnityEngine;

public class TPC : MonoBehaviour
{
    public float movementSpeed = 5f;

    //private Animator animator;
    private Rigidbody rb;
    private Transform mainCameraTransform;

    private void Start()
    {
        //animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        mainCameraTransform = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        // Get input axes
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction relative to camera
        Vector3 cameraForward = Vector3.Scale(mainCameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = (vertical * cameraForward + horizontal * mainCameraTransform.right).normalized;

        // Move the character
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);

        // Rotate the character towards the movement direction
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 10f * Time.fixedDeltaTime);
        }

        // Update animator parameters
    //    animator.SetFloat("Speed", movement.magnitude);
    }
}
