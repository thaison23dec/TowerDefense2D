using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameState
{
    PrepareState,
    BattleState,
    EndState
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] public EnemyController Enemy;
    [SerializeField] public AllyController Ally;
    [SerializeField] public PrefabData PrefabData;
    private EnemyFactory enemyFactory;
    public Transform enemySpawner;
    public Transform allySpawner;
    public GameState CurrentState;

    public event Action OnStartPrepare;
    public event Action OnStartNewWave;
    public event Action OnStartEnd;

    public int CurrentCoin;

    protected override void Awake()
    {
        base.Awake();
        CurrentState = GameState.PrepareState;
        enemyFactory = new EnemyFactory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            WaveManager.Instance.InitWave();
        }
    }

    public void StartNewWave()
    {
        CurrentState = GameState.BattleState;
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
}
