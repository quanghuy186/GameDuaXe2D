using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private bool isMuted = false;

    void Start()
    {
        // Kiểm tra trạng thái Mute từ PlayerPrefs
        if (PlayerPrefs.HasKey("Muted"))
        {
            isMuted = PlayerPrefs.GetInt("Muted") == 1;
            AudioListener.volume = isMuted ? 0 : 1;  // Điều chỉnh âm lượng của toàn bộ game
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleAudio();
        }
    }

    void ToggleAudio()
    {
        isMuted = !isMuted;

        // Thay đổi âm lượng toàn bộ game (kể cả âm thanh nổ và âm thanh khác)
        AudioListener.volume = isMuted ? 0 : 1;

        // Lưu trạng thái Mute vào PlayerPrefs
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }
}
