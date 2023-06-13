using UnityEngine;

public class TripleJump : MonoBehaviour
{
    public float jumpForce = 5f;
    public int maxJumpCount = 3;

    private int jumpCount = 0;
    private bool canJump = true;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        jumpCount++;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        if (jumpCount >= maxJumpCount)
        {
            canJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            canJump = true;
        }
    }
}

