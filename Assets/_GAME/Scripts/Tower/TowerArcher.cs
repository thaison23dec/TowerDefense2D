using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerArcher : TowerShoot
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Process()
    {
        base.Process();
    }

    protected override void Shoot()
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

    protected override bool IsTrackTarget()
    {
        return base.IsTrackTarget();
    }

    protected override int FindNearestTarget(List<Vector3> targetArr, Vector3 centerPos)
    {
        return base.FindNearestTarget(targetArr, centerPos);
    }

    protected override CharacterBase LoadTarget()
    {
        return base.LoadTarget();
    }
}
