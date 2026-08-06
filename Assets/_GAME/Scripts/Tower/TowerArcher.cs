using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerArcher : MonoBehaviour
{
    [SerializeField] private Transform shootPos;
    [SerializeField] Projectile projectile;
    [SerializeField] private float shootRange;
    private List<Vector3> targetList;
    public LayerMask targetMask;
    public float countDownTime;
    public float timer;

    private void Awake()
    {
        timer = countDownTime;
        targetList = new List<Vector3>();
    }

    private void Update()
    {
        Process();
    }

    private void Process()
    {
        timer -= Time.deltaTime;
        if (IsTrackTarget())
        {
            if(timer <= 0)
            {
                Shoot();
                timer = countDownTime;
            }
        }
    }

    private void Shoot()
    {
        CharacterBase target = LoadTarget();

        if (target == null)
            return;

        Arrow projectile = SimplePool.Spawn<Arrow>(
            PoolType.Projectile_Arrow,
            shootPos.position,
            Quaternion.identity);

        projectile.curentTarget = target.transform;
    }

    public bool IsTrackTarget()
    {
        bool isTrack = false;
        Collider2D[] targetsCol = Physics2D.OverlapCircleAll(transform.position, shootRange, targetMask);

        if (targetsCol.Length > 0)
        {
            for (int i = 0; i < targetsCol.Length; i++)
            {
                if (Vector2.Distance(transform.position, targetsCol[i].transform.position) < shootRange)
                {
                    isTrack = true;
                    return isTrack;
                }
            }
            isTrack = false;
        }
        return isTrack;
    }

    public int FindNearestTarget(List<Vector3> targetArr, Vector3 centerPos)
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

    private CharacterBase LoadTarget()
    {
        CharacterBase target = null;

        Collider2D[] targetCol = Physics2D.OverlapCircleAll(transform.position, shootRange, targetMask);

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
            if (Vector3.Distance(target.transform.position, transform.position) > shootRange)
            {
                target = null;
            }

        }


        return target;
    }
}
