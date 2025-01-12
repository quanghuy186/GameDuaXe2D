using UnityEngine;
using UnityEngine.SceneManagement; // Để xử lý chuyển cảnh nếu cần

public class ButtonManager : MonoBehaviour
{
    // Hàm thoát game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game has been quit!"); // Debug kiểm tra trong Editor
    }

    // Hàm chuyển sang một Scene khác
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Hàm bật hoặc tắt một GameObject (ví dụ: bảng menu, hướng dẫn...)
    public void ToggleGameObject(GameObject target)
    {
        target.SetActive(!target.activeSelf);
    }

    // Hàm in thông báo (debug hoặc hành vi tuỳ chỉnh khác)
    public void PrintMessage(string message)
    {
        Debug.Log(message);
    }

}
