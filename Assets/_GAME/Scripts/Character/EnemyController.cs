using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Slime,
    Orc,
    Blood,
    Demon,
}
public class EnemyController : CharacterBase
{
    public EnemyType Type;
    public int currentWpTargetIndx = 0;
    public WaypointPath waypointPath;

    protected override void Awake()
    {
        base.Awake();
        //WaypointIndexCheck();        
    }



    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        WaypointIndexCheck();
        if (currentTarget == null)
        {
            Walk();
        }
    }

    public override void Init()
    {
        base.Init();
        currentWpTargetIndx = 0;
    }


    public override void Walk()
    {
        base.Walk();
        if (IsDead) return;
        if (currentTarget == null)
        {
            MoveToPosition(waypointPath.WaypointList[currentWpTargetIndx].position);
        }
    }

    protected override void Die()
    {
        base.Die();
        GameManager.Instance.IncreaseCoin(data.coinBonus);
        UIManager.Instance.TriggerCoinUpdate();
    }

    public override void OnDespawn()
    {
        WaveManager.Instance.OnEnemyDead();
        base.OnDespawn();
    }

    public void WaypointIndexCheck()
    {
        if (currentTarget == null)
        {
            if (Vector3.Distance(transform.position, waypointPath.WaypointList[currentWpTargetIndx].position) < 0.1f)
            {
                if (currentWpTargetIndx < waypointPath.WaypointList.Length - 1)
                {
                    currentWpTargetIndx++;
                }
            }
        }
    }
}
