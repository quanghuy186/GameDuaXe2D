using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab của coin
    public float spawnInterval = 2f; // Thời gian giữa các lần spawn
    public Vector2 spawnAreaMin; // Góc dưới bên trái của khu vực spawn
    public Vector2 spawnAreaMax; // Góc trên bên phải của khu vực spawn

    void Start()
    {
        // Gọi hàm SpawnCoin liên tục sau mỗi spawnInterval giây
        StartCoroutine(SpawnCoinRoutine());
    }

    IEnumerator SpawnCoinRoutine()
    {
        while (true)
        {
            SpawnCoin();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCoin()
    {
        // Tạo vị trí ngẫu nhiên trong khu vực spawn
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector3 randomPosition = new Vector3(randomX, randomY, transform.position.z);

        // Tạo coin tại vị trí ngẫu nhiên
        Instantiate(coinPrefab, randomPosition, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        // Hiển thị khu vực spawn trong Scene view
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(spawnAreaMin.x, spawnAreaMin.y, transform.position.z),
                        new Vector3(spawnAreaMax.x, spawnAreaMin.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(spawnAreaMax.x, spawnAreaMin.y, transform.position.z),
                        new Vector3(spawnAreaMax.x, spawnAreaMax.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(spawnAreaMax.x, spawnAreaMax.y, transform.position.z),
                        new Vector3(spawnAreaMin.x, spawnAreaMax.y, transform.position.z));
        Gizmos.DrawLine(new Vector3(spawnAreaMin.x, spawnAreaMax.y, transform.position.z),
                        new Vector3(spawnAreaMin.x, spawnAreaMin.y, transform.position.z));
    }
}
