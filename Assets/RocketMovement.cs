using UnityEngine;
using UnityEngine.InputSystem;

public enum Speeds { Slow = 0, Normal = 1, Fast = 2, Faster = 3, Fastest = 4 };

public class RocketMovement : MonoBehaviour
{
    public RocketData rocketData; 
    public Speeds CurrentSpeed;

    public Transform Sprite;
    public RocketCameraFollow cameraFollow;

    private Vector3 spawnPosition;

    Rigidbody2D rb;
    PlayerInput inputActions;

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
        transform.position += Vector3.right * rocketData.SpeedValues[(int)CurrentSpeed] * Time.fixedDeltaTime;
            
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
        rb.gravityScale *= rocketData.Gravity;
    }

    void OnCollisionEnter2D(Collision2D collision){
<<<<<<< HEAD
        if(collision.gameObject.CompareTag("Obstacle")){
=======
        if(collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("UFO")){
>>>>>>> Fathir
            Debug.Log("Player Mati");
            AudioManager.Instance.Play(AudioManager.SoundType.Hurt);
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

        // Reset semua UFO
        GameObject[] ufos = GameObject.FindGameObjectsWithTag("UFO");
        foreach (GameObject ufoObj in ufos)
        {
            UfoMovement ufo = ufoObj.GetComponent<UfoMovement>();
            if (ufo != null)
            {
                ufo.ResetPosition();
            }
        }
    }
}