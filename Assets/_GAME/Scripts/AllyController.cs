using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyController : CharacterBase
{
    [SerializeField] public Transform patrolPos;

    protected override void Awake()
    {
        base.Awake();
        
    }



    protected override void Die()
    {
        if(patrolPos != null)
        {
            patrolPos.GetComponent<PatrolSlot>().Owner = null;
            patrolPos = null;
        } 
        base.Die();
        
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }


    public override void Walk()
    {
        base.Walk();
        if(currentTarget == null)
        {
            MoveToPosition(patrolPos);
            if(Vector3.Distance(transform.position, patrolPos.position) < 0.1f)
            {
                Idle();
            }
        }
    }
}
