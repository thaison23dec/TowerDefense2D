using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : TowerBase
{
    [SerializeField] private Transform spawnPos;
    [SerializeField] public int TowerLevel;
    private TowerSpawnData towerSpawnData;

    [SerializeField] private int allyCount = 3;
    private List<Vector3Int> patrolCells;
    private List<AllyController> allies = new List<AllyController>();



    private void Update()
    {
        if (!HasAliveAlly())
        {
            
        }
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        foreach (AllyController a in allies)
        {
            a.OnDead -= HandleAllyDead;
            a.OnDespawn();

        }
    }

    public override void Init()
    {
        base.Init();
        towerSpawnData = towerData as TowerSpawnData;
        patrolCells = MapManager.Instance.FindPatrolCells(
            transform.position,
            allyCount,
            2
        );

        if (patrolCells == null)
        {
            return;
        }


        SpawnAllAlly();
    }


    private void SpawnAllAlly()
    {
        if (HasAliveAlly())
        {
            return;
        }
        allies.Clear();

        for (int i = 0; i < patrolCells.Count; i++)
        {
            AllyController ally = ObjectPoolManager.Instance.SpawnObject(towerSpawnData.allyPrefab, spawnPos.position, Quaternion.identity);

            ally.Init();
            ally.OnDead += HandleAllyDead;
            Vector3 patrolPos =
                MapManager.Instance.GetCellCenterWorld(patrolCells[i]);

            ally.SetPatrolPosition(patrolPos);

            allies.Add(ally);
        }
    }



    private bool HasAliveAlly()
    {
        for (int i = allies.Count - 1; i >= 0; i--)
        {
            if (allies[i] == null || allies[i].IsDead)
            {
                allies.RemoveAt(i);
                continue;
            }

            return true;
        }

        return false;
    }

    private void HandleAllyDead(AllyController ally)
    {
        ally.OnDead -= HandleAllyDead;

        allies.Remove(ally);

        if (allies.Count == 0)
        {
            Invoke("SpawnAllAlly", 10f);
        }
    }

    public override void Sell()
    {
        base.Sell();
        foreach(AllyController a in allies)
        {
            a.OnDead -= HandleAllyDead;
            a.OnDespawn();

        }
    }

    public override void Upgrade()
    {
        base.Upgrade();
        
    }
}
