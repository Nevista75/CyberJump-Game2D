using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
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

    public void QuitGame()
    {
        // Application.Quit();
        Debug.Log("Game Keluar");
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    public void BackToMainMenu()
    {
        AudioManager.Instance.Play(AudioManager.SoundType.Click);
        Invoke(nameof(ExecuteMenuScene), 0.5f);
    }

    private void ExecuteMenuScene()
    {
       SceneManager.LoadScene("ChooseLevel"); 
    }
}