using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    private string sceneToLoad;

    private void Start()
    {
        AudioManager.Instance.ChangeMusic(AudioManager.SoundType.MenuMusic);
    }

    public void PlayGame(string sceneName)
    {
        AudioManager.Instance.Play(AudioManager.SoundType.Click);

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