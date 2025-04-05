using UnityEngine;

public class CubeCollectible : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.CubeCollected();
            Destroy(gameObject);
        }
    }
}
