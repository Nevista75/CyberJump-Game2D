using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.ChangeMusic(AudioManager.SoundType.MenuMusic);
    }

    public void PlayGame()
    {
        AudioManager.Instance.Play(AudioManager.SoundType.Click);
        Invoke(nameof(loadLevel1), 0.5f);
    }

    private void loadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Game Keluar");
    }

    public void BackToMainMenu()
    {
        AudioManager.Instance.Play(AudioManager.SoundType.Click);
        Invoke(nameof(LoadMenu), 0.5f);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}