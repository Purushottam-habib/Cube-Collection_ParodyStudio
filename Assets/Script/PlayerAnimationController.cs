using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;  // Reference to Animator
    private Rigidbody rb;      // Reference to Rigidbody

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (rb == null || animator == null) return;

        // Get horizontal movement speed (ignoring Y-axis)
        float speed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;

        // Detect movement using input keys instead of just speed
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        // Detect falling state
        bool isFalling = rb.velocity.y < -0.1f; // Lower threshold for better detection

        //  animation parameters
        animator.SetFloat("Speed", speed); 
        animator.SetBool("IsRunning", isMoving); 
        animator.SetBool("IsFalling", isFalling);

        
    }
}
