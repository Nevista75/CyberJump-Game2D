using UnityEngine;
using UnityEngine.InputSystem;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravityForce = 10f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private float moveInput;
    private bool gravityUp = false;
    private bool isGrounded;
    private Vector3 spawnPosition;
    private Vector3 cameraSpawnPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnPosition = transform.position;
        cameraSpawnPosition = Camera.main.transform.position;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    // Gerak kiri kanan
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>().x;
        if (isGrounded) AudioManager.Instance.PlayLong(AudioManager.SoundType.Move, value.Get<Vector2>().magnitude > 0);
    }

    // Toggle gravity dengan Spacebar
    public void OnToggleGravity(InputValue value)
    {
        gravityUp = !gravityUp;

        if (gravityUp)
        {
            rb.gravityScale = -gravityForce;
            transform.rotation = Quaternion.Euler(0, 0, 180);
            AudioManager.Instance.Play(AudioManager.SoundType.Jump);
        }
        else
        {
            rb.gravityScale = gravityForce;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            AudioManager.Instance.Play(AudioManager.SoundType.JumpAlt);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("Ball Mati");

            AudioManager.Instance.Play(AudioManager.SoundType.Hurt);

            Respawn();
        }
    }

     private void Respawn()
    {
        // Stop velocity
        rb.linearVelocity = Vector2.zero;

        // Balik ke posisi awal
        transform.position = spawnPosition;

        // Reset gravity
        gravityUp = false;

        rb.gravityScale = gravityForce;

        // Reset rotasi visual
        transform.rotation = Quaternion.Euler(0, 0, 0);

        // Reset kamera
        Camera.main.transform.position = cameraSpawnPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            
            AudioManager.Instance.Play(AudioManager.SoundType.Land);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}