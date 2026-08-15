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
            if(CurrentHP <= 0)
            {
                CurrentHP = 0;
            }
            UIManager.Instance.UpdateMainTowerHpText(CurrentHP);
            e.OnDespawn();
            if(CurrentHP <= 0)
            {
                GameManager.Instance.CurrentState = GameState.Lose;
                GameManager.Instance.EndGame();
            }
        }
    }
}
