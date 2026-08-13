using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] public EnemyType enemyType;
    [SerializeField] public int Quantity;
    [SerializeField] public int WaveIndex;
}
