using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    [SerializeField] private AllyController allyPrefab;
    [SerializeField] private Transform spawnPos;

    [SerializeField] private int allyCount = 3;
    private List<Vector3Int> patrolCells;
    private List<AllyController> allies = new List<AllyController>();


    private void Start()
    {
        patrolCells = MapManager.Instance.FindPatrolCells(
            transform.position,
            allyCount,
            1
        );

        if (patrolCells == null)
        {
            return;
        }


        SpawnAllAlly();
    }

    private void Update()
    {
        if (!HasAliveAlly())
        {
            
        }
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
            AllyController ally = SimplePool.Spawn<AllyController>(
                PoolType.Ally_Sword,
                spawnPos.position,
                Quaternion.identity);

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
            Invoke("SpawnAllAlly", 2f);
        }
    }
}
