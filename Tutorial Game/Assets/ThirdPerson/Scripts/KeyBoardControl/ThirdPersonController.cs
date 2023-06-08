using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 5f;

    private Animator animator;
    private Rigidbody rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Get input axes
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction based on input
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement.Normalize();

        // Move the character
        rb.MovePosition(rb.position + transform.TransformDirection(movement) * movementSpeed * Time.fixedDeltaTime);

        // Rotate the character towards the movement direction
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // Update animator parameters
        animator.SetFloat("Speed", movement.magnitude);
    }
}
