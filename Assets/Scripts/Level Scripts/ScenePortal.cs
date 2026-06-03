using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScenePortal : MonoBehaviour
{
    [SerializeField] public string targetScene;
    bool LoadCheck = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.Play(AudioManager.SoundType.EndPortal);

            LoadCheck = true;
        }
    }

    public void LoadNextScene(string sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(string sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (operation.progress <= 0.9f) Debug.Log("Load Progress: " + progress * 100f + "%");

            if (operation.progress >= 0.9f && LoadCheck == true)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}