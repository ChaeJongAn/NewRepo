using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject clearPanel;
    public GameObject gameOverPanel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        clearPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void OnEnemyDeath()
    {
        clearPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnPlayerDeath()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}