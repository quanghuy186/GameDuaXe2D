using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float speed;

     // Biến lưu giới hạn dưới
    public Transform limitBottom;

    public float minX = -2.5f;
    public float maxX = 2.5f;
    public float spawnY = 5f;

    // Hàm tạo lại chướng ngại vật ngẫu nhiên
   
    Rigidbody2D m_rb;

    private void Awake() {
        m_rb = GetComponent<Rigidbody2D>();
    }

     void RespawnObstacle()
    {
        float randomX = Random.Range(minX, maxX);
        transform.position = new Vector3(randomX, spawnY, 0);
    }

    // private void Update() {
    //     if (m_rb != null){
    //         m_rb.linearVelocity = Vector2.down * speed;
    //     }
    // }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y <= limitBottom.position.y)
        {
            RespawnObstacle();
        }
    }

   
}
