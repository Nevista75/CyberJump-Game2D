using UnityEngine;

public class RocketCameraFollow : MonoBehaviour
{
    public Transform player;

    private float offsetX;
    private Vector3 checkpointCameraPos;

    private void Start()
    {
        checkpointCameraPos = transform.position;

        if (player != null)
        {
            offsetX = transform.position.x - player.position.x;
        }
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(
                player.position.x + offsetX,
                transform.position.y,
                transform.position.z
            );
        }
    }

    public void ResetCamera()
    {
        transform.position = checkpointCameraPos;
    }
}