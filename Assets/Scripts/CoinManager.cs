using UnityEngine;
using TMPro; // Thư viện TextMeshPro

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance; // Singleton để truy cập dễ dàng
    public TextMeshProUGUI coinText; // TextMeshPro để hiển thị số xu
    private int totalCoins = 0; // Tổng số xu

    private void Awake()
    {
        // Đảm bảo chỉ có một instance của CoinManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoin(int amount)
    {
        totalCoins += amount; // Cộng số xu
        UpdateCoinUI(); // Cập nhật giao diện
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + totalCoins; // Hiển thị tổng số xu
        }
    }
}
