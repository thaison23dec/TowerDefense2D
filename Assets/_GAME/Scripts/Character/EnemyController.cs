using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Slime,
    Orc,

}
public class EnemyController : CharacterBase
{
    public EnemyType Type;
    public int currentWpTargetIndx = 0;

    protected override void Awake()
    {
        base.Awake();
        WaypointIndexCheck();        
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
        if (currentTarget == null)
        {
            MoveToPosition(WaypointPath.Instance.WaypointList[currentWpTargetIndx].position);
        }
    }

    public void WaypointIndexCheck()
    {
        if (currentTarget == null)
        {
            if (Vector3.Distance(transform.position, WaypointPath.Instance.WaypointList[currentWpTargetIndx].position) < 0.1f)
            {
                if (currentWpTargetIndx < WaypointPath.Instance.WaypointList.Length - 1)
                {
                    currentWpTargetIndx++;
                }
            }
        }
    }
}
