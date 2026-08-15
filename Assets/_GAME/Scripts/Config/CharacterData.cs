using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    [SerializeField] private float sightRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float hp;
    [SerializeField] public int coinBonus;
    [SerializeField] private TeamType teamType;

    public float SightRange => sightRange;
    public float AttackRange => attackRange;
    public float AttackCooldown => attackCooldown;
    public float MoveSpeed => moveSpeed;
    public float Damage => damage;
    public float Hp => hp;
    public TeamType TeamType => teamType;
}
