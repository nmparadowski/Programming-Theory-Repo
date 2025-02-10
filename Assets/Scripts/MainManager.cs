using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static bool isGameRunning = true;
    public static PowerupType currentPowerup;

    private int currentScore = 0;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI bestScoreText;
    [SerializeField]
    private TextMeshProUGUI powerupText;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button quitButton;


    private void Awake()
    {
        currentScore = 0;
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);

        bestScoreText.text = GameManager.Instance.GetBestScoreText();
        isGameRunning = true;
    }

    //Appends score
    public void AddScore(int score)
    {
        currentScore += score;
        scoreText.text = "Score: " + currentScore;
    }

    public void SetPowerup(PowerupType type)
    {
        powerupText.text = "Powerup: " + type.ToString();
    }

    //Shows gameOver panel
    public void GameOver()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        for (int i = enemies.Length - 1; i >= 0; i--)
        {
            Destroy(enemies[i].gameObject);
        }

        gameOverPanel.SetActive(true);
        isGameRunning = false;
        GameManager.Instance.SaveGameData(currentScore);
        bestScoreText.text = GameManager.Instance.GetBestScoreText();
    }

    //Restarts game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Exits game and goes back to title screen
    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
