using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float upperLimit = 2f;

    [SerializeField] private float verticalOffset = 1.5f;

    private Vector3 originalPosition;

    private Vector3 offset;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void LateUpdate()
    {
        float targetY = originalPosition.y;

        if (player.position.y > upperLimit)
        {
            targetY = player.position.y - verticalOffset;
        }

        transform.position = new Vector3(
            transform.position.x,
            targetY,
            transform.position.z
        );
    }
}