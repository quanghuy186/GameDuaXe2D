using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapLevelManager : MonoBehaviour
{
    public static MapLevelManager Instance;

    [System.Serializable]
    public class Level
    {
        public string name;                  // Tên level
        public bool isUnlocked = false;      // Trạng thái đã mở khóa
        public GameObject levelItem;         // Tham chiếu tới ô level trong UI
        public Button btnLock;               // Nút khóa
        public Button btnUnlock;             // Nút mở khóa
    }

    public Level[] levels;                 // Danh sách các level
    //public TextMeshProUGUI playerLevelText; // Hiển thị cấp độ người chơi

    private int playerLevel;               // Cấp độ hiện tại của người chơi

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Giữ lại đối tượng này khi chuyển cảnh
        }
        else
        {
            Destroy(gameObject);  // Xóa nếu đã có instance khác
        }
    }

    void Start()
    {
        // Lấy cấp độ người chơi từ PlayerDataManager
        playerLevel = PlayerDataManager.Instance.GetPlayerLevel();
        //playerLevelText.text = playerLevel.ToString();
        Debug.Log("Cấp độ người chơi: " + playerLevel);  // Log cấp độ người chơi

        LoadLevelStates();
        PopulateLevels();
    }

    void LoadLevelStates()
    {
        // Đảm bảo level 1 luôn được mở khóa mặc định
        levels[0].isUnlocked = true;
        PlayerPrefs.SetInt(levels[0].name + "_isUnlocked", 1); // Lưu lại trạng thái mở khóa
        PlayerPrefs.Save();
        Debug.Log("Level 1 đã được mở khóa mặc định.");

        // Load trạng thái của các level khác từ PlayerPrefs
        for (int i = 1; i < levels.Length; i++) // Bắt đầu từ level 2
        {
            levels[i].isUnlocked = PlayerPrefs.GetInt(levels[i].name + "_isUnlocked", 0) == 1;
            Debug.Log($"Trạng thái level {levels[i].name}: {(levels[i].isUnlocked ? "Mở khóa" : "Khóa")}");
        }
    }

    void PopulateLevels()
    {
        foreach (var level in levels)
        {
            // Nếu level đã mở khóa
            if (level.isUnlocked)
            {
                SetLevelUnlockedUI(level);
            }
            else
            {
                SetLevelLockedUI(level);
            }
        }
    }

    void SetLevelLockedUI(Level level)
    {
        level.btnLock.gameObject.SetActive(true);
        level.btnUnlock.gameObject.SetActive(false);

        level.btnLock.onClick.RemoveAllListeners();
        level.btnLock.onClick.AddListener(() => ShowLockMessage());

        Debug.Log("Level " + level.name + " vẫn đang khóa.");
    }

    void SetLevelUnlockedUI(Level level)
    {
        level.btnLock.gameObject.SetActive(false);
        level.btnUnlock.gameObject.SetActive(true);

        level.btnUnlock.onClick.RemoveAllListeners();
        level.btnUnlock.onClick.AddListener(() => LoadLevel(level));

        Debug.Log("Level " + level.name + " đã mở khóa.");
    }

    void ShowLockMessage()
    {
        Debug.Log("Level này chưa mở khóa! Bạn cần hoàn thành các level trước.");
    }

    void LoadLevel(Level level)
    {
        Debug.Log("Đang vào level: " + level.name);
        // Logic để chuyển cảnh hoặc mở level tại đây
    }

    // Phương thức để người chơi thắng level và mở khóa level tiếp theo
    public void WinLevel(int levelIndex)
    {
        // Kiểm tra nếu level hiện tại đã mở khóa
        if (levelIndex < levels.Length && !levels[levelIndex].isUnlocked)
        {
            levels[levelIndex].isUnlocked = true;

            // Cập nhật trạng thái mở khóa level vào PlayerPrefs
            PlayerPrefs.SetInt(levels[levelIndex].name + "_isUnlocked", 1);
            PlayerPrefs.Save();
            Debug.Log("Đã mở khóa level " + levels[levelIndex].name);

            // Nếu người chơi hoàn thành level, cập nhật lại UI
            PopulateLevels();

            // Cập nhật cấp độ người chơi
            playerLevel++;
            PlayerDataManager.Instance.SetPlayerLevel(playerLevel);
            //playerLevelText.text = playerLevel.ToString();

            Debug.Log("Bạn đã thắng level " + levels[levelIndex].name + ". Mở khóa level " + (levelIndex + 2));
        }
        else
        {
            Debug.LogWarning("Không thể mở khóa level " + (levelIndex + 1) + " vì nó đã được mở khóa hoặc không hợp lệ.");
        }
    }
}
