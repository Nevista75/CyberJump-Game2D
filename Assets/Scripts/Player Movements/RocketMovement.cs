using UnityEngine;
using UnityEngine.InputSystem;


public enum Speeds { Slow = 0, Normal = 1, Fast = 2, Faster = 3, Fastest = 4 };

public class RocketMovement : PlayerRespawn
{
    public RocketData rocketData; 
    public Speeds CurrentSpeed;
    public Transform Sprite;
    public RocketCameraFollow cameraFollow;
    public ParticleSystem fireParticle;
    
    public GameObject deadEffectPrefab;
    public float lifetimeLedakan = 2f;

    PlayerInput inputActions;
    bool isHolding = false;
    private GameObject currentLedakan;

    protected override void Awake()
    {
        base.Awake();

        inputActions = new PlayerInput();

        if (cameraFollow == null)
        {
            cameraFollow = Object.FindAnyObjectByType<RocketCameraFollow>();
        }
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Jump.performed += ctx => isHolding = true;
        inputActions.Player.Jump.canceled  += ctx => isHolding = false;
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        // Gerak horizontal otomatis
        transform.position += Vector3.right * rocketData.SpeedValues[(int)CurrentSpeed] * Time.fixedDeltaTime;
            
        Rocket();
    }

    private void Rocket()
    {
        if (isDead) return;

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

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("UFO")){
            Debug.Log("Player Mati");
            Die();
        } 
    }

    public override void Die()
    {
        if (isDead) return; // Mencegah ledakan muncul berkali-kali

        if (fireParticle != null)
        {
            fireParticle.Stop(); // Hentikan partikel baru agar tidak keluar
            fireParticle.Clear(); // Hapus sisa partikel di layar seketika
        }

        if (deadEffectPrefab != null)
        {
            currentLedakan = Instantiate(deadEffectPrefab, transform.position, Quaternion.identity);
            Destroy(currentLedakan, lifetimeLedakan);
        }

        base.Die();


    }

    protected override void Respawn()
    {
        if (currentLedakan != null)
        {
            Destroy(currentLedakan);
        }

        if (fireParticle != null)
        {
            fireParticle.Play(); // Mainkan ulang partikel saat respawn
        }

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