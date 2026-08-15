using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] public int WaveAmount;
    [SerializeField] private List<EnemySpawner> enemySpawnerList;

    public int currentWaveIndex = 0;

    public int aliveEnemyCount;

    protected override void Awake()
    {
        base.Awake();
        currentWaveIndex = 0;
        aliveEnemyCount = 0;
        UIManager.Instance.UpdateWaveIndexText(currentWaveIndex, WaveAmount);
    }

    public void InitWave()
    {
        StartCoroutine(InitWaveCoroutine());
    }

    IEnumerator InitWaveCoroutine()
    {
        currentWaveIndex++;
        UIManager.Instance.UpdateWaveIndexText(currentWaveIndex, WaveAmount);
        aliveEnemyCount = 0;
        UIManager.Instance.HideStartWaveBtn();
        AudioManager.Instance.PlayStartWave();
        foreach (EnemySpawner enemySpawner in enemySpawnerList)
        {
            foreach (EnemyGroup enemyGroup in enemySpawner.enemyGroupList)
            {
                if (enemyGroup.WaveIndex == currentWaveIndex)
                {
                    aliveEnemyCount += enemyGroup.Quantity;
                    yield return new WaitForSeconds(1f);
                    enemySpawner.SpawnEnemy(enemyGroup);
                }
            }
        }
    }

    public void OnEnemyDead()
    {
        aliveEnemyCount--;

        Debug.Log($"Enemy remaining: {aliveEnemyCount}");

        if (aliveEnemyCount <= 0)
        {
            OnWaveComplete();
        }
    }

    private void OnWaveComplete()
    {
        Debug.Log($"Wave {currentWaveIndex} Complete!");      
        if(currentWaveIndex == WaveAmount)
        {
            GameManager.Instance.CurrentState = GameState.Win;
            GameManager.Instance.EndGame();
        }
        UIManager.Instance.ShowStartWaveBtn();

    }
}
