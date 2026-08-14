using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProjectileData : ScriptableObject
{
    [SerializeField] public float Damage;
    [SerializeField] public float Speed;
}
