using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour
{
    [SerializeField] private string targetScene;
    [SerializeField] private int unlockLevel = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.Play(AudioManager.SoundType.EndPortal);

            if (unlockLevel > 0)
            {
                SaveManager.UnlockLevel(unlockLevel);

                Debug.Log("Unlocked Level " + unlockLevel);
            }

            SceneManager.LoadScene(targetScene);
        }
    }
}