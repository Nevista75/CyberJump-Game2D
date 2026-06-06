using UnityEngine;

public static class SaveManager
{
    private const string LEVEL_KEY = "HighestUnlockedLevel";

    public static int GetHighestUnlockedLevel()
    {
        return PlayerPrefs.GetInt(LEVEL_KEY, 1);
    }

    public static void UnlockLevel(int level)
    {
        int current = GetHighestUnlockedLevel();

        if (level > current)
        {
            PlayerPrefs.SetInt(LEVEL_KEY, level);
            PlayerPrefs.Save();
        }
    }

    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey(LEVEL_KEY);
    }
}