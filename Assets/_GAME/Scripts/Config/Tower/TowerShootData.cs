using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerShootData : TowerData
{
    [SerializeField] public Projectile projectile;
    [SerializeField] public float shootRange;
}
