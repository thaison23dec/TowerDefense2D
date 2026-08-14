using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTower : MonoBehaviour
{
    public float CurrentHP;
    [SerializeField] private float damageTaken;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController e = collision.GetComponent<EnemyController>();
        if (e != null)
        {
            CurrentHP -= damageTaken;
            e.OnDespawn();
        }
    }
}
