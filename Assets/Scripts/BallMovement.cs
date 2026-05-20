using UnityEngine;
using UnityEngine.InputSystem;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravityForce = 10f;

    private Rigidbody2D rb;

    private float moveInput;
    private bool gravityUp = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    // Gerak kiri kanan
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>().x;
    }

    // Toggle gravity dengan Space
    public void OnToggleGravity(InputValue value)
    {
        gravityUp = !gravityUp;

        if (gravityUp)
        {
            rb.gravityScale = -gravityForce;
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            rb.gravityScale = gravityForce;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}