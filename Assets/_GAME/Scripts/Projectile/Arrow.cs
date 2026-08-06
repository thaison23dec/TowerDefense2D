using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{

    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    protected override void Update()
    {
        if (curentTarget == null) return;

        Vector2 dir = (curentTarget.position - transform.position).normalized;

        transform.position = Vector2.MoveTowards(
            transform.position,
            curentTarget.position,
            speed * Time.deltaTime
        );

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterBase target = collision.GetComponent<CharacterBase>();
        if (target != null)
        {
            switch (team)
            {
                case TeamType.Ally:
                    if (target.Team == TeamType.Enemy)
                        target.TakeDamage(damage);
                    SimplePool.Despawn(this);
                    break;
                case TeamType.Enemy:
                    if (target.Team == TeamType.Ally)
                        target.TakeDamage(damage);
                    SimplePool.Despawn(this);
                    break;
            }
        }
    }
}
