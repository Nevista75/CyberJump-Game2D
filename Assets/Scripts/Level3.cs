using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3: MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.ChangeMusic(AudioManager.SoundType.Level3Music);
    }
}