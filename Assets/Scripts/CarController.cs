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

    void Start()
    {
        targetPositionX = transform.position.x;
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
            gameObject.SetActive(false);
            col.gameObject.SetActive(false);
            gameOverManager.ShowGameOver();
        }
    }
}
