using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Dừng thời gian của trò chơi
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

     public void ExitToMainMenu()
    {
        Time.timeScale = 1f; // Đảm bảo thời gian trở lại bình thường
        SceneManager.LoadScene("MainScene"); // Tải scene MainMenu
    }
}
