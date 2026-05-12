using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private PlayerInput inputActions;

    private Vector2 moveInput;

    private Vector3 spawnPosition;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 25f;

    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        inputActions = new PlayerInput();

        spawnPosition = transform.position;
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
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
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
        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("Player Mati");

            rb.linearVelocity = Vector2.zero;

            transform.position = spawnPosition;
        }
    }
}