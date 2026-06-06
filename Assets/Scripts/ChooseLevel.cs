using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevel : MonoBehaviour
{
    private string sceneToLoad;

    [Header("Buttons")]
    [SerializeField] private Button level2Button;
    [SerializeField] private Button level3Button;

    [Header("Sprites")]
    [SerializeField] private Sprite level2Unlocked;
    [SerializeField] private Sprite level2Locked;

    [SerializeField] private Sprite level3Unlocked;
    [SerializeField] private Sprite level3Locked;

    private void Start()
    {
        AudioManager.Instance.ChangeMusic(AudioManager.SoundType.MenuMusic);
        Debug.Log(
        "HighestUnlockedLevel = "+ SaveManager.GetHighestUnlockedLevel());
        UpdateLevelButtons();
    }

     private void UpdateLevelButtons()
    {
        int unlocked = SaveManager.GetHighestUnlockedLevel();

        // Level 2
        bool level2UnlockedState = unlocked >= 2;

        level2Button.interactable = level2UnlockedState;

        level2Button.GetComponent<Image>().sprite =
            level2UnlockedState
            ? level2Unlocked
            : level2Locked;

        // Level 3
        bool level3UnlockedState = unlocked >= 3;

        level3Button.interactable = level3UnlockedState;

        level3Button.GetComponent<Image>().sprite =
            level3UnlockedState
            ? level3Unlocked
            : level3Locked;
    }

    private void SetupLevel(
        Button button,
        Image image,
        bool unlocked,
        Sprite unlockedSprite,
        Sprite lockedSprite)
    {
        button.interactable = unlocked;

        image.sprite = unlocked? unlockedSprite: lockedSprite;
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

    public void ResetProgress()
    {
        AudioManager.Instance.Play(AudioManager.SoundType.Click);
        PlayerPrefs.DeleteAll();
        Debug.Log("Save Data Dihapus");
    }
}