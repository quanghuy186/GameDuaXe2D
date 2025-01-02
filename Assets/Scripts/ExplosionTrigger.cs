using UnityEngine;

public class ExplosionTrigger : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab hiệu ứng nổ

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle")) // Nếu va chạm với chướng ngại vật
        {
            // Tạo hiệu ứng nổ tại vị trí hiện tại
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Ẩn xe và chướng ngại vật
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
        }
    }
}
