using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PrefabData : ScriptableObject
{
    public EnemyController EnemyOrcPrefab;
    public AllyController AllySwordPrefab;
    public Arrow ProjectileArrowPrefab;
    public Bullet ProjectileBulletPrefab;
    public GameObject explosion;
}
