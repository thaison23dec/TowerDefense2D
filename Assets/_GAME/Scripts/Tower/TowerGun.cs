using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGun : TowerShoot
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

        //Bullet projectile = SimplePool.Spawn<Bullet>(
        //    PoolType.Projectile_Bullet,
        //    shootPos.position,
        //    Quaternion.identity);

        Bullet projectile = ObjectPoolManager.Instance.SpawnObject(GameManager.Instance.PrefabData.ProjectileBulletPrefab, shootPos.transform.position, Quaternion.identity);

        Vector3 targetPos = target.transform.position;
        projectile.Init(targetPos);
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
