using UnityEngine;
using UnityEngine.InputSystem;

public class BallMovement : PlayerRespawn
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravityForce = 10f;

    private float moveInput;
    private bool gravityUp = false;
    private bool isGrounded;

    public GameObject deadEffectPrefab;
    public float lifetimeLedakan = 2f;
    private GameObject currentLedakan;

    private void FixedUpdate()
    {
        if (isDead) return;
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    // Gerak kiri kanan
    public void OnMove(InputValue value)
    {
        if (isDead) return;

        moveInput = value.Get<Vector2>().x;
        
        if (isGrounded) AudioManager.Instance.PlayLong(AudioManager.SoundType.Move, value.Get<Vector2>().magnitude > 0);
    }

    // Toggle gravity dengan Spacebar
    public void OnToggleGravity(InputValue value)
    {
        if (isDead) return;

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
            Die();
        }
    }

    public override void Die()
    {
        if (isDead) return;

        if (deadEffectPrefab != null)
        {
            currentLedakan = Instantiate(deadEffectPrefab, transform.position, Quaternion.identity);
            Destroy(currentLedakan, lifetimeLedakan);
        }

        base.Die();
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

    protected override void Respawn()
    {
        if (currentLedakan != null)
        {
            Destroy(currentLedakan);
        }

        transform.position = spawnPosition;
        rb.linearVelocity = Vector2.zero;
        moveInput = 0f;

        gravityUp = false;
        isGrounded = false;

        rb.gravityScale = gravityForce;
        transform.rotation = Quaternion.identity;
        Camera.main.transform.position = cameraSpawnPosition;
    }
}