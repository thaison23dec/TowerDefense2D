using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] public EnemyController Enemy;
    [SerializeField] public AllyController Ally;
    [SerializeField] public PrefabData PrefabData;
    public Transform enemySpawner;
    public Transform allySpawner;

    public int CurrentCoin;

    protected override void Awake()
    {
        base.Awake();
        CurrentCoin = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AllyController a = SimplePool.Spawn<AllyController>(PoolType.Ally_Sword, allySpawner.position, allySpawner.rotation);
            a.Init();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            EnemyController e = ObjectPoolManager.Instance.SpawnObject(GameManager.Instance.PrefabData.EnemyOrcPrefab, enemySpawner.transform.position, Quaternion.identity);
            e.Init();
        }
    }
}
