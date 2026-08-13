using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WaveData : ScriptableObject
{
    [SerializeField] public List<EnemyGroupData> GroupList;
    [SerializeField] public float waveDuration;
    [SerializeField] public List<EnemySpawner> SpawnerList;
}
