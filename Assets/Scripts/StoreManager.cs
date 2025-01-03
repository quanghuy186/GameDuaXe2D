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
        public bool isPurchased;      // Trạng thái đã mua hay chưa
        public bool isSelected;       // Trạng thái được chọn hay không
        public GameObject carItem;    // Tham chiếu tới ô xe trong UI
        public Button buyButton;      // Nút mua
        public Button selectButton;   // Nút chọn
        public Button selectedButton; // Nút đã chọn
        public TextMeshProUGUI priceText; // Giá xe hiển thị
        public Image carImage;        // Hình ảnh xe
    }

    public Car[] cars;               // Danh sách xe
    public TextMeshProUGUI PlayerMoneyText;  // TextMeshProUGUI để hiển thị tiền của người chơi
    private int playerMoney = 1000;   // Tiền của người chơi

    void Start()
    {
        // Tải số tiền và trạng thái các xe từ PlayerPrefs
        playerMoney = PlayerPrefs.GetInt("PlayerMoney", 1000); // Lấy tiền người chơi (mặc định là 1000)
        PlayerMoneyText.text = playerMoney.ToString();

        // Tải trạng thái các xe (đã mua, đã chọn) từ PlayerPrefs
        LoadCarStates();

        // Hiển thị các xe trong cửa hàng
        PopulateStore();
    }

    void PopulateStore()
    {
        // Duyệt qua tất cả các xe và cập nhật giao diện
        foreach (var car in cars)
        {
            // Cập nhật giá và hình ảnh xe
            car.carImage.sprite = car.image;
            car.priceText.text = car.price.ToString();

            // Xử lý trạng thái của nút
            if (car.isPurchased)
            {
                // Nếu xe đã mua
                if (car.isSelected)
                {
                    // Nếu xe được chọn
                    car.selectedButton.gameObject.SetActive(true);  // Hiển thị nút "Selected"
                    car.selectButton.gameObject.SetActive(false);  // Ẩn nút "Select"
                    car.buyButton.gameObject.SetActive(false);     // Ẩn nút "Buy"
                }
                else
                {
                    // Nếu xe đã mua nhưng chưa được chọn
                    car.selectedButton.gameObject.SetActive(false); // Ẩn nút "Selected"
                    car.selectButton.gameObject.SetActive(true);    // Hiển thị nút "Select"
                    car.buyButton.gameObject.SetActive(false);      // Ẩn nút "Buy"
                    car.selectButton.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
                    car.selectButton.onClick.RemoveAllListeners();
                    car.selectButton.onClick.AddListener(() => SelectCar(car));
                }
            }
            else
            {
                // Nếu xe chưa mua
                car.selectedButton.gameObject.SetActive(false);  // Ẩn nút "Selected"
                car.selectButton.gameObject.SetActive(false);   // Ẩn nút "Select"
                car.buyButton.gameObject.SetActive(true);       // Hiển thị nút "Buy"
                car.buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Buy";
                car.buyButton.onClick.RemoveAllListeners();
                car.buyButton.onClick.AddListener(() => BuyCar(car));
            }
        }
    }

    void BuyCar(Car car)
    {
        if (playerMoney >= car.price) // Kiểm tra đủ tiền để mua xe
        {
            car.isPurchased = true; // Đánh dấu xe đã mua
            playerMoney -= car.price; // Trừ tiền
            PlayerMoneyText.text = playerMoney.ToString(); // Cập nhật tiền sau khi mua

            // Lưu lại trạng thái tiền và xe đã mua vào PlayerPrefs
            PlayerPrefs.SetInt("PlayerMoney", playerMoney);
            PlayerPrefs.SetInt(car.name + "_isPurchased", 1); // Lưu trạng thái đã mua của xe

            PopulateStore(); // Làm mới cửa hàng để hiển thị lại trạng thái xe
        }
        else
        {
            Debug.Log("Không đủ tiền để mua xe!");
        }
    }

    void SelectCar(Car car)
    {
        // Bỏ chọn tất cả xe và cập nhật lại UI
        foreach (var c in cars)
        {
            c.isSelected = false;
            c.selectButton.GetComponentInChildren<TextMeshProUGUI>().text = "Select"; // Cập nhật lại tên nút
            c.selectedButton.gameObject.SetActive(false); // Ẩn nút "Selected"
        }

        // Đánh dấu xe được chọn
        car.isSelected = true;
        car.selectedButton.gameObject.SetActive(true);  // Hiển thị nút "Selected"
        car.selectButton.gameObject.SetActive(false);  // Ẩn nút "Select"
        car.buyButton.gameObject.SetActive(false);     // Ẩn nút "Buy"
        PopulateStore(); // Làm mới cửa hàng để hiển thị trạng thái xe đã chọn

        // Lưu lại trạng thái xe đã chọn
        PlayerPrefs.SetString("SelectedCar", car.name);
    }

    void LoadCarStates()
    {
        // Duyệt qua các xe và tải trạng thái đã lưu từ PlayerPrefs
        bool anyCarSelected = false;
        foreach (var car in cars)
        {
            // Kiểm tra trạng thái đã mua của xe
            if (PlayerPrefs.GetInt(car.name + "_isPurchased", 0) == 1)
            {
                car.isPurchased = true; // Nếu đã mua thì đánh dấu là đã mua
            }

            // Kiểm tra trạng thái xe đã chọn
            if (PlayerPrefs.GetString("SelectedCar", "") == car.name)
            {
                car.isSelected = true; // Nếu xe này được chọn thì đánh dấu là đã chọn
                anyCarSelected = true; // Ghi nhận là đã có xe được chọn
            }
        }

        // Nếu không có xe nào được chọn, chọn xe mặc định (ví dụ xe đầu tiên)
        if (!anyCarSelected && cars.Length > 0)
        {
            cars[0].isSelected = true; // Chọn xe đầu tiên mặc định
            PlayerPrefs.SetString("SelectedCar", cars[0].name); // Lưu lại xe được chọn mặc định
        }
    }
}
