using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    [SerializeField] List<WaveData> WaveList;
    [SerializeField] Transform enemySpawnPos;
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
        for (int i = 0; i < WaveList.Count; i++)
        {
            if(currentWaveIndex == i)
            {
                StartCoroutine(SpawnWaveCoroutine(WaveList[currentWaveIndex]));
            }
        }
    }

    IEnumerator SpawnWaveCoroutine(WaveData wave)
    {
        currentWaveIndex++;
        foreach (EnemyGroupData enemyGroupData in wave.GroupList)
        {
            for(int i = 0; i < enemyGroupData.GroupQuanity; i++)
            {
                yield return new WaitForSeconds(0.75f);
                EnemyController e = enemyFactory.Create(enemyGroupData.enemyType, enemySpawnPos.position);
                e.Init();
            }
            
        }
    }
}
