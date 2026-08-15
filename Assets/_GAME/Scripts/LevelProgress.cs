using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelProgress
{
    public static void CompleteLevel(int levelIndex)
    {
        PlayerPrefs.SetInt($"Level_{levelIndex}_Completed", 1);
        PlayerPrefs.SetInt($"Level_{levelIndex + 1}_Unlocked", 1);
        PlayerPrefs.Save();
    }

    public static bool IsLevelCompleted(int levelIndex)
    {
        return PlayerPrefs.GetInt($"Level_{levelIndex}_Completed", 0) == 1;
    }

    public static void UnlockedLevel(int levelIndex)
    {
        PlayerPrefs.SetInt($"Level_{levelIndex}_Unlocked", 1);
        PlayerPrefs.Save();
    }

    public static bool IsLevelUnlocked(int levelIndex)
    {
        return PlayerPrefs.GetInt($"Level_{levelIndex}_Unlocked", 0) == 1;
    }
}
