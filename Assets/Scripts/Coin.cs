using UnityEngine;

public class Coin : MonoBehaviour
{
    public float speed = 3f; // Tốc độ di chuyển của coin

    void Update()
    {
        // Coin di chuyển xuống theo trục Y
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Hủy coin khi chạm vào player
            Destroy(gameObject);
        }

        // Hủy coin khi ra khỏi màn hình để tránh rác bộ nhớ
        Debug.Log("Chạm vào: " + other.tag);

        if (other.CompareTag("Boundary"))
        {
            Debug.Log("Coin đã chạm vào Boundary");
            Destroy(gameObject);
        }

    }
}
