using UnityEngine;

public class CarController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float smoothMoveTime = 0.1f;
    public float minX = -1.7f;
    public float maxX = 1.7f;

    private float targetPositionX;
    private float currentVelocity = 0f;

    // Tham chiếu đến GameOverManager
    public GameOverManager gameOverManager;
    public GameObject explosionEffectPrefab;  // Tham chiếu đến Prefab hiệu ứng nổ
    public AudioClip explosionSound;          // Tham chiếu đến âm thanh nổ

    void Start()
    {
        targetPositionX = transform.position.x;
        gameObject.SetActive(true); // Đảm bảo xe hiển thị khi bắt đầu
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            targetPositionX -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            targetPositionX += moveSpeed * Time.deltaTime;
        }

        targetPositionX = Mathf.Clamp(targetPositionX, minX, maxX);
        float newX = Mathf.SmoothDamp(transform.position.x, targetPositionX, ref currentVelocity, smoothMoveTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Obstacle"))
        {
            // Hiển thị hiệu ứng nổ tại vị trí của xe
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

            // Phát âm thanh nổ
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);

            // Ẩn đối tượng player (tạm thời)
            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            // Hiển thị Game Over
            gameOverManager.ShowGameOver();
        }
    }

    public void ResetCar()
    {
        // Hiển thị lại xe
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        targetPositionX = 0; // Reset vị trí về giữa
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }
}
