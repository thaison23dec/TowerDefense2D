using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] private int WaveAmount;
    [SerializeField] private List<EnemySpawner> enemySpawnerList;

    public int currentWaveIndex = 0;

    public int aliveEnemyCount;

    protected override void Awake()
    {
        base.Awake();
        currentWaveIndex = 0;
        aliveEnemyCount = 0;
    }

    public void InitWave()
    {
        currentWaveIndex++;

        aliveEnemyCount = 0;
        UIManager.Instance.HideStartWaveBtn();
        foreach (EnemySpawner enemySpawner in enemySpawnerList)
        {
            foreach (EnemyGroup enemyGroup in enemySpawner.enemyGroupList)
            {
                if (enemyGroup.WaveIndex == currentWaveIndex)
                {
                    aliveEnemyCount += enemyGroup.Quantity;

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
        UIManager.Instance.ShowStartWaveBtn();

    }
}
