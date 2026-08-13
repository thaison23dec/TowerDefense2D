using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerSpawn", menuName = "Tower/TowerSpawn")]
public class TowerSpawnData : TowerData
{
    [SerializeField] public AllyController allyPrefab;
}
