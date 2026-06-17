using UnityEngine;

public class UfoMovement : MonoBehaviour
{
    public UfoData data;
    
    [Header("UFO Settings")]
    public UfoMoveType moveType;
    public bool isActivated = false; // Ubah menjadi public agar bisa dicentang di Inspector

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (!isActivated) return;

        float currentSpeed = data.speed;
        
        // Fast mode
        if (moveType == UfoMoveType.Fast)
        {
            currentSpeed *= data.fastSpeedMultiplier;
        }

        // Gerak ke kiri
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);

        // Zigzag
        if (moveType == UfoMoveType.Zigzag)
        {
            float y = Mathf.Sin(Time.time * data.zigzagSpeed)
                    * data.zigzagHeight;

            transform.position = new Vector3(
                transform.position.x,
                startPos.y + y,
                transform.position.z
            );
        }
    }

    // Ufo jalan ketika trigger menyala
    public void Activate()
    {
        isActivated = true;
        Debug.Log("UFO telah diaktifkan dan mulai bergerak!");
    }

    // Mengembalikan UFO ke posisi awal dan mematikannya
    public void ResetPosition()
    {
        isActivated = false;
        transform.position = startPos;
        Debug.Log("UFO direset ke posisi awal");
    }
}