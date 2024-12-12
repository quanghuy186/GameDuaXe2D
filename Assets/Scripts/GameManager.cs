using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Tham chiếu đến các prefab của chướng ngại vật
    public GameObject obstaclePrefab1; // Chướng ngại vật 1
    public GameObject obstaclePrefab2; // Chướng ngại vật 2

    public float minTime = 1f;  // Thời gian tối thiểu giữa các lần xuất hiện chướng ngại vật
    public float maxTime = 3f;  // Thời gian tối đa giữa các lần xuất hiện chướng ngại vật
    public float spawnRangeX = 1.7f;  // Phạm vi ngẫu nhiên trên trục X
    public float spawnY = 10f;      // Vị trí cố định cho trục Y (vị trí cao trên màn hình)
    
    void Start()
    {
        // Bắt đầu gọi Coroutine để xuất hiện chướng ngại vật ngẫu nhiên
        StartCoroutine(SpawnObstacleRandomly());
    }

    // Coroutine để xuất hiện chướng ngại vật ngẫu nhiên sau một khoảng thời gian ngẫu nhiên
    IEnumerator SpawnObstacleRandomly()
    {
        while (true)
        {
            // Chọn một thời gian ngẫu nhiên trong khoảng minTime và maxTime
            float randomTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(randomTime); // Chờ trong thời gian ngẫu nhiên

            // Chọn một vị trí ngẫu nhiên trong phạm vi trục X và sử dụng một vị trí cố định cho trục Y (vị trí cao)
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnRangeX, spawnRangeX), // Vị trí ngẫu nhiên trên trục X
                spawnY, // Vị trí cố định trên trục Y (tít trên màn hình)
                0f  // Vị trí Z cố định (không cần thay đổi trong 2D)
            );

            // Chọn ngẫu nhiên giữa 2 chướng ngại vật
            GameObject obstacleToSpawn = Random.Range(0f, 1f) > 0.5f ? obstaclePrefab1 : obstaclePrefab2;

            // Tạo chướng ngại vật tại vị trí ngẫu nhiên
            Instantiate(obstacleToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
