using UnityEngine;

public class CarController : MonoBehaviour {
    public float moveSpeed = 10f;      // Tốc độ di chuyển sang trái/phải
    public float smoothMoveTime = 0.1f; // Thời gian làm mượt chuyển động

    public float minX = -1.7f;   // Giới hạn bên trái
    public float maxX = 1.7f;    // Giới hạn bên phải

    public bool isDead = false;


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

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag(Const.OBSTACLE_TAG)){
            gameObject.SetActive(false);
            col.gameObject.SetActive(false);
            isDead = true;
        }
    }
}

