using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AllyController : CharacterBase
{
    [SerializeField] public Vector3 patrolPos;
    public event Action<AllyController> OnDead;

    protected override void Awake()
    {
        base.Awake();
        
    }



    protected override void Die()
    {
        
        base.Die();
        OnDead?.Invoke(this);
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }


    public override void Walk()
    {
        base.Walk();
        if(currentTarget == null && patrolPos != null)
        {
            MoveToPosition(patrolPos);
            if(Vector3.Distance(transform.position, patrolPos) < 0.1f)
            {
                Idle();
            }
        }
    }

    public void SetPatrolPosition(Vector3 pos)
    {
        patrolPos = pos;
    }
}
