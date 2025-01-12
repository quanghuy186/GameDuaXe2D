using UnityEngine;

public class BGLoop : MonoBehaviour
{
    public float speed;           
    public Transform bg1;         
    public Transform bg2;         
    private float m_ySize;        
    public bool isStart;   

    void Awake()
    {
        // Lấy chiều cao của hình nền dựa trên BoxCollider2D
        m_ySize = bg1.GetComponent<BoxCollider2D>().size.y * bg1.transform.localScale.y;

        // Đặt vị trí ban đầu của bg1 và bg2
        bg1.position = Vector3.zero;
        bg2.position = new Vector3(
            bg1.position.x,
            bg1.position.y + m_ySize,
            0f
        );
    }

    void Update()
    {
        // Nếu chưa bắt đầu, thoát khỏi Update
        if (!isStart) return;

        // Di chuyển toàn bộ hình nền xuống dưới
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Kiểm tra nếu bg1 đã ra khỏi màn hình, đặt lại vị trí
        if (bg1.position.y < -m_ySize)
        {
            bg1.position = new Vector3(
                bg2.position.x,
                bg2.position.y + m_ySize,  // Giảm khoảng trống để tránh bị hở
                0f
            );

            // Hoán đổi bg1 và bg2
            Transform temp = bg1;
            bg1 = bg2;
            bg2 = temp;
        }
    }
}
