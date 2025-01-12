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

      public void NextMap()
    {
        Time.timeScale = 1f; // Đảm bảo thời gian trở lại bình thường

        // Lấy chỉ số của bản đồ hiện tại
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Tính chỉ số bản đồ tiếp theo
        int nextSceneIndex = currentSceneIndex + 1;

        // Kiểm tra nếu đã là bản đồ cuối cùng
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // Quay về bản đồ đầu tiên
        }

        // Chuyển đến bản đồ tiếp theo
        SceneManager.LoadScene(nextSceneIndex);
    }
}
