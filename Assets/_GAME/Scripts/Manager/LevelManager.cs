using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] public LevelDatabase LevelDatabase;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(int index)
    {
        Time.timeScale = 1f;
        LevelData level = LevelDatabase.levels[index - 1];
        GameManager.Instance.SetCurrentGameLevel(level.levelIndex);
        SceneManager.LoadScene(level.sceneName);
    }

    public void LoadNextLevel(int currentIndex)
    {
        int nextIndex = currentIndex + 1;

        if (nextIndex > LevelDatabase.levels.Count)
        {
            return;
        }

        LoadLevel(nextIndex);
    }

    public void CompleteLevel(int levelIndex)
    {
        PlayerPrefs.SetInt($"Level_{levelIndex}_Completed", 1);
        PlayerPrefs.Save();
    }

    public void UnlockLevel(int levelIndex)
    {
        PlayerPrefs.SetInt($"Level_{levelIndex}_Unlocked", 1);
        PlayerPrefs.Save();
    }

    public bool IsCompleted(int levelIndex)
    {
        return PlayerPrefs.GetInt(
            $"Level_{levelIndex}_Completed", 0
        ) == 1;
    }

    public bool IsUnlocked(int levelIndex)
    {
        if (levelIndex == 1)
            return true;

        return PlayerPrefs.GetInt(
            $"Level_{levelIndex}_Unlocked", 0
        ) == 1;
    }
}
