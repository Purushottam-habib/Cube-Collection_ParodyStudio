using UnityEngine;

public class GravityController : MonoBehaviour
{
    public Transform player;         // Player reference
    public GameObject holoObject;    // Hologram reference

    private Vector3 selectedGravity = Vector3.down; // Default gravity
    private Vector3 startPosition;   // Stores player's initial position
    private Rigidbody playerRb;

    void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
        if (playerRb == null)
        {
            Debug.LogError("Rigidbody missing on Player! Add a Rigidbody component.");
        }

        startPosition = player.position;  // Store initial position

        if (holoObject != null)
        {
            holoObject.SetActive(false);  // Hide hologram initially
        }
    }

    void Update()
    {
        // Gravity Selection
        if (Input.GetKeyDown(KeyCode.UpArrow)) selectedGravity = Vector3.forward;
        if (Input.GetKeyDown(KeyCode.DownArrow)) selectedGravity = Vector3.back;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) selectedGravity = Vector3.left;
        if (Input.GetKeyDown(KeyCode.RightArrow)) selectedGravity = Vector3.right;
        if (Input.GetKeyDown(KeyCode.PageUp)) selectedGravity = Vector3.up;
        if (Input.GetKeyDown(KeyCode.PageDown)) selectedGravity = Vector3.down;

        ShowHologram();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ApplyGravity();
        }

        // Reset Player when "R" is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPosition();
        }
    }

    void ShowHologram()
    {
        if (holoObject == null) return;
        holoObject.SetActive(true);
        holoObject.transform.position = player.position + selectedGravity.normalized * 2f;
    }

    void ApplyGravity()
    {
        Physics.gravity = selectedGravity * 9.81f;
        Debug.Log("Gravity changed to: " + Physics.gravity);

        if (playerRb != null)
        {
            playerRb.velocity = Vector3.zero; // Reset velocity
            playerRb.AddForce(selectedGravity * 2f, ForceMode.Impulse);
        }

        if (holoObject != null) holoObject.SetActive(false);
    }

    // ✅ Function to Reset Player Position
    public void ResetPosition()
    {
        player.position = startPosition;
        playerRb.velocity = Vector3.zero;
        Physics.gravity = Vector3.down * 9.81f; // Reset gravity to normal
        Debug.Log("Player Reset to Start Position!");
    }
}
