using UnityEngine;

public class CarController : MonoBehaviour {
    public float moveSpeed = 10f;      // Tốc độ di chuyển sang trái/phải
    public float smoothMoveTime = 0.1f; // Thời gian làm mượt chuyển động

    public float minX = -2.8f;   // Giới hạn bên trái
    public float maxX = 2.9f;    // Giới hạn bên phải


    private float targetPositionX;     // Vị trí X mục tiêu
    private float currentVelocity = 0f;

    void Start() {
        // Lưu vị trí X ban đầu
        targetPositionX = transform.position.x;
    }

    void Update() {
        // Xác định hướng di chuyển dựa trên phím nhấn
        if (Input.GetKey(KeyCode.A)) {
            targetPositionX -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D)) {
            targetPositionX += moveSpeed * Time.deltaTime;
        }
        targetPositionX = Mathf.Clamp(targetPositionX, minX, maxX);
        // Làm mượt chuyển động sang trái/phải
        float newX = Mathf.SmoothDamp(transform.position.x, targetPositionX, ref currentVelocity, smoothMoveTime);
        
        // Cập nhật vị trí mới
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        
    }
}

