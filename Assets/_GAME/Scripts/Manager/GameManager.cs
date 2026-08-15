using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public enum GameState
{
    PrepareState,
    BattleState,
    EndState,
    Playing,
    Win,
    Lose
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] public PrefabData PrefabData;
    public GameState CurrentState;

    public event Action OnStartPrepare;
    public event Action OnStartNewWave;
    public event Action OnStartEnd;

    public int CurrentCoin;

    protected override void Awake()
    {
        base.Awake();
        Time.timeScale = 1f;
        CurrentState = GameState.Playing;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            WaveManager.Instance.InitWave();
        }
    }

    public void StartNewWave()
    {
        WaveManager.Instance.InitWave();
    }

    public void IncreaseCoin(int coin)
    {
        CurrentCoin += coin;
    }

    public bool DecreaseCoin(int coin)
    {
        if (CurrentCoin - coin < 0)
        {
            return false;
        }
        CurrentCoin -= coin;
        return true;
    }

    public void EndGame()
    {
        if(CurrentState == GameState.Playing)
        {
            return;
        }
        if(CurrentState == GameState.Lose)
        {
            Time.timeScale = 0f;
            UIManager.Instance.ShowLosePanel();
        }
        if(CurrentState == GameState.Win)
        {
            Time.timeScale = 0f;
            UIManager.Instance.ShowWinPanel();
            LevelManager.Instance.CompleteLevel(LevelManager.Instance.currentGameLevel);
            if (!LevelManager.Instance.IsUnlocked(LevelManager.Instance.currentGameLevel + 1))
            {
                LevelManager.Instance.UnlockLevel(LevelManager.Instance.currentGameLevel + 1);
            }
        }
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadNextLevel()
    {
        LevelManager.Instance.LoadNextLevel(LevelManager.Instance.currentGameLevel);
    }

    public void TryAgain()
    {

    }
}
