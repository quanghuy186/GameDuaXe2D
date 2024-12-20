using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab của coin
    public float spawnInterval = 2f; // Thời gian giữa các lần spawn

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
        // Tạo coin tại vị trí của CoinSpawner
        Instantiate(coinPrefab, transform.position, Quaternion.identity);
    }
}
