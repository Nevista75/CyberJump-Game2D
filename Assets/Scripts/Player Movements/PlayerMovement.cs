using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : PlayerRespawn
{
    private PlayerInput inputActions;
    private Vector2 moveInput;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 25f;

    private bool isGrounded;

    protected override void Awake()
    {
        base.Awake();
        inputActions = new PlayerInput();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Move.performed += Move;
        inputActions.Player.Move.canceled += Move;

        inputActions.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (isGrounded) AudioManager.Instance.PlayLong(AudioManager.SoundType.Move, !context.canceled);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (isDead) return;

        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            AudioManager.Instance.Play(AudioManager.SoundType.Jump);
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        if (moveInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("UFO"))
        {
            Debug.Log("Player Mati");
            Die();
        }
    }

    protected override void Respawn()
    {
        transform.position = spawnPosition;
        Camera.main.transform.position = cameraSpawnPosition;
        isGrounded = false;
    }

}