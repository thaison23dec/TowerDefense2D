using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyGroupData : ScriptableObject
{
    [SerializeField] public int GroupQuanity;
    [SerializeField] public EnemyType enemyType;
}
