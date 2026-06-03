using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget;

    [SerializeField] private Transform cameraTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = teleportTarget.position;

            AudioManager.Instance.Play(AudioManager.SoundType.Portal);

            Camera.main.transform.position = new Vector3(
                cameraTarget.position.x,
                cameraTarget.position.y,
                Camera.main.transform.position.z
            );


        }
    }
}