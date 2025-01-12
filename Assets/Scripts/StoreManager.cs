using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    [System.Serializable]
    public class Car
    {
        public string name;           // Tên xe
        public Sprite image;          // Hình ảnh xe
        public int price;             // Giá xe
        public bool isPurchased;      // Trạng thái đã mua
        public bool isSelected;       // Trạng thái được chọn
        public GameObject carItem;    // Tham chiếu tới ô xe trong UI
        public Button buyButton;      // Nút mua
        public Button selectButton;   // Nút chọn
        public Button selectedButton; // Nút đã chọn
        public TextMeshProUGUI priceText; // Hiển thị giá xe
        public Image carImage;        // Hình ảnh xe
    }

    public Car[] cars;                       // Danh sách các xe trong cửa hàng
    public TextMeshProUGUI PlayerMoneyText;  // Hiển thị tiền người chơi
    private int playerMoney = 1000;          // Tiền mặc định của người chơi

    void Start()
    {
        // Tải dữ liệu từ PlayerPrefs
        playerMoney = PlayerPrefs.GetInt("PlayerMoney", 1000); // Tiền của người chơi
        PlayerMoneyText.text = playerMoney.ToString();         // Hiển thị tiền
        LoadCarStates();                                       // Tải trạng thái xe

        // Hiển thị trạng thái cửa hàng
        PopulateStore();
    }

    void PopulateStore()
    {
        // Cập nhật trạng thái từng xe trong cửa hàng
        foreach (var car in cars)
        {
            car.carImage.sprite = car.image;       // Hiển thị hình ảnh xe
            car.priceText.text = car.price.ToString(); // Hiển thị giá xe

            // Kiểm tra trạng thái xe
            if (car.isPurchased)
            {
                // Xe đã mua
                if (car.isSelected)
                {
                    // Xe đang được chọn
                    car.selectedButton.gameObject.SetActive(true);
                    car.selectButton.gameObject.SetActive(false);
                    car.buyButton.gameObject.SetActive(false);
                }
                else
                {
                    // Xe đã mua nhưng chưa được chọn
                    car.selectedButton.gameObject.SetActive(false);
                    car.selectButton.gameObject.SetActive(true);
                    car.buyButton.gameObject.SetActive(false);

                    car.selectButton.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
                    car.selectButton.onClick.RemoveAllListeners();
                    car.selectButton.onClick.AddListener(() => SelectCar(car));
                }
            }
            else
            {
                // Xe chưa mua
                car.selectedButton.gameObject.SetActive(false);
                car.selectButton.gameObject.SetActive(false);
                car.buyButton.gameObject.SetActive(true);

                car.buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Buy";
                car.buyButton.onClick.RemoveAllListeners();
                car.buyButton.onClick.AddListener(() => BuyCar(car));
            }
        }
    }

    void BuyCar(Car car)
    {
        // Kiểm tra nếu người chơi đủ tiền để mua xe
        if (playerMoney >= car.price)
        {
            playerMoney -= car.price;         // Trừ tiền
            car.isPurchased = true;          // Đánh dấu xe đã mua
            PlayerMoneyText.text = playerMoney.ToString(); // Cập nhật tiền hiển thị

            // Lưu dữ liệu
            PlayerPrefs.SetInt("PlayerMoney", playerMoney);
            PlayerPrefs.SetInt(car.name + "_isPurchased", 1);

            PopulateStore(); // Làm mới giao diện cửa hàng
        }
        else
        {
            Debug.Log("Không đủ tiền để mua xe!");
        }
    }

    void SelectCar(Car car)
    {
        // Bỏ chọn tất cả xe
        foreach (var c in cars)
        {
            c.isSelected = false;
            c.selectButton.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
            c.selectedButton.gameObject.SetActive(false);
        }

        // Chọn xe hiện tại
        car.isSelected = true;
        car.selectedButton.gameObject.SetActive(true);
        car.selectButton.gameObject.SetActive(false);
        car.buyButton.gameObject.SetActive(false);

        // Lưu xe đã chọn
        PlayerPrefs.SetString("SelectedCar", car.name);

        PopulateStore(); // Cập nhật giao diện
    }

    void LoadCarStates()
    {
        // Tải trạng thái đã mua và đã chọn cho từng xe
        bool anyCarSelected = false;

        foreach (var car in cars)
        {
            // Kiểm tra trạng thái mua
            if (PlayerPrefs.GetInt(car.name + "_isPurchased", 0) == 1)
            {
                car.isPurchased = true;
            }

            // Kiểm tra trạng thái chọn
            if (PlayerPrefs.GetString("SelectedCar", "") == car.name)
            {
                car.isSelected = true;
                anyCarSelected = true;
            }
        }

        // Nếu không có xe nào được chọn, chọn xe đầu tiên mặc định
        if (!anyCarSelected && cars.Length > 0)
        {
            cars[0].isSelected = true;
            PlayerPrefs.SetString("SelectedCar", cars[0].name);
        }
    }

    public void ResetStore()
    {
        // Đặt lại dữ liệu khi rời khỏi cửa hàng
        foreach (var car in cars)
        {
            car.isPurchased = false;
            car.isSelected = false;
        }
        playerMoney = 1000; // Đặt lại tiền mặc định
        PlayerPrefs.DeleteAll(); // Xóa toàn bộ dữ liệu đã lưu
        PlayerPrefs.Save();
        Debug.Log("Dữ liệu cửa hàng đã được đặt lại!");
    }
}
