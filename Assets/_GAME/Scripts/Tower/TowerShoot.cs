using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : TowerBase
{
    private TowerShootData towerShootData;
    [SerializeField] protected Transform shootPos;
    
    protected List<Vector3> targetList;
    public LayerMask targetMask;
    public float countDownTime;
    public float timer;

    protected virtual void Awake()
    {
        towerShootData = towerData as TowerShootData;
        timer = countDownTime;
        targetList = new List<Vector3>();
    }

    protected virtual void Update()
    {
        Process();
    }

    protected virtual void Process()
    {
        timer -= Time.deltaTime;
        if (IsTrackTarget())
        {
            if (timer <= 0)
            {
                Shoot();
                timer = countDownTime;
            }
        }
    }

    protected virtual void Shoot()
    {
        
    }

    protected virtual bool IsTrackTarget()
    {
        bool isTrack = false;
        Collider2D[] targetsCol = Physics2D.OverlapCircleAll(transform.position, towerShootData.shootRange, targetMask);

        if (targetsCol.Length > 0)
        {
            for (int i = 0; i < targetsCol.Length; i++)
            {
                if (Vector2.Distance(transform.position, targetsCol[i].transform.position) < towerShootData.shootRange)
                {
                    isTrack = true;
                    return isTrack;
                }
            }
            isTrack = false;
        }
        return isTrack;
    }

    protected virtual int FindNearestTarget(List<Vector3> targetArr, Vector3 centerPos)
    {
        int nearestTargetIndex = 0;
        float nearestDistance = Vector3.Distance(targetArr[nearestTargetIndex], centerPos);
        for (int i = 0; i < targetArr.Count; i++)
        {
            if (nearestDistance > Vector3.Distance(targetArr[i], centerPos))
            {
                nearestTargetIndex = i;
                nearestDistance = Vector3.Distance(targetArr[i], centerPos);
            }
        }
        return nearestTargetIndex;
    }

    protected virtual CharacterBase LoadTarget()
    {
        CharacterBase target = null;

        Collider2D[] targetCol = Physics2D.OverlapCircleAll(transform.position, towerShootData.shootRange, targetMask);

        if (targetCol.Length > 0)
        {
            targetList.Clear();
            for (int i = 0; i < targetCol.Length; i++)
            {
                targetList.Add(targetCol[i].transform.position);
            }

            int nearestIndex = FindNearestTarget(targetList, transform.position);

            CharacterBase tempTarget = targetCol[nearestIndex].GetComponent<CharacterBase>();

            if (tempTarget.currentHp > 0.0f && tempTarget.currentState != CharacterBase.CHARACTER_STATE.DIE)
                target = tempTarget;
        }

        if (target != null)
        {
            if (Vector3.Distance(target.transform.position, transform.position) > towerShootData.shootRange)
            {
                target = null;
            }

        }


        return target;
    }
}
