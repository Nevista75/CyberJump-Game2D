using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    private string sceneToLoad;

    private void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ChangeMusic(AudioManager.SoundType.MenuMusic);
        }
    }

    public void PlayGame(string sceneName)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.Play(AudioManager.SoundType.Click);
        }

        sceneToLoad = sceneName;
        Invoke(nameof(ExecuteLoadScene), 0.5f);
    }

    private void ExecuteLoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}