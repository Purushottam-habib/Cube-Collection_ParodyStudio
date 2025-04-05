using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Assign Rigidbody
        if (rb == null)
        {
            Debug.LogError("Rigidbody component missing from player!");
        }
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down

        // Fixing the direction by swapping signs
        moveDirection = new Vector3(-moveX, 0, -moveZ).normalized * speed;

        if (Input.GetKeyDown(KeyCode.Space)) // Jump
        {
            Jump();
        }
    }


    void FixedUpdate()
    {
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z); // Move player
    }

    void Jump()
    {
        if (IsGrounded()) // Ensure player is on the ground
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f); // Raycast to check ground
    }
}
