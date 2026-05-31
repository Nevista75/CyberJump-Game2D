using UnityEngine;
using UnityEngine.InputSystem;

public enum Speeds { Slow = 0, Normal = 1, Fast = 2, Faster = 3, Fastest = 4 };

public class RocketMovement : MonoBehaviour
{
    public Speeds CurrentSpeed;

    //                        0      1      2       3      4
    float[] SpeedValues = { 5f, 10.4f, 12.96f, 15.6f, 19.27f };

    public Transform Sprite;
    public RocketCameraFollow cameraFollow;

    private Vector3 spawnPosition;

    Rigidbody2D rb;
    PlayerInput inputActions;

    int Gravity = 1;
    bool isHolding = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new PlayerInput();

        spawnPosition = transform.position;
        if (cameraFollow == null)
        {
            cameraFollow = Object.FindAnyObjectByType<RocketCameraFollow>();
        }
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Jump.performed += ctx => isHolding = true;
        inputActions.Player.Jump.canceled  += ctx => isHolding = false;
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void FixedUpdate()
    {
        // Gerak horizontal otomatis
        transform.position += Vector3.right * SpeedValues[(int)CurrentSpeed] * Time.fixedDeltaTime;

        // Clamp kecepatan jatuh maksimum
        if (rb.linearVelocity.y < -24.2f)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -24.2f);
            
        Rocket();
    }

    void Rocket()
    {
        float targetAngle = rb.linearVelocity.y * 2f;
        targetAngle = Mathf.Clamp(targetAngle, -45f, 45f);
        Sprite.rotation = Quaternion.Lerp(
            Sprite.rotation,
            Quaternion.Euler(0, 0, targetAngle),
            Time.fixedDeltaTime * 10f
        );

        if (isHolding)
            rb.gravityScale = -4.314969f;
        else
            rb.gravityScale = 4.314969f;
        rb.gravityScale *= Gravity;
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Obstacle")){
            Debug.Log("Player Mati");
            Die();
        } 
    }

    public void Die()
    {
        // Reset player
        transform.position = spawnPosition;

        // Reset velocity
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Reset kamera
        if (cameraFollow != null)
        {
            cameraFollow.ResetCamera();
        }
    }
}