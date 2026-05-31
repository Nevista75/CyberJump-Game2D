using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private float offsetX;
    private Vector3 checkpointCameraPos;

    private void Start()
    {
        checkpointCameraPos = transform.position;
        if (player != null)
        {
            // Menyimpan jarak awal antara kamera dan pemain
            offsetX = transform.position.x - player.position.x;
        }
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            // Kamera mengikuti posisi X pemain secara presisi
            transform.position = new Vector3(player.position.x + offsetX, transform.position.y, transform.position.z);
        }
    }

    public void ResetCamera()
    {
        transform.position = checkpointCameraPos;
    }
}