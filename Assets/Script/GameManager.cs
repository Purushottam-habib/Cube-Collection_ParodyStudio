using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public float totalTime = 120f;
    public float fallThreshold = -10f;

    [Header("References")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI collectibleText;
    public TextMeshProUGUI gameOverText;
    public GameObject gameOverPanel;
    public Transform player;

    private float currentTime;
    private bool isGameOver = false;
    private int totalCubes;
    private int collectedCubes = 0;

    void Start()
    {
        currentTime = totalTime;
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);

        // Count all cubes with tag "Collectible"
        totalCubes = GameObject.FindGameObjectsWithTag("Collectible").Length;

        UpdateTimerUI();
        UpdateCollectibleUI();
    }

    void Update()
    {
        if (isGameOver) return;

        currentTime -= Time.deltaTime;
        UpdateTimerUI();

        if (currentTime <= 0f)
        {
            GameOver("⏰ Time's Up! Game Over!");
        }

        if (player != null && player.position.y < fallThreshold)
        {
            GameOver("💀 You Fell! Game Over!");
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = $"Time Left: {minutes:00}:{seconds:00}";
    }

    void UpdateCollectibleUI()
    {
        int remaining = totalCubes - collectedCubes;
        collectibleText.text = $"Collectibles Left: {remaining}";
    }

    public void CubeCollected()
    {
        collectedCubes++;
        UpdateCollectibleUI();

        if (collectedCubes >= totalCubes)
        {
            GameOver("🏆 All Cubes Collected! You Win!");
        }
    }

    public void GameOver(string message)
    {
        isGameOver = true;
        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = message;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
