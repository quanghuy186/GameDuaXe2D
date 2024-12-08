using UnityEngine;
using UnityEngine.SceneManagement; // Để quản lý chuyển cảnh

public class MenuMapController : MonoBehaviour
{
    // Hàm này sẽ được gọi khi người chơi chọn một level
    public void StartLevel(int level)
    {
        // Chuyển sang scene của level tương ứng
        SceneManager.LoadScene("MapLevel" + level);
    }
}
