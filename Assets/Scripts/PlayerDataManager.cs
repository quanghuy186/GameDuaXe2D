using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;

    private const string MoneyKey = "PlayerMoney";
    private const string NameSelectedCarKey = "SelectedCar";
    private const string NamePurchasedCarsKey = "PurchasedCars";
    private const string OpenedLevelsKey = "OpenedLevels";
    private const string PlayerLevelKey = "PlayerLevel";  // Key lưu cấp độ người chơi

    private int playerMoney;
    private string selectedCar;  // Lưu tên xe thay vì đối tượng Car
    private List<string> purchasedCars;  // Lưu tên xe thay vì đối tượng Car
    private List<int> openedLevels;
    private int playerLevel;  // Lưu cấp độ người chơi

    private Dictionary<string, string> carImagePaths;  // Lưu đường dẫn hình ảnh các xe

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Giữ lại đối tượng này khi chuyển cảnh
            LoadData();  // Tải dữ liệu từ PlayerPrefs
        }
        else
        {
            Destroy(gameObject);  // Xóa nếu đã có instance khác
        }
    }

    // Lấy số tiền của người chơi
    public int GetPlayerMoney()
    {
        return playerMoney;
    }

    // Cập nhật số tiền của người chơi
    public void SetPlayerMoney(int amount)
    {
        playerMoney = amount;
        PlayerPrefs.SetInt(MoneyKey, playerMoney);
        PlayerPrefs.Save();
    }

    // Lấy tên xe hiện đang được chọn
    public string GetSelectedCar()
    {
        return PlayerPrefs.GetString(NameSelectedCarKey, "StartCar");
    }

    // Cập nhật xe đang chọn
    public void SetSelectedCar(string carName)
    {
        selectedCar = carName;
        PlayerPrefs.SetString(NameSelectedCarKey, selectedCar);
        PlayerPrefs.Save();
    }

    // Lấy danh sách xe đã mua
    public List<string> GetPurchasedCars()
    {
        return new List<string>(purchasedCars);
    }

    // Thêm xe vào danh sách xe đã mua
    public void AddPurchasedCar(string carName, string carImagePath)
    {
        if (!purchasedCars.Contains(carName))
        {
            purchasedCars.Add(carName);
            carImagePaths[carName] = carImagePath;  // Lưu đường dẫn hình ảnh của xe
            SavePurchasedCars();
        }
    }

    // Kiểm tra xem xe đã được mua chưa
    public bool IsCarPurchased(string carName)
    {
        return purchasedCars.Contains(carName);
    }

    // Lấy danh sách các level đã mở
    public List<int> GetOpenedLevels()
    {
        return new List<int>(openedLevels);
    }

    // Thêm level vào danh sách level đã mở
    public void AddOpenedLevel(int levelIndex)
    {
        if (!openedLevels.Contains(levelIndex))
        {
            openedLevels.Add(levelIndex);
            SaveOpenedLevels();
        }
    }

    // Kiểm tra xem level đã được mở chưa
    public bool IsLevelOpened(int levelIndex)
    {
        return openedLevels.Contains(levelIndex);
    }

    // Cập nhật cấp độ người chơi
    public int GetPlayerLevel()
    {
        return PlayerPrefs.GetInt(PlayerLevelKey, 1);  // Default is level 1
    }

    // Lưu cấp độ người chơi vào PlayerPrefs
    public void SetPlayerLevel(int level)
    {
        playerLevel = level;
        PlayerPrefs.SetInt(PlayerLevelKey, playerLevel);
        PlayerPrefs.Save();
    }

    // Lấy đường dẫn hình ảnh của xe đã mua
    public Sprite GetCarImage(string carName)
    {
        if (carImagePaths.ContainsKey(carName))
        {
            return Resources.Load<Sprite>(carImagePaths[carName]);
        }
        return null;  // Nếu không có, trả về null
    }

    // Tải dữ liệu người chơi từ PlayerPrefs
    private void LoadData()
    {
        playerMoney = PlayerPrefs.GetInt(MoneyKey, 0);  // Tiền mặc định là 0
        selectedCar = PlayerPrefs.GetString(NameSelectedCarKey, "StartCar");  // Xe mặc định là "StartCar"

        purchasedCars = LoadStringList(NamePurchasedCarsKey);
        if (purchasedCars.Count == 0) purchasedCars.Add("StartCar");  // Xe mặc định nếu không có dữ liệu

        openedLevels = LoadIntList(OpenedLevelsKey);
        if (openedLevels.Count == 0) openedLevels.Add(1);  // Level mặc định là level 1

        playerLevel = PlayerPrefs.GetInt(PlayerLevelKey, 1);  // Cấp độ mặc định là level 1

        // Lấy đường dẫn hình ảnh các xe đã mua
        carImagePaths = new Dictionary<string, string>();
        foreach (var carName in purchasedCars)
        {
            string imagePath = PlayerPrefs.GetString(carName + "_imagePath", "");
            if (!string.IsNullOrEmpty(imagePath))
            {
                carImagePaths[carName] = imagePath;
            }
        }
    }

    // Lưu danh sách xe đã mua
    private void SavePurchasedCars()
    {
        SaveStringList(NamePurchasedCarsKey, purchasedCars);
    }

    // Lưu danh sách level đã mở
    private void SaveOpenedLevels()
    {
        SaveIntList(OpenedLevelsKey, openedLevels);
    }

    // Helper: Tải danh sách chuỗi từ PlayerPrefs
    private List<string> LoadStringList(string key)
    {
        string rawData = PlayerPrefs.GetString(key, "");
        if (string.IsNullOrEmpty(rawData))
            return new List<string>();

        return new List<string>(rawData.Split(','));
    }

    // Helper: Tải danh sách số nguyên từ PlayerPrefs
    private List<int> LoadIntList(string key)
    {
        string rawData = PlayerPrefs.GetString(key, "");
        if (string.IsNullOrEmpty(rawData))
            return new List<int>();

        List<int> intList = new List<int>();
        foreach (var str in rawData.Split(','))
        {
            if (int.TryParse(str, out int value))
                intList.Add(value);
        }
        return intList;
    }

    // Helper: Lưu danh sách chuỗi vào PlayerPrefs
    private void SaveStringList(string key, List<string> list)
    {
        PlayerPrefs.SetString(key, string.Join(",", list));
        PlayerPrefs.Save();
    }

    // Helper: Lưu danh sách số nguyên vào PlayerPrefs
    private void SaveIntList(string key, List<int> list)
    {
        PlayerPrefs.SetString(key, string.Join(",", list));
        PlayerPrefs.Save();
    }
}
