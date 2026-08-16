using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PrefabData : ScriptableObject
{
    [Header("Enemy Prefabs")]
    public EnemyController EnemySlimePrefab;
    public EnemyController EnemyOrcPrefab;
    public EnemyController EnemyBloodPrefab;
    public EnemyController EnemyDemonPrefab;
    [Space(20)]


    [Header("Ally Prefabs")]
    public AllyController AllySwordPrefab;


    [Header("Projectile Prefabs")]
    public Arrow ProjectileArrowPrefab;
    public Bullet ProjectileBulletPrefab;
    [Space(20)]
    


    [Header("Tower Prefabs")]
    public TowerArcher TowerArcher;
    public TowerSpawn TowerSpawn;
    public TowerGun TowerGun;
    [Space(20)]


    [Header("BuildSpot Prefabs")]
    public BuildSpot BuildSpot;
    [Space(20)]


    [Header("VFX Prefabs")]
    public VFXExplosion VFXExplosion;
    public VFXCoinPopUp VFXCoinPopUp;

    public EnemyController GetEnemy(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Slime:
                return EnemySlimePrefab;
            case EnemyType.Orc:
                return EnemyOrcPrefab;
            case EnemyType.Blood:
                return EnemyBloodPrefab;
            case EnemyType.Demon:
                return EnemyDemonPrefab;
        }
        return null;
    }
}
