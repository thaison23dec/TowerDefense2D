using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] List<WaveData> WaveList;
    [SerializeField] Transform enemySpawnPos;
    [SerializeField] int WaveAmount;
    [SerializeField] private List<EnemySpawner> enemySpawnerList;
    public int currentWaveIndex = 0;
    private EnemyFactory enemyFactory;

    protected override void Awake()
    {
        base.Awake();
        enemyFactory = new EnemyFactory();
        currentWaveIndex = 0;
    }

    public void InitWave()
    {
        currentWaveIndex++;
        for (int i = 1; i <= WaveAmount; i++)
        {
            if(currentWaveIndex == i)
            {
                foreach(EnemySpawner enemySpawner in enemySpawnerList)
                {
                    foreach(EnemyGroup enemyGroup in enemySpawner.enemyGroupList)
                    {
                        if(enemyGroup.WaveIndex == currentWaveIndex)
                        {
                            enemySpawner.SpawnEnemy(enemyGroup);
                        }
                    }
                }
            }
        }
    }
}
