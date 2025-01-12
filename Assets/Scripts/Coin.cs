using UnityEngine;

public class Coin : MonoBehaviour
{
    public float speed = 3f; // Tốc độ di chuyển của coin
    public int coinValue = 1; // Giá trị của đồng xu


    void Update()
    {
        // Coin di chuyển xuống theo trục Y
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager.instance.AddCoin(coinValue);

            Destroy(gameObject);
        }

        if (other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }

    }
}
