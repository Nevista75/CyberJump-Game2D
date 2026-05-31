using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2 : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.ChangeMusic(AudioManager.SoundType.Level2Music);
    }
}