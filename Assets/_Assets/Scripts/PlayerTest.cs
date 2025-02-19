using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerTest : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isPlaying = false;

    public Transform groundCheck;  // Tạo một EmptyObject đặt dưới chân nhân vật
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer; // Chỉ nhận diện mặt đất
    public GameObject player;
    private Vector3 startPosition;
    public Canvas menuCanvas;
    public Canvas gameplayCanvas;
    public Canvas gameOverCanvas;
    public GameObject ground;
    private bool isDead = false;
    private Vector3 groundStartPosition;
    public AudioClip gameOverSound; // Âm thanh game over
    public Button retryButton;     // Nút Retry
    public Button mainMenuButton;  // Nút Main Menu
    private AudioSource audioSource;
    public AudioClip CheckGround;
    public bool canJumpThroughGround = false;
    public float minX = -5f, maxX = 5f; // Giới hạn theo trục X
    public float minY = -3f, maxY = 3f; // Giới hạn theo trục Y
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = player.transform.position;
        groundStartPosition = ground.transform.position;
        menuCanvas.gameObject.SetActive(true);
        gameplayCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
       
        
    }

    void Update()
    {
        Move();

        // Kiểm tra nếu đang chơi thì tự động nhảy
        if (isPlaying && isGrounded)
        {
            Jump();
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

       
    }
    public void SwitchToGameplay()
    {
        menuCanvas.gameObject.SetActive(false);
        gameplayCanvas.gameObject.SetActive(true);
        gameOverCanvas.gameObject.SetActive(false);
        player.transform.position = groundStartPosition + new Vector3(0, 1, 0);
        // Đảm bảo nhân vật bắt đầu từ vị trí lưu trữ và nhảy tự do
        ground.transform.position = groundStartPosition;
        Jump();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        StartCoroutine(JumpEffect());
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"), true);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        Invoke("EnableCollision", 0.5f);  // Bật lại va chạm sau 0.5 giâ
    }

        public void PlayGame()
    {
        isPlaying = true;
        menuCanvas.gameObject.SetActive(false);
    }
    IEnumerator JumpEffect()
    {
        // Bước 1: Nén lại trước khi nhảy
        transform.localScale = new Vector3(1.2f, 0.8f, 1);
        yield return new WaitForSeconds(0.1f);

        // Bước 2: Giãn ra khi bay lên
        transform.localScale = new Vector3(0.8f, 1.2f, 1);
        yield return new WaitForSeconds(0.1f);

        // Bước 3: Trở lại bình thường
        transform.localScale = Vector3.one;
    }


    private void OnTriggerEnter2D(Collider2D other) // Nếu dùng 2D
    {
        if (other.CompareTag("Enemy") && !isDead) // Nếu va chạm với quái
        {
            isDead = true;
            SwitchToGameOver();
            // Xoay nhân vật 90 độ
            transform.rotation = Quaternion.Euler(0, 0, 180);

            // Bật trọng lực để rơi xuống đất
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 3;

            // Gọi Game Over sau khi rơi xuống đất
          
        }
        if (other.CompareTag("Ground"))
        {
            Debug.Log("Nhân vật đã xuyên qua ground");
        }

        if (TryGetComponent<PlayerTest>(out PlayerTest controller))
        {
            controller.enabled = false;
        }

    }
    void SwitchToGameOver()
    {
        // Ẩn canvas gameplay và hiện canvas game over
        gameplayCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(true);
        menuCanvas.gameObject.SetActive(false);
        // Dừng chuyển động của player (nếu có Rigidbody2D)
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
       
        // Thêm âm thanh hoặc hiệu ứng khi game over (nếu cần)
        Debug.Log("Game Over! Player collided with enemy.");
       
    }
  
    public void RetryGame()
    {
        // Tải lại cảnh hiện tại (Game Play)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            Debug.Log("Player chạm vào biên giới, đưa về vị trí hợp lệ");
            float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}

