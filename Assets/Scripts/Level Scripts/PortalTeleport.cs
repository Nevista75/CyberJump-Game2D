using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget;

    [SerializeField] private Transform cameraTarget;
    
    [SerializeField] private ScenePortal portal; 

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

            if (portal != null)
            {
                portal.LoadNextScene(portal.targetScene);
            }
            else
            {
                Debug.LogError("Komponen ScenePortal belum ada di GameObject!");
            }
        }
    }
}