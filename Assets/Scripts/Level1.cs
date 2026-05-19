using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.ChangeMusic(AudioManager.SoundType.Level1Music);
    }
}