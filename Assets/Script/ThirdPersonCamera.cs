using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;  // Player reference
    public Vector3 offset = new Vector3(0, 2, -5);  // Default offset behind the player
    public float smoothSpeed = 10f;  // Base smooth speed for movement
    public LayerMask collisionMask;  // Only the wall layer
    public float minRotationSpeed = 2f;  // Minimum camera rotation speed
    public float maxRotationSpeed = 10f;  // Maximum camera rotation speed

    private Vector3 currentOffset;
    private Rigidbody playerRigidbody;

    void Start()
    {
        currentOffset = offset;
        playerRigidbody = player.GetComponent<Rigidbody>();  // Get the Rigidbody component to access velocity
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Calculate the player's movement speed (magnitude of velocity)
        float playerSpeed = playerRigidbody.velocity.magnitude;

        // Adjust camera rotation speed based on player's speed (min and max limits)
        float adjustedRotationSpeed = Mathf.Lerp(minRotationSpeed, maxRotationSpeed, playerSpeed / 10f);

        // Desired camera position behind player
        Vector3 desiredPosition = player.position + player.TransformDirection(offset);

        // Check if there's any wall between player and camera
        RaycastHit hit;
        if (Physics.Raycast(player.position, (desiredPosition - player.position).normalized, out hit, offset.magnitude, collisionMask))
        {
            // Obstacle detected: bring camera closer
            float distance = hit.distance * 0.9f; // Slightly before the hit point
            desiredPosition = player.position + (desiredPosition - player.position).normalized * distance;
        }

        // Smooth movement to new position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Always look at player with adjusted rotation speed based on movement
        Vector3 lookDirection = player.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, adjustedRotationSpeed * Time.deltaTime);
    }
}
