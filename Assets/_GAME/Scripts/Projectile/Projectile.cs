using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : UnitController
{
    [SerializeField] protected TeamType team;
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected ProjectileData data;
    public Transform curentTarget;
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected SpriteRenderer sprite;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        damage = data.Damage;
        speed = data.Damage;
    }

    protected virtual void Update()
    {
        
    }


}
