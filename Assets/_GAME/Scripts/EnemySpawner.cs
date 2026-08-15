using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public Transform SpawnPos;
    [SerializeField] public List<WaypointPath> waypointPathList;
    [SerializeField] public List<EnemyGroup> enemyGroupList;
    private EnemyFactory enemyFactory;

    private void Awake()
    {
        enemyFactory = new EnemyFactory();
    }

    public void SpawnEnemy(EnemyGroup enemyGroup)
    {
        StartCoroutine(SpawnEnemyCoroutine(enemyGroup));
    }

    IEnumerator SpawnEnemyCoroutine(EnemyGroup enemyGroup)
    {
        for (int i = 0; i < enemyGroup.Quantity; i++)
        {
            yield return new WaitForSeconds(0.75f);
            EnemyController e = enemyFactory.Create(enemyGroup.enemyType, SpawnPos.position);
            e.Init();
            int rand = Random.Range(0, waypointPathList.Count);
            e.waypointPath = waypointPathList[rand];
        }
    }
}
