using UnityEngine;
using UnityEngine.UI; // Để sử dụng UI
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Danh sách các prefab chướng ngại vật
    public List<GameObject> obstaclePrefabs;

    public float minTime = 1f;  // Thời gian tối thiểu giữa các lần xuất hiện chướng ngại vật
    public float maxTime = 3f;  // Thời gian tối đa giữa các lần xuất hiện chướng ngại vật
    public float spawnRangeX = 1.7f;  // Phạm vi ngẫu nhiên trên trục X
    public float spawnY = 10f;       // Vị trí cố định cho trục Y (vị trí cao trên màn hình)

    // Thời gian chơi và UI
    public float playTime = 60f;  // Thời gian chơi (60 giây)
    // public Text timerText;        // Text hiển thị thời gian còn lại
    public TMP_Text timerText;
    public GameObject winPanel;   // Bảng hiển thị chiến thắng

    private bool gameEnded = false; // Cờ kiểm tra xem game đã kết thúc hay chưa
    

    void Start()
    {
        // Bắt đầu gọi Coroutine để xuất hiện chướng ngại vật ngẫu nhiên
        StartCoroutine(SpawnObstacleRandomly());

        // Đặt lại thời gian chơi
        if (timerText != null)
        {
            timerText.text = "Time Left: " + playTime.ToString("F1") + "s";
        }

        // Ẩn bảng chiến thắng khi bắt đầu
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (!gameEnded)
        {
            // Giảm thời gian chơi theo thời gian thực
            playTime -= Time.deltaTime;

            // Cập nhật UI hiển thị thời gian
            if (timerText != null)
            {
                timerText.text = "Time Left: " + Mathf.Clamp(playTime, 0, playTime).ToString("F1") + "s";
            }

            // Kiểm tra nếu thời gian hết
            if (playTime <= 0)
            {
                EndGame();
            }
        }
    }

    IEnumerator SpawnObstacleRandomly()
    {
        while (!gameEnded) // Chỉ chạy nếu game chưa kết thúc
        {
            // Chọn một thời gian ngẫu nhiên trong khoảng minTime và maxTime
            float randomTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(randomTime); // Chờ trong thời gian ngẫu nhiên

            // Chọn một vị trí ngẫu nhiên trong phạm vi trục X và sử dụng một vị trí cố định cho trục Y
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnRangeX, spawnRangeX),
                spawnY,
                0f
            );

            // Chọn ngẫu nhiên một chướng ngại vật từ danh sách obstaclePrefabs
            if (obstaclePrefabs.Count > 0)
            {
                int randomIndex = Random.Range(0, obstaclePrefabs.Count);
                GameObject obstacleToSpawn = obstaclePrefabs[randomIndex];

                // Tạo chướng ngại vật tại vị trí ngẫu nhiên
                Instantiate(obstacleToSpawn, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Danh sách obstaclePrefabs rỗng! Hãy thêm ít nhất một prefab.");
            }
        }
    }

    void EndGame()
    {
        gameEnded = true; // Đặt cờ kết thúc game
        StopAllCoroutines(); // Dừng việc tạo chướng ngại vật

        // Hiển thị bảng chiến thắng
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }

}
