using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    public static GameLoop instance;
    public GameObject gameOverUI;
    private TMP_Text scoreText;
    private TMP_Text highScoreText;
    private TMP_Text currentScore;
    private GameObject player;
    public int score = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        instance = this;
        gameOverUI = GameObject.Find("GameOver");
        scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
        highScoreText = GameObject.Find("HighScore").GetComponent<TMP_Text>();
        currentScore = GameObject.Find("ScoreText").GetComponent <TMP_Text>();
        player = GameObject.Find("PlayerArmature");
        gameOverUI.SetActive(false);
    }

    public void AddScore()
    {
        score++;
        currentScore.text = "Current Score: " + score;
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        player.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (PlayerPrefs.GetInt("HighScore") < score)
                PlayerPrefs.SetInt("HighScore", score);
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        gameOverUI.SetActive(true);
        scoreText.text = "Your Score: " + score;
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
