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

    public event Action OnStartPrepareState;
    public event Action OnStartBattleState;
    public event Action OnStartEndState;

    public int CurrentCoin;

    protected override void Awake()
    {
        base.Awake();
        CurrentState = GameState.PrepareState;
        CurrentCoin = 0;
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

    public void StartWave()
    {
        CurrentState = GameState.BattleState;
        OnStartPrepareState();
        WaveManager.Instance.InitWave();
    }
}
